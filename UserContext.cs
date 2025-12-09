using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace LabSQL
{
    public class UserContext : DbContext
    {
            public DbSet<User> Users{ get; set; }  

        public UserContext() { }

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // connection string från appsettings.json 
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())     
                    .AddJsonFile("appsettings.json",                 
                                 optional: false,               
                                 reloadOnChange: true)                
                    .Build();

             
                var connectionString = config.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);    
          
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");       
        }
    }
}