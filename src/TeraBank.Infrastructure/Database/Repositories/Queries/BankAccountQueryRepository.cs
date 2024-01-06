using Microsoft.EntityFrameworkCore;

using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Abstractions.Database.Repositories.Queries;
using TeraBank.Domain.Entities;

namespace TeraBank.Infrastructure.Database.Repositories.Queries;

internal sealed class BankAccountQueryRepository(IBankDbContext dbContext) : BaseQueryRepository<BankAccount>(dbContext), IBankAccountQueryRepository
{
    public Task<BankAccount> FindByIBan(string iBan)
    {
        return DbContext.Set<BankAccount>().SingleOrDefaultAsync(x => x.IBan == iBan);
    }
}
