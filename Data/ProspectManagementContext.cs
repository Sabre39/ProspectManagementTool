using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProspectManagementTool.Models;
using ProspectManagementTool.ViewModels;

namespace ProspectManagementTool.Data
{
    public class ProspectManagementContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string UserName
        {
            get; private set;
        }

        public ProspectManagementContext(DbContextOptions<ProspectManagementContext> options)
            : base(options)
        {
            UserName = "SeedData";
        }

        public ProspectManagementContext(DbContextOptions<ProspectManagementContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            UserName = _httpContextAccessor.HttpContext?.User.Identity.Name;
            UserName = UserName ?? "Unknown";
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Prospect> Prospects { get; set; }
        public DbSet<Models.Attribute> Attributes { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<ProspectManagementTool.ViewModels.TeamProspectVM> TeamProspectVM { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PM");

            modelBuilder.Entity<Prospect>()
                .HasIndex(p => new { p.ProspectName, p.ProspectAge, p.ProspectPosition }).IsUnique();

            modelBuilder.Entity<Team>()
                .HasMany<Prospect>(p => p.Prospects)
                .WithOne(t => t.Team)
                .HasForeignKey(t => t.TeamID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Attribute>()
                .HasMany<Prospect>(p => p.Prospects)
                .WithOne(a => a.Attribute)
                .HasForeignKey(a => a.AttributeID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}