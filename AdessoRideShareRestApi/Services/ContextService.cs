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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .OwnsOne(c => c.Travel)
                .WithOwner();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
