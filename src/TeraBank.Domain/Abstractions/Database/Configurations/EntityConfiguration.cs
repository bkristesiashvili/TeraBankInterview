using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeraBank.Common.Helpers;
using TeraBank.Domain.Abstractions.Database.Entities;

namespace TeraBank.Domain.Abstractions.Database.Configurations;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        TableNameBuilder nameBuilder = new(typeof(TEntity));
        builder.ToTable(nameBuilder.Generate());
    }
}
