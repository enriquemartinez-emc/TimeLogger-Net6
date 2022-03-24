using System.Globalization;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timelogger;
using Timelogger.Api.Infrastructure.Errors;
using Timelogger.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
RegisterServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureApplication(app);

app.Run();

static void RegisterServices(WebApplicationBuilder builder)
{
    var services = builder.Services;

    services.AddMediatR(typeof(Program));
    services.AddCors();
    services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));
    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddMvc()
        .AddFluentValidation(cfg =>
        {
            cfg.RegisterValidatorsFromAssemblyContaining<Program>();
        });
}

static void ConfigureApplication(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseCors(builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());

    // app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    SeedDatabase();
}

static void SeedDatabase()
{
    var optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
    optionsBuilder.UseInMemoryDatabase("e-conomic interview");

    using (var context = new ApiContext(optionsBuilder.Options))
    {
        context.Database.EnsureCreated();

        var testProjects = new List<Project>();

        var initialProject = new Project("e-conomic Interview", DateTime.ParseExact("20220331", "yyyyMMdd", CultureInfo.InvariantCulture));

        initialProject.AddTimeLog("Creating new documents", 35);
        initialProject.AddTimeLog("Interviewing users", 55);
        initialProject.AddTimeLog("Creating new diagrams", 30);
        initialProject.AddTimeLog("Finishing last details", 45);

        testProjects.Add(initialProject);
        testProjects.Add(new Project("Fintech Interview", DateTime.ParseExact("20220531", "yyyyMMdd", CultureInfo.InvariantCulture)));
        testProjects.Add(new Project("Airport Interview", DateTime.ParseExact("20220630", "yyyyMMdd", CultureInfo.InvariantCulture)));
        testProjects.Add(new Project("Real Estate Interview", DateTime.ParseExact("20220831", "yyyyMMdd", CultureInfo.InvariantCulture)));

        context.Projects.AddRange(testProjects);

        context.SaveChanges();
    }
}

// For testing purposes
public partial class Program { }