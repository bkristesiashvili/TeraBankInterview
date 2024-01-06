using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TeraBank.Domain.Abstractions.Database;

public interface IBankDbContext
{
    public DatabaseFacade Database { get; }

    public DbSet<TEntity> Set<TEntity>() where TEntity : class;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
