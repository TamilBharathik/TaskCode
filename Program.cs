using backendtask.Context;
using backendtask.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using backendtask.Model;
using backendtask.Controllers;
using backendtask.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddTransient<DapperDBContext>();
builder.Services.AddTransient<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IDepartmentRepository>(provider =>
    new DepartmentRepository(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IDateAndInvoiceRepository, DateAndInvoiceRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

// Configure HTTPS redirection
app.UseHttpsRedirection();

// Configure CORS
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();

