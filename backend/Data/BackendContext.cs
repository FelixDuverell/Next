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

        IdentityRole adminRole = new("admin")
        {
            NormalizedName = "ADMIN"
        };
        IdentityRole userRole = new("user")
        {
            NormalizedName = "USER"
        };
        modelBuilder
            .Entity<IdentityRole>()
            .HasData(adminRole, userRole);
        

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

         AppUser user2 = new AppUser
        {
            Id = "c8a32ef7-46cf-4c01-988c-85feb76c7abc",
            UserName = "Tina@example.com",
            NormalizedUserName = "TINA@EXAMPLE.COM",
            Email = "Tina@example.com",
            NormalizedEmail = "TINA@EXAMPLE.COM",
            EmailConfirmed = true,
            SecurityStamp = "initial_value"
        };

        user1.PasswordHash = new PasswordHasher<AppUser>().HashPassword(user2, "Abc123!");


        modelBuilder
            .Entity<AppUser>()
            .HasData(
                user1, user2
            );

            modelBuilder
                .Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = adminRole.Id,
                        UserId = user1.Id
                    }
                );

            modelBuilder
            .Entity<Kanbanpost>()
                .HasData(
                    // new Kanbanpost("Hello World", "Mimi". user1.Id ) { Id = 1 },
                );

        base.OnModelCreating(modelBuilder);
    }

}

