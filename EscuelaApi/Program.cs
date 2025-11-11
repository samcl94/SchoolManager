using SchoolApi.Interfaces;
using SchoolApi.Models;
using SchoolApi.Services;
using Microsoft.EntityFrameworkCore;
using SchoolApi.GenerateFakeData;

var builder = WebApplication.CreateBuilder(args);

// configure string connection to the DB school
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EscuelaDb")));

// register services
builder.Services.AddScoped<IStudentService, StudentService>(); // Dependency injection container to initialize controllers constuctors

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Var used to generate data in BBDD
var test = false;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    if (test)
    {
        using var scope = app.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<SchoolContext>();
        await Seeder.SeedStudentsAsync(ctx);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
