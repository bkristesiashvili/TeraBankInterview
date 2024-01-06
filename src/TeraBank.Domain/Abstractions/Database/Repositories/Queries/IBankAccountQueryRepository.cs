using TeraBank.Domain.Entities;

namespace TeraBank.Domain.Abstractions.Database.Repositories.Queries;

public interface IBankAccountQueryRepository : IQueryRepository<BankAccount>
{
    Task<BankAccount> FindByIBan(string iBan);
}
