using School.Application.Extensions;
using School.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

if (!builder.Environment.IsEnvironment("master"))
{
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (!builder.Environment.IsEnvironment("master"))
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
