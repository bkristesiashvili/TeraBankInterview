using TeraBank.Domain.Abstractions.Database.Entities;

namespace TeraBank.Domain.Abstractions.Database.Repositories.Commands;

public interface ICommandRepository<TEntity> : IRepository
    where TEntity : BaseEntity
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
