using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMvcWithDefaultRoute();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();//作業4?
app.UseAuthorization();

app.UseMiddleware<ContextMiddleware>();
//app.UseEndpoints(endpoints => {});

app.Run();
