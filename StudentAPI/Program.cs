using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StudentAPI;
using StudentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StudentsDBSettings>(builder.Configuration.GetSection("StudentsDBSettings"));
builder.Services.AddSingleton<StudentsService>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
