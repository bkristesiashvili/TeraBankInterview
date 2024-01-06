using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeraBank.Domain.Abstractions.Database.Configurations;

namespace TeraBank.Domain.Entities.Configurations;

public sealed class BankAccountEntityConfigurations : EntityConfiguration<BankAccount>
{
    public override void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.OwnerId).IsRequired();

        builder.Property(x => x.IBan)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(25);

        builder.HasIndex(x => x.IBan).IsUnique();

        builder.Property(x => x.Balance)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasDefaultValue(decimal.Zero)
            .ValueGeneratedOnAdd();

        builder.HasMany(x => x.Transactions)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
