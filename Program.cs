using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Controllers;
using TheaterLaakBackend.Generators;
using TheaterLaakBackend.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

// Add the Contexts
builder.Services.AddDbContext<TheaterDbContext>();

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

// app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseCors();

app.UseAuthorization();

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