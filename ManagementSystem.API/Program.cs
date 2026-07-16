using ManagementSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddValidation();     
builder.Services.AddProblemDetails();   
builder.Services.AddOpenApi();          

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
}

app.UseHttpsRedirection();


app.MapGet("/health", () => TypedResults.Ok(new { Status = "Healthy", Version = "10.0" }))
   .WithName("HealthCheck")
   .WithTags("System");


app.Run();