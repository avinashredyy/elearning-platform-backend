using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "E-Learning Platform API",
        Version = "v1",
        Description = "Enterprise E-Learning Platform with AI-Powered Personalization"
    });
});

// Add CORS for Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Learning Platform API v1");
        c.RoutePrefix = string.Empty; // Makes Swagger UI available at root
    });
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

// Map health check endpoint
app.MapHealthChecks("/api/health");

app.Run();