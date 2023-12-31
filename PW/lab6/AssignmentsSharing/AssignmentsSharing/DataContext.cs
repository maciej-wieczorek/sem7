﻿using AssignmentsSharing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssignmentsSharing
{
    public class DataContext : IdentityDbContext<Developer, IdentityRole<Guid>, Guid>
	{
        private IConfiguration _configuration;
        public DataContext(IConfiguration configuration) { _configuration = configuration; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));
        }

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Developer> Developers { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
    }
}
