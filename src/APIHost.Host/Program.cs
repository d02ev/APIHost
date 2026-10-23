using APIHost.Modules.Resume;
using APIHost.SharedKernel.Modules;

var builder = WebApplication.CreateBuilder(args);
IModule[] modules = [];

foreach (var module in modules)
    module.RegisterServices(builder.Services, builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
foreach (var module in modules)
    module.MapEndpoints(app);

app.MapGet("/health", () => Results.Ok(new { status = "The Host is Healthy", timestamp = DateTime.UtcNow }));

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.Run();