using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Data;
using TimeRecord.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().
    ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Where(x => x.Value?.Errors.Count > 0).ToDictionary(k => k.Key, v => v.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
            return new BadRequestObjectResult(
                new { message = "Dados invalidos", errors });
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "MyPolicyCors",
        policy =>
        {
            policy
                .WithOrigins("https://ponto-facil-lake.vercel.app", "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    );
});

var connectionString =
    Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")
    ?? builder.Configuration.GetConnectionString("AppDbConnectionString");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// inejtar dependencias
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<MarkingsService>();
builder.Services.AddScoped<BusinessService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("MyPolicyCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
