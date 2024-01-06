using TeraBank.Domain.Abstractions.Database.Entities;

namespace TeraBank.Domain.Abstractions.Database.Repositories.Queries;

public interface IQueryRepository<TEntity> : IRepository
    where TEntity : BaseEntity
{
    Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
}
