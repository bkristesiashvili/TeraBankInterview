using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Entities;

namespace TeraBank.Infrastructure.Database;

public sealed class BankDbContext(DbContextOptions<BankDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options), IBankDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
    }
}
