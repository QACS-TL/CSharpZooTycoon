using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CSharpZooTycoonLibrary
{
    public class ZooContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasDiscriminator<string>("AnimalType")
                .HasValue<Dog>("Dog")
                .HasValue<Cat>("Cat")
                .HasValue<Bird>("Bird");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=(local);Initial Catalog=ZooTycoon;trusted_connection=true;Encrypt=False");
        }
    }
}
