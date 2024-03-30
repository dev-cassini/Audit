using Audit.Api.Endpoints;
using Audit.Domain;
using Audit.Infrastructure;
using Audit.Infrastructure.Persistence.EntityFramework;
using EntityFramework = Audit.Infrastructure.Persistence.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDomainServices()
    .AddInfrastructure(infrastructureConfigurator =>
    {
        infrastructureConfigurator
            .AddPersistence(persistenceConfigurator =>
            {
                persistenceConfigurator
                    .AddEntityFramework(efConfigurator =>
                    {
                        efConfigurator
                            .AddDbContext<EntityFramework.Write.DbContext>(
                                builder.Configuration,
                                dbConfigurator => dbConfigurator.UsePostgres(),
                                interceptorConfigurator => interceptorConfigurator.AddAuditInterceptors());
                        
                        efConfigurator
                            .AddDbContext<EntityFramework.Read.DbContext>(
                                builder.Configuration,
                                dbConfigurator => dbConfigurator.UsePostgres());
                    });
            })
            .AddMessaging(messagingConfigurator => messagingConfigurator.AddMediatR());
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.MigrateDatabase();

app
    .RegisterEndpoints()
    .UseHttpsRedirection();

app.Run();