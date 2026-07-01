using Microsoft.OpenApi;
using School.Application.Extensions;
using School.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.AddApplication();
builder.AddInfrastructure();

builder.Services.AddAuthorization();

if (!builder.Environment.IsEnvironment("master"))
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
        });
        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Description = "Enter 'Bearer {token}'",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
        };

        c.AddSecurityDefinition("bearer", securityScheme);
        c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("bearer", document)] = []
        });
    });
}

if (builder.Environment.IsEnvironment("local"))
{
    builder.Configuration.AddUserSecrets<Program>();
}

var app = builder.Build();

if (!builder.Environment.IsEnvironment("master"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
