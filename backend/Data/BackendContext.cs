using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class BackendContext : DbContext
{
    public DbSet<Kanbanpost> Kanbanposts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=kanbandb.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Kanbanpost>();

        base.OnModelCreating(modelBuilder);
    }
}

// Kolla 2.3 video