using Microsoft.EntityFrameworkCore;
using TimeRecord.Data;
using TimeRecord.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "MyPolicyCors",
        policy =>
        {
            policy
                .WithOrigins("https://ponto-facil-lake.vercel.app/", "http://localhost:3000")
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
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("MyPolicyCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
