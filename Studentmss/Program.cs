using MongoDB.Driver;
using Studentms.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddRazorPages();
IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Set the base path for appsettings.json
           .AddJsonFile("appsettings.json") // Add the appsettings.json file
           .Build();

var mongoClient = new MongoClient(configuration.GetConnectionString("MongoDb"));//set connection string
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
var app = builder.Build();

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
