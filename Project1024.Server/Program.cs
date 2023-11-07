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
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();


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
builder.Services.AddScoped<UserFollowerService>();
builder.Services.AddScoped<LikeService>();
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



//测试swaggger加token
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"}
                           },new string[] { }
                        }
                    });


});
//测试swaggger加token

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
