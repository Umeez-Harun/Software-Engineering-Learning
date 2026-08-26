using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Product> products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("NEWSEQUENTIALID()");
                entity.HasIndex(e => e.seller_id);
                entity.HasIndex(e => e.sku).IsUnique();
                entity.Property(e => e.createdAt).HasDefaultValueSql("GETUTCDATE()");
                entity.HasIndex(e => e.isDeleted);
            });
        }
    }
}
