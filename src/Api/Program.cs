var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

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
