using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManager.DatabaseComms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "TaskManager",
            Description = "Task Api",
            Contact = new OpenApiContact
            {
                Name = "Yolisa",
                Email = "yolisapingilili2@gmail.com",
                Url = new Uri("https://github.com/Darth-Tenebros")
            },
            License = new OpenApiLicense
            {
                Name = "no",
                Url = new Uri("https://github.com/Darth-Tenebros")
            },
            Version = "v1"
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });


// builder.Services.AddDbContext<TaskManagerDbContext>(options => options.UseInMemoryDatabase("TaskManagerStore"));
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TaskDBConn")));
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