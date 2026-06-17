using Microsoft.EntityFrameworkCore;
using Myntra.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text;

namespace Myntra.DAL.Data
{
    public class MyntraDbContext:DbContext
    {
        public MyntraDbContext(DbContextOptions<MyntraDbContext> options)
           : base(options)
        { } 
       

        public DbSet<User> Users { get; set; }   
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUser(modelBuilder);

            ConfigureRefreshToken(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureUser(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Email)
                      .IsRequired()
                      .HasMaxLength(256);

                entity.HasIndex(x => x.Email)
                      .IsUnique();

                entity.Property(x => x.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(x => x.FirstName)
                      .HasMaxLength(100);

                entity.Property(x => x.LastName)
                      .HasMaxLength(100);

                entity.Property(x => x.Role)
                      .IsRequired();

                entity.HasQueryFilter(x => !x.IsDeleted);

                entity.HasMany(x => x.RefreshTokens)
                      .WithOne(x => x.User)
                      .HasForeignKey(x => x.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureRefreshToken(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Token)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.HasIndex(x => x.Token)
                      .IsUnique();

                entity.Property(x => x.ExpiresAt)
                      .IsRequired();
            });
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt =
                        DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt =
                        DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(
                cancellationToken);
        }


    }
}
