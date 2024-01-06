using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Abstractions.Database.Entities;
using TeraBank.Domain.Abstractions.Database.Repositories.Commands;

namespace TeraBank.Infrastructure.Database.Repositories.Commands;

internal abstract class BaseCommandRepository<TEntity>(IBankDbContext dbContext) : ICommandRepository<TEntity>
    where TEntity : BaseEntity
{
    protected IBankDbContext DbContext => dbContext;

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        return Task.CompletedTask;
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            entity.UpdateDate = DateTimeOffset.Now;
            dbContext.Set<TEntity>().Update(entity);
        }

        return Task.CompletedTask;
    }
}
