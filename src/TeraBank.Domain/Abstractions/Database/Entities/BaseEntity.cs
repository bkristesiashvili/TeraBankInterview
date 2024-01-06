namespace TeraBank.Domain.Abstractions.Database.Entities;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTimeOffset CreateDate { get; protected set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdateDate { get; set; }

    public void Update()
    {
        UpdateDate = DateTimeOffset.Now;
    }
}