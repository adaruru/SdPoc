using ProxyHost.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddJsonFile($"Config/ProxySetting.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProxySetting>(options => builder.Configuration.GetSection("ProxySetting").Bind(options));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMvcWithDefaultRoute();
app.Run();
