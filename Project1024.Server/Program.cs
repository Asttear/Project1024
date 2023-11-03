using Microsoft.EntityFrameworkCore;
using Project1024.Server.Data;
using Project1024.Server.Services;
using System.Configuration;
using System;
using Microsoft.Extensions.Options;
using Project1024.Server.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Project1024.Server.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();


builder.Services.AddDbContext<VideoContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("MySql")!;
    options.UseMySQL(connectionString);
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddDbContext<UserContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("MySql")!;
    options.UseMySQL(connectionString);
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<UserContext>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<VideoService>();
builder.Services.AddScoped<VideoCategoryService>();
builder.Services.AddSingleton<QiniuService>();
builder.Services.Configure<QiniuOptions>(builder.Configuration.GetSection("Qiniu"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var securityKey = builder.Configuration.GetRequiredSection("JwtSettings")["SecurityKey"] 
    ?? throw new InvalidOperationException("SecurityKey cannot be null!");
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey)),
    ClockSkew = TimeSpan.Zero,
};

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => { options.TokenValidationParameters = tokenValidationParameters; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
