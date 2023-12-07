using Library.Shared.Models;
using Library.Shared.Services.LibraryService;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.API.Data;
using LibraryAPI.API.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILibraryService, LibraryServiceC>();

var app = builder.Build();

// Configure CORS
app.UseCors(builder => builder
    .WithOrigins("https://localhost:7088") 
    .AllowAnyHeader()
    .AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
