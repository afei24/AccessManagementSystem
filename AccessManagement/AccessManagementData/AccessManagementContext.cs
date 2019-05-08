using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementData
{
    public class AccessManagementContext: DbContext
    {
        public AccessManagementContext(DbContextOptions<AccessManagementContext> options)
       : base(options)
        {
        }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Function> Function { get; set; }
        public virtual DbSet<AppMenu> AppMenu { get; set; }
        public virtual DbSet<ReSetFunction> ReSetFunction { get; set; }
        public virtual DbSet<Account> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
