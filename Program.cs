using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TheaterLaakBackend.Contexts;
using TheaterLaakBackend.Generators;
using TheaterLaakBackend.Models;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

//fix cors settings
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
if (Environment.GetEnvironmentVariable("SQLAZURECONNSTR_defaultConnection") != null)
{
    builder.Services.AddDbContext<TheaterDbContext, SqlServerTheaterDbContext>();
}
else
{
    builder.Services.AddDbContext<TheaterDbContext, SqlLiteTheaterDbContext>();
}

builder.Services.AddIdentity<Account, IdentityRole>()
                        .AddEntityFrameworkStores<TheaterDbContext>()
                        .AddDefaultTokenProviders();
// set options for specifications for indetity role login
builder.Services.Configure<IdentityOptions>(options =>
{
  // Paswoord instellingen
  // options.Password.RequireDigit = true;
  // options.Password.RequireLowercase = true;
  // options.Password.RequireNonAlphanumeric = true;
  // options.Password.RequireUppercase = true;
  // options.Password.RequiredLength = 6;
  // options.Password.RequiredUniqueChars = 1;
  options.Lockout.AllowedForNewUsers = true;
  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
  options.Lockout.MaxFailedAccessAttempts = 3;

  // Gebruiker instellingen
  options.User.RequireUniqueEmail = true;
  options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-._@+/ "; // here is the issue
});

//JWT tokens
builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
            {
            opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:5086",
                    ValidAudience = "http://localhost:5086",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh89awe98f89uawef9j8aw89hefawef"))
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
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options => options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TheaterDbContext>();
    context.Database.Migrate();
    
    if (app.Environment.IsDevelopment() && !context.Accounts.Any())
    {
        new DbEntryGenerator(context).DatabaseGenerator();  
    }
}

// app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseCors();

//make the roles if they dont already exist
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new string[5] {"Gast", "Artist", "Donateur", "Medewerker", "Admin"};
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

//TODO: Change the if(true) statement


//TODO: make rules for database