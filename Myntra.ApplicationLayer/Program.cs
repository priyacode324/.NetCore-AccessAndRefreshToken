using Myntra.BLL.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();     // Important for Swagger
//builder.Services.AddSwaggerGen();               // Classic Swagger
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString != null)
{
    builder.Services.RegisterServices(connectionString,builder!.Configuration);
}

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod())
);
var app = builder.Build();
await app.Services.SeedDataAsync();
app.UseCors();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
if (app.Environment.IsDevelopment())
{
    // Serves OpenAPI JSON at /openapi/v1.json
    app.MapOpenApi();

    // Use a nice UI (Swagger UI still works great with it)
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Myntra API V1");
        options.RoutePrefix = "swagger";
    });
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
