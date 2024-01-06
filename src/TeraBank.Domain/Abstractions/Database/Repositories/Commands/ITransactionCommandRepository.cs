using TeraBank.Domain.Entities;

namespace TeraBank.Domain.Abstractions.Database.Repositories.Commands;

public interface ITransactionCommandRepository : ICommandRepository<Transaction>
{
}
