using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameWORLD.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GameWORLD.Data
{
    public class GameWORLDContext : IdentityDbContext<IdentityUser>
    {
        public GameWORLDContext(DbContextOptions<GameWORLDContext> options)
            : base(options)
        {
        }

        public DbSet<GameWORLD.Models.Customer> Customer { get; set; } = default!;

        public DbSet<GameWORLD.Models.Game> Game { get; set; } = default!;

        public DbSet<GameWORLD.Models.GameGenre> GameGenre { get; set; } = default!;

        public DbSet<GameWORLD.Models.GameGameGenre> GameGameGenre { get; set; } = default!;

        public DbSet<GameWORLD.Models.PublishingCompany> PublishingCompany { get; set; } = default!;

        public DbSet<GameWORLD.Models.Rating> Rating { get; set; } = default!;

        public DbSet<GameWORLD.Models.SoftwareDeveloper> SoftwareDeveloper { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });
        }

        
    }
}