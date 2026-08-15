using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options): base(options) { }

        public DbSet<Employee> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>(entity =>
            {
                entity.Property(p => p.id).HasDefaultValueSql("NEWSEQUENTIALID()");
                entity.HasIndex(e => e.identificationNo).IsUnique();
                entity.HasOne<ApplicationUser>().WithOne().HasForeignKey<Employee>(e => e.ApplicationUserId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
