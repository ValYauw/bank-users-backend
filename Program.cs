using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BNI_Users_backend.Data;
using BNI_Users_backend.Models;
using BNI_Users_backend.Seed;
using BNI_Users_backend.Controllers;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("UserContext") ?? throw new InvalidOperationException("Connection string 'UserContext' not found.")));

// Untuk development: Perbolehkan request dari domain apapun
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
    builder =>
    {
      // Sebutkan domain yang diperbolehkan di sini
      builder.WithOrigins("http://localhost:5173", "*")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Tambah model validation
builder.Services.AddScoped<IValidator<User>, UserValidator>();

var app = builder.Build();

// Seeding database
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  SeedUsers.Initialize(services);
}

// Setup CORS
app.UseCors(MyAllowSpecificOrigins);


/*
 * API Routing
 */
// app.UseRouting();
app.MapGet("/", UsersController.GetUsers);
app.MapGet("/{id:int}", UsersController.GetUserById);
app.MapPost("/", UsersController.AddUser);
app.MapPut("/{id:int}", UsersController.UpdateUser);
app.MapDelete("/{id:int}", UsersController.DeleteUser);

app.Run();
