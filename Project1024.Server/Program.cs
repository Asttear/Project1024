using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project1024.Server.Data;
using Project1024.Server.Models;
using Project1024.Server.Services;
using Project1024.Server.Settings;
using Project1024.Shared.Services;
using System.Text;

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
}).AddEntityFrameworkStores<UserContext>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IVideoCategoryService, VideoCategoryService>();
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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => { options.TokenValidationParameters = tokenValidationParameters; });

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
           }, Array.Empty<string>()
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
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();