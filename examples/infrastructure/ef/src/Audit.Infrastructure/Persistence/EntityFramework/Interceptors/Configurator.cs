using Audit.Domain.Abstraction.Tooling;
using Audit.Domain.Model;
using Audit.Infrastructure.Persistence.EntityFramework.Interceptors.SaveChanges;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence.EntityFramework.Interceptors;

public class Configurator
{
    private readonly IServiceProvider _serviceProvider;
    private readonly DbContextOptionsBuilder _dbContextOptionsBuilder;

    public Configurator(
        IServiceProvider serviceProvider, 
        DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        _serviceProvider = serviceProvider;
        _dbContextOptionsBuilder = dbContextOptionsBuilder;
    }

    public Configurator AddAuditInterceptors()
    {
        var dateTimeProvider = _serviceProvider.GetRequiredService<IDateTimeProvider>();
        _dbContextOptionsBuilder
            .AddInterceptors(
                new CreateInitialAuditInterceptor<PumpAuditRecord>(dateTimeProvider),
                new CreateUpdateAuditInterceptor<PumpAuditRecord>(dateTimeProvider));
            
        return this;
    }
}