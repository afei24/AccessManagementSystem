using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AccessManagementData
{
    public partial class AccessManagementContext : DbContext
    {
        public AccessManagementContext()
        {
        }

        public AccessManagementContext(DbContextOptions<AccessManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountFunction> AccountFunction { get; set; }
        public virtual DbSet<AccountRole> AccountRole { get; set; }
        public virtual DbSet<AppMenu> AppMenu { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Function> Function { get; set; }
        public virtual DbSet<FunctionRole> FunctionRole { get; set; }
        public virtual DbSet<ReSetFunction> ReSetFunction { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Branch");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Company");
            });

            modelBuilder.Entity<AccountFunction>(entity =>
            {
                //entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountFunction)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountFunction_Account");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.AccountFunction)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountFunction_Function");
            });

            modelBuilder.Entity<AccountRole>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountRole)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountRole_Account");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AccountRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountRole_Role");
            });

            modelBuilder.Entity<AppMenu>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Route).IsRequired();

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_AppMenu_AppMenu");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Branch)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branch_Company");

                entity.HasOne(d => d.ParentBranch)
                    .WithMany(p => p.InverseParentBranch)
                    .HasForeignKey(d => d.ParentBranchId)
                    .HasConstraintName("FK_Branch_Branch");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Function>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OpName1).HasMaxLength(50);

                entity.Property(e => e.OpName10).HasMaxLength(50);

                entity.Property(e => e.OpName2).HasMaxLength(50);

                entity.Property(e => e.OpName3).HasMaxLength(50);

                entity.Property(e => e.OpName4).HasMaxLength(50);

                entity.Property(e => e.OpName5).HasMaxLength(50);

                entity.Property(e => e.OpName6).HasMaxLength(50);

                entity.Property(e => e.OpName7).HasMaxLength(50);

                entity.Property(e => e.OpName8).HasMaxLength(50);

                entity.Property(e => e.OpName9).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Function)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Function_Company");
            });

            modelBuilder.Entity<FunctionRole>(entity =>
            {
                entity.HasOne(d => d.Function)
                    .WithMany(p => p.FunctionRole)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FunctionRole_Function");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.FunctionRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FunctionRole_Role");
            });

            modelBuilder.Entity<ReSetFunction>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OpName1).HasMaxLength(50);

                entity.Property(e => e.OpName10).HasMaxLength(50);

                entity.Property(e => e.OpName2).HasMaxLength(50);

                entity.Property(e => e.OpName3).HasMaxLength(50);

                entity.Property(e => e.OpName4).HasMaxLength(50);

                entity.Property(e => e.OpName5).HasMaxLength(50);

                entity.Property(e => e.OpName6).HasMaxLength(50);

                entity.Property(e => e.OpName7).HasMaxLength(50);

                entity.Property(e => e.OpName8).HasMaxLength(50);

                entity.Property(e => e.OpName9).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Company");
            });
        }
    }
}
