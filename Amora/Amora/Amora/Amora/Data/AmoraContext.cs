using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Amora.Models;

namespace Amora.Data
{
    public class AmoraContext : DbContext
    {
        public AmoraContext (DbContextOptions<AmoraContext> options)
            : base(options)
        {
        }

        public DbSet<Amora.Models.RegisterViewModel> RegisterViewModel { get; set; } = default!;
        public DbSet<Amora.Models.Match> Match { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.MatchedUser)
                .WithMany()
                .HasForeignKey(m => m.MatchedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Amora.Models.MatchAccepted> MatchAccepted { get; set; } = default!;
        public DbSet<Amora.Models.Rejected> Rejected { get; set; } = default!;

    }
}
