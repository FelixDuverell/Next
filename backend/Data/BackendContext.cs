using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class BackendContext : DbContext
{
    public DbSet<Kanbanpost> Kanbanposts { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=kanbandb.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder
            // .Entity<Kanbanpost>();

        

        modelBuilder
            .Entity<AppUser>()
            .HasData(
                new { Id = 1, Username = "Felix", Password = "Abc123"}
            );

        base.OnModelCreating(modelBuilder);
    }

}

