using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.Core.Entities;

namespace Users.Core.Database;

    public class UsersEfContext : IdentityDbContext<User>
    {
        public UsersEfContext(DbContextOptions<UsersEfContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("user");
        }
    }
