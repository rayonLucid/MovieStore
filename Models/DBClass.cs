using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace MovieStore.Models
{
    public class DBClass:DbContext
    {
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stores>().ToTable("Stores", "dbo");
            modelBuilder.Entity<Movies>().ToTable("Movies", "dbo");
            modelBuilder.Entity<Customers>().ToTable("Customers", "dbo");
            modelBuilder.Entity<Countries>().ToTable("Countries", "dbo");
            
                base.OnModelCreating(modelBuilder);
        }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Customers> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite("Filename=MovieStores.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
