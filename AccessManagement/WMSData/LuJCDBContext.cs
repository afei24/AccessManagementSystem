using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WMSData
{
    public partial class LuJCDBContext : DbContext
    {
        public LuJCDBContext()
        {
        }

        public LuJCDBContext(DbContextOptions<LuJCDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BadReport> BadReport { get; set; }
        public virtual DbSet<BadReportDetail> BadReportDetail { get; set; }
        public virtual DbSet<CheckData> CheckData { get; set; }
        public virtual DbSet<CheckStock> CheckStock { get; set; }
        public virtual DbSet<CheckStockInfo> CheckStockInfo { get; set; }
        public virtual DbSet<CloneHistory> CloneHistory { get; set; }
        public virtual DbSet<CloneTemp> CloneTemp { get; set; }
        public virtual DbSet<CusAddress> CusAddress { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<InStorDetail> InStorDetail { get; set; }
        public virtual DbSet<InStorUploadDetail> InStorUploadDetail { get; set; }
        public virtual DbSet<InStorage> InStorage { get; set; }
        public virtual DbSet<InventoryBook> InventoryBook { get; set; }
        public virtual DbSet<LocalProduct> LocalProduct { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Measure> Measure { get; set; }
        public virtual DbSet<MeasureRel> MeasureRel { get; set; }
        public virtual DbSet<MoveOrder> MoveOrder { get; set; }
        public virtual DbSet<MoveOrderDetail> MoveOrderDetail { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OutStoDetail> OutStoDetail { get; set; }
        public virtual DbSet<OutStorUploadDetail> OutStorUploadDetail { get; set; }
        public virtual DbSet<OutStorage> OutStorage { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ReportParams> ReportParams { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<ReturnDetail> ReturnDetail { get; set; }
        public virtual DbSet<ReturnOrder> ReturnOrder { get; set; }
        public virtual DbSet<Sequence> Sequence { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SysDepart> SysDepart { get; set; }
        public virtual DbSet<SysRelation> SysRelation { get; set; }
        public virtual DbSet<SysResource> SysResource { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<Tnum> Tnum { get; set; }

        // Unable to generate entity type for table 'dbo.sheet'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=DESKTOP-QUO241A\\SQLEXPRESS;initial catalog=LuJCDB;user id=sa;password=*asdf*1234;MultipleActiveResultSets=True;App=EntityFramework");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            //modelBuilder.Entity<Admin>(entity =>
            //{
            //    entity.HasIndex(e => e.UserCode)
            //        .IsUnique();

            //    entity.Property(e => e.Id).HasColumnName("ID");

            //    entity.Property(e => e.CreateIp)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CreateTime).HasColumnType("datetime");

            //    entity.Property(e => e.CreateUser).HasMaxLength(15);

            //    entity.Property(e => e.DepartNum)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Email)
            //        .HasMaxLength(30)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Mobile)
            //        .HasMaxLength(11)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ParentCode)
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.PassWord)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Phone)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Picture).HasMaxLength(30);

            //    entity.Property(e => e.RealName).HasMaxLength(20);

            //    entity.Property(e => e.Remark).HasMaxLength(20);

            //    entity.Property(e => e.RoleNum)
            //        .IsRequired()
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.UpdateTime).HasColumnType("datetime");

            //    entity.Property(e => e.UserCode)
            //        .IsRequired()
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.UserName)
            //        .IsRequired()
            //        .HasMaxLength(20)
            //        .IsUnicode(false);
            //});

            modelBuilder.Entity<BadReport>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuditUser).HasMaxLength(20);

                entity.Property(e => e.AuditeTime).HasColumnType("datetime");

                entity.Property(e => e.ContractOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(20);

                entity.Property(e => e.EquipmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrintTime).HasColumnType("datetime");

                entity.Property(e => e.PrintUser).HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BadReportDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.FromLocalNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ToLocalNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CheckData>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.FirstUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocalName).HasMaxLength(50);

                entity.Property(e => e.LocalNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.ProductNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CheckStock>(entity =>
            {
                entity.HasIndex(e => e.OrderNum)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuditUser).HasMaxLength(20);

                entity.Property(e => e.AuditeTime).HasColumnType("datetime");

                entity.Property(e => e.ContractOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EquipmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrintTime).HasColumnType("datetime");

                entity.Property(e => e.PrintUser).HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CheckStockInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TargetNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CloneHistory>(entity =>
            {
                entity.HasKey(e => e.CloneId)
                    .HasName("PK__CloneHis__C8F2EB11E3F74608");

                entity.Property(e => e.CloneId).HasColumnName("CloneID");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.CreateName).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LocalName).HasMaxLength(50);

                entity.Property(e => e.LocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(200);

                entity.Property(e => e.ProductNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Sn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageName).HasMaxLength(50);

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CloneTemp>(entity =>
            {
                entity.HasKey(e => e.CloneId)
                    .HasName("PK__CloneTem__C8F2EB11748E6242");

                entity.Property(e => e.CloneId).HasColumnName("CloneID");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.CreateName).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LocalName).HasMaxLength(50);

                entity.Property(e => e.LocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(200);

                entity.Property(e => e.ProductNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Sn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageName).HasMaxLength(50);

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CusAddress>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Contact).HasMaxLength(200);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CusNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CusName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CusNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EquipmentNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Flag).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InStorDetail>(entity =>
            {
                entity.HasIndex(e => e.SnNum)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.Consignee)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContractOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ExceptionHandling)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpTime).HasColumnType("datetime");

                entity.Property(e => e.OpUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductEffect).HasColumnType("datetime");

                entity.Property(e => e.ProductModel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductSpecification)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiptDate).HasColumnType("datetime");

                entity.Property(e => e.ReceivesException)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InStorUploadDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.LocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<InStorage>(entity =>
            {
                entity.HasIndex(e => e.OrderNum)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.AuditUser).HasMaxLength(50);

                entity.Property(e => e.AuditeTime).HasColumnType("datetime");

                entity.Property(e => e.ContactName).HasMaxLength(100);

                entity.Property(e => e.ContractOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.EquipmentCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MergeOrderNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderTime).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrintTime).HasColumnType("datetime");

                entity.Property(e => e.PrintUser).HasMaxLength(50);

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreKeeper).HasMaxLength(50);

                entity.Property(e => e.SupName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SupNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InventoryBook>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.ContactOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FromLocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.ProductNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ToLocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LocalProduct>(entity =>
            {
                entity.HasIndex(e => e.Sn)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Batch)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.CreateName).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocalName).HasMaxLength(50);

                entity.Property(e => e.LocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(200);

                entity.Property(e => e.ProductNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Sn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageName).HasMaxLength(50);

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SupNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValidityDateTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.LocalBarCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LocalName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LocalNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Rack).HasMaxLength(100);

                entity.Property(e => e.Remark).HasMaxLength(4000);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UnitNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Measure>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MeasureName).HasMaxLength(50);

                entity.Property(e => e.MeasureNum).HasMaxLength(50);

                entity.Property(e => e.Sn)
                    .IsRequired()
                    .HasColumnName("SN")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MeasureRel>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MeasureSource).HasMaxLength(50);

                entity.Property(e => e.MeasureTarget).HasMaxLength(50);

                entity.Property(e => e.Sn)
                    .IsRequired()
                    .HasColumnName("SN")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MoveOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuditUser).HasMaxLength(20);

                entity.Property(e => e.AuditeTime).HasColumnType("datetime");

                entity.Property(e => e.ContractOrder)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(20);

                entity.Property(e => e.EquipmentCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrintTime).HasColumnType("datetime");

                entity.Property(e => e.PrintUser).HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MoveOrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.FromLocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ToLocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContractId)
                    .IsRequired()
                    .HasColumnName("ContractID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.ContractOrder)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.CusName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CusNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderTime).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OutStoDetail>(entity =>
            {
                entity.HasIndex(e => e.SnNum)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.ContractOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContractSn)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.LocalNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpTime).HasColumnType("datetime");

                entity.Property(e => e.OpUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OutStorUploadDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Batch)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.LocalNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OutStorage>(entity =>
            {
                entity.HasIndex(e => e.OrderNum)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.AuditUser).HasMaxLength(50);

                entity.Property(e => e.AuditeTime).HasColumnType("datetime");

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.ContractOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CusName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CusNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MergeOrderNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrintTime).HasColumnType("datetime");

                entity.Property(e => e.PrintUser).HasMaxLength(50);

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SupNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.BarCode)
                    .IsUnique();

                entity.HasIndex(e => e.SnNum)
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Batch)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CateName).HasMaxLength(50);

                entity.Property(e => e.CateNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Color).HasMaxLength(200);

                entity.Property(e => e.CommodityCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(20);

                entity.Property(e => e.CusName).HasMaxLength(30);

                entity.Property(e => e.CusNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultLocal).HasMaxLength(20);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Display).HasMaxLength(50);

                entity.Property(e => e.PicUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.Remark).HasColumnType("ntext");

                entity.Property(e => e.Size).HasMaxLength(400);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sx1).HasMaxLength(50);

                entity.Property(e => e.Sx2).HasMaxLength(50);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValidityDays)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Volume)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.VolumeSize)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CateName).HasMaxLength(50);

                entity.Property(e => e.CateNum)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<ReportParams>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DefaultValue)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InputNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParamData)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ParamElement)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ParamName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParamNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParamType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.ReportNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShowName).HasMaxLength(50);
            });

            modelBuilder.Entity<Reports>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DataSource).HasMaxLength(4000);

                entity.Property(e => e.FileName).HasMaxLength(200);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.ReportName).HasMaxLength(50);

                entity.Property(e => e.ReportNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReturnDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNum).HasMaxLength(50);

                entity.Property(e => e.ContractOrder)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.LocalNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SnNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReturnOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.AuditUser).HasMaxLength(50);

                entity.Property(e => e.AuditeTime).HasColumnType("datetime");

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.ContractOrder)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.CusName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CusNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrintTime).HasColumnType("datetime");

                entity.Property(e => e.PrintUser).HasMaxLength(50);

                entity.Property(e => e.Reason).HasMaxLength(400);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sequence>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentValue).HasMaxLength(100);

                entity.Property(e => e.FirstRule).HasMaxLength(100);

                entity.Property(e => e.FourRule).HasMaxLength(100);

                entity.Property(e => e.JoinChar)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Sample).HasMaxLength(100);

                entity.Property(e => e.SecondRule).HasMaxLength(100);

                entity.Property(e => e.Sn)
                    .HasColumnName("SN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TabName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ThirdRule).HasMaxLength(100);
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action).HasMaxLength(200);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.StorageName).HasMaxLength(50);

                entity.Property(e => e.StorageNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.ContactName).HasMaxLength(20);

                entity.Property(e => e.ContractNum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SupNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysDepart>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DepartName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.DepartNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ParentNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysRelation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ResNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysResource>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateIp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(20);

                entity.Property(e => e.CssName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParentNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ParentPath)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.ResName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ResNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateIp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser).HasMaxLength(20);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RoleNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tnum>(entity =>
            {
                entity.ToTable("TNum");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Day)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TabName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
