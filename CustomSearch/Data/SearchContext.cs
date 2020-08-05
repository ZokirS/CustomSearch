using CustomSearch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomSearch.Data
{
    public class SearchContext : DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Google> GoogleResults { get; set; }
        public DbSet<Bing> BingResults { get; set; }
        public DbSet<Yandex> YendexResults { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;initial catalog=CustomSearch;integrated security=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Google>(entity =>
            {
                entity.HasKey("Id");
            });
            modelBuilder.Entity<Bing>(entity =>
            {
                entity.HasKey("Id");
            });
            modelBuilder.Entity<Yandex>(entity =>
            {
                entity.HasKey("Id");
            });
        }
    }

    
}
