using Audit.Domain.Abstraction.Enums;
using Audit.Domain.Abstraction.Model;

namespace Audit.Domain.Model;

public class Transaction : Abstraction.Model.Transaction
{
    public Transaction(
        Guid id, 
        TransactionStatus status,
        DateTimeOffset dateTimeCreated,
        DateTimeOffset? dateTimeFilling,
        Vehicle vehicle, 
        Pump? pump)
        : base(
            id,
            status,
            dateTimeCreated,
            dateTimeFilling,
            vehicle,
            pump) { }
}