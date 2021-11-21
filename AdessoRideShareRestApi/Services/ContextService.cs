using AdessoRideShareRestApi.Infrastructure;
using AdessoRideShareRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareRestApi.Services
{
    public class ContextService : DbContext, IContextService
    {
        public ContextService(DbContextOptions<ContextService> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TravelModel> Travels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .OwnsOne(c => c.Travel)
                .WithOwner();

            modelBuilder.Entity<TravelModel>()
                .HasMany(t => t.Passengers)
                .WithOne();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
