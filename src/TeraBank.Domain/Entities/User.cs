using Microsoft.AspNetCore.Identity;
using TeraBank.Domain.Abstractions.Database.Entities;

namespace TeraBank.Domain.Entities;

public class User : IdentityUser<Guid>, IEntity
{
    public virtual ICollection<BankAccount> Accounts { get; set; } = new HashSet<BankAccount>();
}
