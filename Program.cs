using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using TerekhovTest;
using TerekhovTest.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => { 

    options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EmailDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
