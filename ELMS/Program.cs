
using ELearningWeb.DbContexts;
using ELearningWeb.DTO;
using ELearningWeb.Helper;
using ELearningWeb.IRepository;
using ELearningWeb.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var audienceConfig = builder.Configuration.GetSection("Audience");
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateIssuer = true,
            ValidIssuer = audienceConfig["Iss"],
            ValidateAudience = true,
            ValidAudience = audienceConfig["Aud"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true,
        };

        options.RequireHttpsMetadata = false;
    });




builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("Development");




builder.Services.AddDbContextFactory<WriteDbContext>(
    options => options.UseSqlServer(connection));
builder.Services.AddDbContextFactory<ReadDbContext>(// error showing this line 
    options => options.UseSqlServer(connection));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Audience>(builder.Configuration.GetSection("Audience"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IRegistrationService, RegistrationService>();
builder.Services.AddTransient<IUserLogInService, UserLogInService>();
builder.Services.AddTransient<IEmailService, EmailService>();




var app = builder.Build();

app.UseCors(policy => policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials());

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
