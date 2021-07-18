using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TenderingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Tender> Tender { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasIndex(c => c.CategoryId)
                .IsUnique();

            builder.Entity<Category>().HasAlternateKey(c => c.CategoryId).HasName("AlternateKey_CategoryId");

            builder.Entity<SubCategory>()
                .HasIndex(s => s.SubCategoryName)
                .IsUnique();

            builder.Entity<Organization>()
                .HasIndex(o => o.OrganizationId)
                .IsUnique();

            builder.Entity<Organization>().HasAlternateKey(o => o.OrganizationId).HasName("AlternateKey_OrganizationId");

            //builder.Entity<Category>().HasMany(c => c.SubCategories).WithRequired(s => s.Category).HasForeignKey(s => s.Category).WillCascadeOnDelete(true);
            base.OnModelCreating(builder);
        }


    }
}
