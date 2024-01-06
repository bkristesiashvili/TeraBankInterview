using TeraBank.Domain.Abstractions.Database;

namespace TeraBank.Infrastructure.Database;

internal sealed class UnitOfWork(IBankDbContext dbContext) : IUnitOfWork
{
    public IBankDbContext DbContext { get; } = dbContext;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await DbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
    }
}
