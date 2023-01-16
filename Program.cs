using TheaterLaakBackend.Controllers;
using TheaterLaakBackend.Generators;
using TheaterLaakBackend.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.OpenApi.Models;


var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Add the Contexts
builder.Services.AddDbContext<TheaterDbContext>();
builder.Services.AddIdentity<Account, IdentityRole>()
                        .AddEntityFrameworkStores<TheaterDbContext>()
                        .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
  // Paswoord instellingen
  // options.Password.RequireDigit = true;
  // options.Password.RequireLowercase = true;
  // options.Password.RequireNonAlphanumeric = true;
  // options.Password.RequireUppercase = true;
  // options.Password.RequiredLength = 6;
  // options.Password.RequiredUniqueChars = 1;

  // Gebruiker instellingen
  options.User.RequireUniqueEmail = true;
  options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"; // here is the issue
});

//JWT tokens
builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
            {
            opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:5086",
                    ValidAudience = "http://localhost:5086",
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh89awe98f89uawef9j8aw89hefawef"))
                };
            });


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { //<-- NOTE 'Add' instead of 'Configure'
    c.SwaggerDoc("v3", new OpenApiInfo {
        Title = "GTrackAPI",
        Version = "v3"
    });
});
// builder.Services.AddSwaggerGen(options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
//   {
//     Name = "Authorization",
//     Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
//     In = ParameterLocation.Header,
//     Type = SecuritySchemeType.ApiKey,
//     Scheme = "Bearer"
//   }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseCors();

using (var scope = app.Services.CreateScope())
  {
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new string[3] {"Gast", "Donateur", "Medewerker", "Admin"};
    foreach (var role in roles)
    {
      if (!await roleManager.RoleExistsAsync(role))
      {
          await roleManager.CreateAsync(new IdentityRole(role));
      }
    }
  }

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

var context = new TheaterDbContext();
if (!context.Accounts.Any())
{
    var dbEntryGenerator = new DbEntryGenerator(new TheaterDbContext());
    dbEntryGenerator.DatabaseGenerator();  
}

app.Run();

//TODO: Change the if(true) statement


//TODO: make rules for database