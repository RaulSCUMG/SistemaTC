using Microsoft.EntityFrameworkCore;
using SistemaTC.Api.Profile;
using SistemaTC.Core;
using SistemaTC.Data;
using SistemaTC.Services;
using SistemaTC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var appSettings = builder.Configuration.Get<AppSettings>();
builder.Services.AddSingleton(appSettings ?? new());
builder.Services.AddTransient<ITCContext, TCContext>();
builder.Services.AddDbContext<TCContext>(options => options.UseMySQL(appSettings?.TCConnection ?? ""));

// Add services to the container.
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
