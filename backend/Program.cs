using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend.Services;
using backend.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Добавление ваших сервисов
builder.Services.AddSingleton<IUser, UserService>();

// Добавление контроллеров
builder.Services.AddControllers();

// Добавление аутентификации JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_key_1234567890123456")), // Заменить на настоящий секретный ключ
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Добавление авторизации
builder.Services.AddAuthorization();

var app = builder.Build();

// Настройка middleware pipeline
app.UseRouting();

// CORS должен быть до аутентификации и авторизации
app.UseCors("AllowAll");

// Аутентификация и авторизация
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();