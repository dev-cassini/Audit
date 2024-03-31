using System.Reflection;
using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Abstraction.Tooling;
using Audit.Domain.Model;
using Audit.Infrastructure.Persistence.EntityFramework.Interceptors.SaveChanges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
    
    /// <summary>
    /// Add custom audit entity framework interceptors for all types in provided <paramref name="assembly"/>
    /// that implement <see cref="IAuditable{T}"/>.
    /// </summary>
    /// <param name="assembly">Assembly to query for auditable types.</param>
    /// <returns>The same instance of the configurator.</returns>
    public Configurator AddAuditInterceptors(Assembly assembly)
    {
        var auditableTypes = assembly.GetTypes()
            .SelectMany(x => x.GetInterfaces())
            .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IAuditable<>))
            .ToList();

        var auditRecordTypes = auditableTypes
            .SelectMany(x => x.GetGenericArguments())
            .Distinct()
            .ToList();

        var dateTimeProvider = _serviceProvider.GetRequiredService<IDateTimeProvider>();
        var auditInterceptors = new List<IInterceptor>();
        foreach (var auditRecordType in auditRecordTypes)
        {
            var createInitialAuditInterceptor = typeof(CreateInitialAuditInterceptor<>).MakeGenericType(auditRecordType);
            if (Activator.CreateInstance(createInitialAuditInterceptor, dateTimeProvider) is IInterceptor createInitialAuditInterceptorInstance)
            {
                auditInterceptors.Add(createInitialAuditInterceptorInstance);
            }
            
            var createUpdateAuditInterceptor = typeof(CreateUpdateAuditInterceptor<>).MakeGenericType(auditRecordType);
            if (Activator.CreateInstance(createUpdateAuditInterceptor, dateTimeProvider) is IInterceptor createUpdateAuditInterceptorInstance)
            {
                auditInterceptors.Add(createUpdateAuditInterceptorInstance);
            }
        }

        _dbContextOptionsBuilder.AddInterceptors(auditInterceptors);
            
        return this;
    }
    
    public Configurator AddAuditInterceptors<TEntity, TAuditRecord>() 
        where TEntity : IAuditable<TAuditRecord> 
        where TAuditRecord : IAuditRecord
    {
        var dateTimeProvider = _serviceProvider.GetRequiredService<IDateTimeProvider>();
        
        _dbContextOptionsBuilder
            .AddInterceptors(
                new CreateInitialAuditInterceptor<TAuditRecord>(dateTimeProvider),
                new CreateUpdateAuditInterceptor<TAuditRecord>(dateTimeProvider));
            
        return this;
    }
}