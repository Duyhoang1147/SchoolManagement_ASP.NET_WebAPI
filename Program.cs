using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagerment_WebAPI.Data;
using SchoolManagerment_WebAPI.Model;
using SchoolManagerment_WebAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  

builder.Services.AddEndpointsApiExplorer();

//services
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<TeacherService>();
builder.Services.AddScoped<ClassroomService>();
builder.Services.AddScoped<TeacherClassroomService>();
// builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
// {
//     options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
// });

// builder.Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//     });
builder.Services.AddSwaggerGen();

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

app.MapGet("/", () => "Hello World!");

app.Run();
