using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SkymeyLibs;
using SkymeyLibs.Data;
using SkymeyLibs.Interfaces.Data;
using SkymeyLibs.Interfaces.User;
using SkymeyLibs.Models.User;
using SkymeyUserService.Actions.JWT;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "SkymeyUserService", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            }, new string[]{}
        }
    });
});
builder.Configuration.SetBasePath(builder.Configuration.GetSection("ConfigPath").Value)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

#region Auth
builder.Services.AddOptions();
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings")); // Сопоставление JWTSettings с файлом конфигурации appsettings.json

var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value; // Секретный код из appsettings.json
var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value; // Издатель токена. Можно указать любое название
var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value; // Пользователь токена. Можно указать любое название

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

 
builder.Services.AddAuthentication(option => { // Указываем аутентификацию с помощью токенов
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(option => {
    option.TokenValidationParameters = new TokenValidationParameters
    { // Задаем параметры валидации токена. Нужно проверять: Издатель, потребитель, ключ, срок действия
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        IssuerSigningKey = signingKey,
        ValidateIssuerSigningKey = true,
        LifetimeValidator = CustomLifetime.CustomLifetimeValidator
    };
});
#endregion
builder.Services.Configure<MainSettings>(builder.Configuration.GetSection("MainSettings"));
builder.Services.AddSingleton<IUserRepository, UserRepository>();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
