namespace TeraBank.Domain.Abstractions.Database;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
