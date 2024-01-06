using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Abstractions.Database.Repositories.Commands;
using TeraBank.Domain.Entities;

namespace TeraBank.Infrastructure.Database.Repositories.Commands;

internal sealed class BankAccountCommandRepository(IBankDbContext dbContext)
    : BaseCommandRepository<BankAccount>(dbContext)
    , IBankAccountCommandRepository
{
}
