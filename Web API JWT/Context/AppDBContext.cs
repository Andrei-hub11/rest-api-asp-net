using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Web_API_JWT.Models;
using Web_API_JWT.Movies.Contracts;

namespace Web_API_JWT.Context;

public class AppDBContext: DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }
    public DbSet<MovieModel> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieModel>()
            .Property(m => m.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<MovieModel>()
            .Property(m => m.UpdatedAt)
            .HasComputedColumnSql("GETDATE()");

        modelBuilder.Entity<MovieModel>().Property(m => m.Name)
          .IsRequired()
          .HasMaxLength(30)
          .HasColumnName("Name")
          .HasMaxLength(30)
          .IsRequired()
          .HasColumnName("Name")
          .HasMaxLength(30);     

        modelBuilder.Entity<MovieModel>().Property(m => m.Director)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnName("Director")
            .HasMaxLength(30);

        modelBuilder.Entity<MovieModel>().Property(m => m.Category)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("Category")
            .HasMaxLength(20);

        modelBuilder.Entity<MovieModel>()
            .Property(m => m.Year)
            .HasColumnName("Year")
            .IsRequired();
        
        modelBuilder.Entity<MovieModel>().
            ToTable(movie => movie
            .HasCheckConstraint("CK_YourEntity_YearRange", "Year >= 1900 AND Year <= GETDATE()"));


       

        base.OnModelCreating(modelBuilder);
    }

}
