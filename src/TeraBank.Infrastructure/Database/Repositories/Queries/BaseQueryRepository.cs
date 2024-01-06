using Microsoft.EntityFrameworkCore;

using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Abstractions.Database.Entities;
using TeraBank.Domain.Abstractions.Database.Repositories.Queries;

namespace TeraBank.Infrastructure.Database.Repositories.Queries;

internal abstract class BaseQueryRepository<TEntity>(IBankDbContext dbContext) : IQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    protected IBankDbContext DbContext => dbContext;

    public Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            return Task.FromResult(DbContext.Set<TEntity>().AsQueryable());
        }

        return Task.FromResult(Enumerable.Empty<TEntity>().AsQueryable());
    }

    public Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
