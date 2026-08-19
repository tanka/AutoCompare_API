    // Connection to the DB
using AutoCompare_API.Data;
using AutoCompare_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // DefaultConnection is at appsettings.json file

// to create tables for identity framework as needed.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();

// Add services to the container.

builder.Services.AddControllers();

// get the settings for JWR auth purpose
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.MapScalarApiReference();
//}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

// Cors needed before redirection 
app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("*"));
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// At the very end of Program.cs
//public partial class Program { }