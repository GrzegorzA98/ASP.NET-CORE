﻿using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities {

    public class RestaurantDbContext : DbContext {

        private string connectionString = "Server=(localdb)\\mssqllocaldb;Database=RestaurantDb;Trusted_Connection=True;";

        public DbSet<Restaurant> Restaurants {

            get;
            set;

        }

        public DbSet<Address> Addresses {

            get;
            set;

        }

        public DbSet<Dish> Dishes {

            get;
            set;

        }

        public DbSet<User> Users {

            get;
            set;

        }

        public DbSet<Role> Roles {

            get;
            set;

        }

        override protected void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.ContactNumber)
                .IsRequired(false);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            optionsBuilder.UseSqlServer(connectionString);

        }

    }

}
