using PerformanceApi;
using PerformanceApi.Middleware;
using PerformanceApp.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                    .AddJsonFile($"notifysetting.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

builder.Configuration.GetSection(nameof(PerformanceSetting)).Bind(new PerformanceSetting());
builder.Configuration.GetSection(nameof(NotifySetting)).Bind(new NotifySetting());


//Add services to the container.
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);
builder.Services.AddLibs();

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
