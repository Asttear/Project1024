using Microsoft.EntityFrameworkCore;
using Project1024.Server.Data;
using Project1024.Server.Services;

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

builder.Services.AddScoped<VideoService>();
builder.Services.Configure<QiniuOptions>(builder.Configuration.GetSection("Qiniu"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
