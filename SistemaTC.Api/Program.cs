using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SistemaTC.Api.Filters;
using SistemaTC.Api.Middleware;
using SistemaTC.Api.Profile;
using SistemaTC.Core;
using SistemaTC.Data;
using SistemaTC.DTO.User;
using SistemaTC.Services;
using SistemaTC.Services.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema TC API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme {
        Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
        },
        []
    }
    });
});
builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var appSettings = builder.Configuration.Get<AppSettings>() ?? new();
builder.Services.AddSingleton(appSettings);
builder.Services.AddDbContext<TCContext>(options => options.UseMySQL(appSettings?.TCConnection ?? ""));

builder.Services.AddTransient<ITCContext, TCContext>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICreditCardService, CreditCardService>();
builder.Services.AddScoped<ICutoffService, CutoffService>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
builder.Services.AddScoped<ICreditCardTransactionService, CreditCardTransactionService>();

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddIdentity<LoggedInUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TCContext>();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.IncludeErrorDetails = true;
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtTokenSettings.SymmetricSecurityKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Register authorization services
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(nameof(PermissionRequirement), policy =>
        policy.Requirements.Add(new PermissionRequirement(new List<string>() { })));
});


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

app.UseAuthorizationMiddleware();

app.MapControllers();

app.Run();
