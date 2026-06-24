using School.Application.Extensions;
using School.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.AddApplication();
builder.AddInfrastructure();

if (!builder.Environment.IsEnvironment("master"))
{
    builder.Services.AddSwaggerGen();
}

if (builder.Environment.IsEnvironment("local"))
{
    builder.Configuration.AddUserSecrets<Program>();
}

var app = builder.Build();

if (!builder.Environment.IsEnvironment("master"))
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
