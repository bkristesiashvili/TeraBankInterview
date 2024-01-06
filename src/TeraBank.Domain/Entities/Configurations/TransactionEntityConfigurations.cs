using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TeraBank.Domain.Abstractions.Database.Configurations;
using TeraBank.Domain.Entities.Enums;
using TeraBank.Domain.Extensions;

namespace TeraBank.Domain.Entities.Configurations;

public sealed class TransactionEntityConfigurations : EntityConfiguration<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.AccountId).IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(TransactionStatuses.Pending)
            .ValueGeneratedOnAdd()
            .HasEnumToStringConversion();

        builder.Property(x => x.Type)
            .IsRequired()
            .HasDefaultValue(TransactionTypes.Deposit)
            .ValueGeneratedOnAdd()
            .HasEnumToStringConversion();

        builder.HasOne(x => x.SenderTransaction)
            .WithOne()
            .HasForeignKey<Transaction>(x => x.SenderTransactionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
