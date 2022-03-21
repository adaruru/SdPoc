using PerformanceApi;
using PerformanceApi.Middleware;
using PerformanceApp.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                    .AddJsonFile($"notifysetting.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

//builder.Configuration.GetSection(nameof(PerformanceSetting)).Bind(new PerformanceSetting());
//builder.Configuration.GetSection(nameof(NotifySetting)).Bind(new NotifySetting());
builder.Services.Configure<PerformanceSetting>(options => builder.Configuration.GetSection("PerformanceSetting").Bind(options));
builder.Services.Configure<NotifySetting>(options => builder.Configuration.GetSection("NotifySetting").Bind(options));

//Add services to the container.
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);
builder.Services.AddLibs();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMvcWithDefaultRoute();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

//設定404
app.UseMiddleware<ContextMiddleware>();

//app.UseEndpoints(endpoints => {});

app.Run();
