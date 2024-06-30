using Audit.Infrastructure.Messaging.MediatR;
using Audit.Infrastructure.Persistence.EntityFramework;
using Audit.Infrastructure.Persistence.EntityFramework.Repositories;
using Falc.CleanArchitecture.Infrastructure;
using Falc.CleanArchitecture.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMediatR(typeof(Audit.Application.Marker).Assembly)
    .AddEfRepositories()
    .AddInfrastructure(infrastructureConfigurator =>
    {
        infrastructureConfigurator
            .AddPersistence(persistenceConfigurator =>
            {
                persistenceConfigurator
                    .AddEntityFramework(efConfigurator =>
                    {
                        efConfigurator
                            .AddDbContext<FuellerDbContext>((_, optionsBuilder) =>
                            {
                                optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
                            });
                    });
            });
    });

var app = builder.Build();

app.Services.MigrateDatabase<FuellerDbContext>();

app.UseHttpsRedirection();

app.Run();