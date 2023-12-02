using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RabbitMQDockerEntityExample.DAL;

public class ExampleDatabaseContext : DbContext
{
    private readonly string _connectionString;
    public ExampleDatabaseContext(Settings.Settings settings)
    {
        _connectionString = settings.ConnectionStrings.ExampleDb ?? throw new ArgumentException("No value provided as connection string.");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString);
    public required DbSet<Author> Authors { get; set; }
    public required DbSet<Article> Articles { get; set; }
    public required DbSet<Site> Sites { get; set; }
    public required DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasKey(model => model.Id);
        modelBuilder.Entity<Author>()
            .HasIndex(model => model.Name)
            .IsUnique();
        modelBuilder.Entity<Author>()
            .HasOne(model => model.Image)
            .WithOne()
            .HasForeignKey<Author>("ImageId");

        modelBuilder.Entity<Article>()
            .HasKey(model => model.Id);
        modelBuilder.Entity<Article>()
            .HasIndex(model => model.Title);
        modelBuilder.Entity<Article>()
            .HasMany(model => model.Author)
            .WithMany();
        modelBuilder.Entity<Article>()
            .HasOne(model => model.Site)
            .WithMany();
        modelBuilder.Entity<Site>().HasKey(model => model.Id);
        modelBuilder.Entity<Image>().HasKey(model => model.Id);
    }
}