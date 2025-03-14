using BankingDashboard.Application.Common.Interfaces;
using BankingDashboard.Infrastructure;
using BankingDashboard.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

//var assemblies = AppDomain.CurrentDomain.GetAssemblies();
//var infrastructureAssembly = assemblies.FirstOrDefault(a => a.GetName().Name == "BankingDashboard.Infrastructure");
//Console.WriteLine($"Infrastructure assembly loaded: {infrastructureAssembly != null}");

//if (infrastructureAssembly != null)
//{
//    var userRepoType = infrastructureAssembly.GetTypes().FirstOrDefault(t => t.Name == "UserRepository");
//    Console.WriteLine($"UserRepository type found: {userRepoType != null}");
//}

// Add services to the container
builder.Services.AddInfrastructure(builder.Configuration);

// Explicitly register the UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsPolicyName = "AllowFrontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Allow frontend origin
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyName);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
