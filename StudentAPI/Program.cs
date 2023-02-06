using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StudentAPI;
using StudentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration.GetSection("StudentsDBSettings");

builder.Services.AddSingleton(x => new MongoClient(config["ConnectionString"]));
builder.Services.AddTransient<IMongoDatabase>(x =>
    x.GetRequiredService<MongoClient>().GetDatabase(config["DatabaseName"]));

builder.Services.AddTransient<StudentsService>();
builder.Services.AddTransient<DatabaseService>();
builder.Services.AddTransient<LecturersService>();
builder.Services.AddTransient<ExamService>();
builder.Services.AddTransient<StudGroupService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
