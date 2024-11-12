using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class BackendContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Kanbanpost> Kanbanposts { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=kanbandb.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Kanbanpost>()
                .HasData(
                    new { Id = 1, Title = "Inbyggd", Message = "Data"}
                );
        

        AppUser user1 = new AppUser
        {
            Id = "c8a32ef7-46cf-4c01-988c-85feb76c7fd3",
            UserName = "felix@example.com",
            NormalizedUserName = "FELIX@EXAMPLE.COM",
            Email = "felix@example.com",
            NormalizedEmail = "FELIX@EXAMPLE.COM",
            EmailConfirmed = true,
            SecurityStamp = "initial_value"
        };

        user1.PasswordHash = new PasswordHasher<AppUser>().HashPassword(user1, "Abc123!");

        modelBuilder
            .Entity<AppUser>()
            .HasData(
                user1
            );

        base.OnModelCreating(modelBuilder);
    }

}

