using System;
using System.IO;
using IntegratedAppraisalControl.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class IntegratedAppraisalControlContext : DbContext
    {
        public IntegratedAppraisalControlContext()
        {
        }

        public IntegratedAppraisalControlContext(DbContextOptions<IntegratedAppraisalControlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAnnualDepreciation> TblAnnualDepreciation { get; set; }
        public virtual DbSet<TblAssetClass> TblAssetClass { get; set; }
        public virtual DbSet<TblBuildingAdditionalData> TblBuildingAdditionalData { get; set; }
        public virtual DbSet<TblBuildingChanges> TblBuildingChanges { get; set; }
        public virtual DbSet<TblBuildingImages> TblBuildingImages { get; set; }
        public virtual DbSet<TblBuildingMaintenance> TblBuildingMaintenance { get; set; }
        public virtual DbSet<TblBuildings> TblBuildings { get; set; }
        public virtual DbSet<TblBuildingSectionPricing> TblBuildingSectionPricing { get; set; }
        public virtual DbSet<TblChangeType> TblChangeType { get; set; }
        public virtual DbSet<TblClientAttachments> TblClientAttachments { get; set; }
        public virtual DbSet<TblClients> TblClients { get; set; }
        public virtual DbSet<TblClientStatus> TblClientStatus { get; set; }
        public virtual DbSet<TblDepartments> TblDepartments { get; set; }
        public virtual DbSet<TblFirstYearDepreciation> TblFirstYearDepreciation { get; set; }
        public virtual DbSet<TblInventory> TblInventory { get; set; }
        public virtual DbSet<TblInventoryMaintenance> TblInventoryMaintenance { get; set; }
        public virtual DbSet<TblRooms> TblRooms { get; set; }
        public virtual DbSet<TblUsers> TblUsers { get; set; }
        public virtual DbSet<TblUsersClients> TblUsersClients { get; set; }

        // Unable to generate entity type for table 'dbo.tblTransactions'. Please see the warning messages.




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    IConfigurationRoot configuration = new ConfigurationBuilder()
            //       .SetBasePath(Directory.GetCurrentDirectory())
            //       .AddJsonFile("appsettings.json")
            //       .Build();
            //    var connectionString = configuration["ConnectionStrings:myConString"];
            //    optionsBuilder.UseSqlServer(connectionString);
            //}

            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                //optionsBuilder.UseSqlServer("Server=192.168.0.24\\MSSQLSERVER2017,2017;Database=IntegratedAppraisalControl;User Id=sa; Password=sys@123;");
                //optionsBuilder.UseSqlServer("Server=iacsql\\iacsql;Database=testreport;User Id=sa; Password=?5monkeys?;");
                optionsBuilder.UseSqlServer("Server=192.168.2.154\\MSSQLSERVER2022,1433; Database=VFACS; User Id=sa; Password=sys@123; TrustServerCertificate=True;");
#else
                optionsBuilder.UseSqlServer("Server=iacsql\\iacsql;Database=vfacs;User Id=sa; Password=?5monkeys?;");
#endif

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAnnualDepreciation>(entity =>
            {
                entity.HasKey(e => e.AnnualDepreciationId);

                entity.ToTable("tblAnnualDepreciation");

                entity.Property(e => e.AnnualDepreciationId).HasColumnName("AnnualDepreciationID");

                entity.Property(e => e.AnnualDepreciationText).HasMaxLength(25);
            });

            modelBuilder.Entity<TblAssetClass>(entity =>
            {
                entity.HasKey(e => e.AssetClassId);

                entity.ToTable("tblAssetClass");

                entity.Property(e => e.AssetClassId).HasColumnName("AssetClassID");

                entity.Property(e => e.AssetClassName).HasMaxLength(50);

                entity.Property(e => e.ClassCode).HasMaxLength(3);

                entity.Property(e => e.ClientId).HasColumnName("ClientID");
            });

            modelBuilder.Entity<TblBuildingAdditionalData>(entity =>
            {
                entity.HasKey(e => e.BuildingAdditionalDataId);

                entity.ToTable("tblBuildingAdditionalData");

                entity.Property(e => e.BuildingAdditionalDataId).HasColumnName("BuildingAdditionalDataID");

                entity.Property(e => e.AdditionalDataName).HasMaxLength(300);

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.TblBuildingAdditionalData)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBuildingAdditionalData_tblBuildings");
            });

            modelBuilder.Entity<TblBuildingChanges>(entity =>
            {
                entity.HasKey(e => e.BuildingChangesId);

                entity.ToTable("tblBuildingChanges");

                entity.Property(e => e.BuildingChangesId).HasColumnName("BuildingChangesID");

                entity.Property(e => e.AdateTime)
                    .HasColumnName("ADateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Auser).HasColumnName("AUser");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.CdateTime)
                    .HasColumnName("CDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChangeTypeId).HasColumnName("ChangeTypeID");

                entity.Property(e => e.Cuser).HasColumnName("CUser");

                entity.Property(e => e.Description).HasMaxLength(200);
            });

            modelBuilder.Entity<TblBuildingImages>(entity =>
            {
                entity.HasKey(e => e.BuildingImageId);

                entity.ToTable("tblBuildingImages");

                entity.Property(e => e.BuildingImageId).HasColumnName("BuildingImageID");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.ImageDescription).HasMaxLength(300);

                entity.Property(e => e.ImageName).HasMaxLength(300);

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.TblBuildingImages)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBuildingImages_tblBuildings");
            });

            modelBuilder.Entity<TblBuildingMaintenance>(entity =>
            {
                entity.HasKey(e => e.BuildingMaintenanceId);

                entity.ToTable("tblBuildingMaintenance");

                entity.Property(e => e.BuildingMaintenanceId).HasColumnName("BuildingMaintenanceID");

                entity.Property(e => e.AdateTime)
                    .HasColumnName("ADateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Auser).HasColumnName("AUser");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.CdateTime)
                    .HasColumnName("CDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChangeTypeId).HasColumnName("ChangeTypeID");

                entity.Property(e => e.Cuser).HasColumnName("CUser");

                entity.Property(e => e.Description).HasMaxLength(200);
            });

            modelBuilder.Entity<TblBuildings>(entity =>
            {
                entity.HasKey(e => e.BuildingId);

                entity.ToTable("tblBuildings");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.AdditionalFeatures).HasMaxLength(2000);

                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.BasementSqft)
                    .HasColumnName("BasementSQFT")
                    .HasMaxLength(25);

                entity.Property(e => e.BuildingCode).HasMaxLength(5);

                entity.Property(e => e.BuildingCrn)
                    .HasColumnName("BuildingCRN")
                    .HasMaxLength(25);

                entity.Property(e => e.BuildingName).HasMaxLength(75);

                entity.Property(e => e.Capacity).HasMaxLength(25);

                entity.Property(e => e.CapacitySize).HasMaxLength(25);

                entity.Property(e => e.CapacityType).HasMaxLength(25);

                entity.Property(e => e.CentralFireAlarm).HasMaxLength(4);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Condition).HasMaxLength(75);

                entity.Property(e => e.ConstructionClass).HasMaxLength(75);

                entity.Property(e => e.ConstructionType).HasMaxLength(50);

                entity.Property(e => e.ContentsCrn)
                    .HasColumnName("ContentsCRN")
                    .HasMaxLength(25);

                entity.Property(e => e.ContractNo).HasMaxLength(20);

                entity.Property(e => e.Coolling).HasMaxLength(75);

                entity.Property(e => e.Cost).HasMaxLength(25);

                entity.Property(e => e.EffectiveDate).HasMaxLength(20);

                entity.Property(e => e.EmergencyGenerator).HasMaxLength(4);

                entity.Property(e => e.ExteriorWallType).HasMaxLength(75);

                entity.Property(e => e.FireProctectionClass).HasMaxLength(4);

                entity.Property(e => e.FloodZone).HasMaxLength(5);

                entity.Property(e => e.FreightElevator).HasMaxLength(4);

                entity.Property(e => e.Gps)
                    .HasColumnName("GPS")
                    .HasMaxLength(50);

                entity.Property(e => e.Gpslat)
                    .HasColumnName("GPSLAT")
                    .HasMaxLength(20);

                entity.Property(e => e.Gpslong)
                    .HasColumnName("GPSLong")
                    .HasMaxLength(20);

                entity.Property(e => e.GrandTotal).HasMaxLength(25);

                entity.Property(e => e.Heating).HasMaxLength(200);

                entity.Property(e => e.Leased).HasMaxLength(1);

                entity.Property(e => e.LocalFireAlarm).HasMaxLength(4);

                entity.Property(e => e.MonthAcq).HasMaxLength(25);

                entity.Property(e => e.Occupancy).HasMaxLength(25);

                entity.Property(e => e.PassengerElevator).HasMaxLength(4);

                entity.Property(e => e.Pito)
                    .HasColumnName("PITO")
                    .HasMaxLength(25);

                entity.Property(e => e.RevaluationNo).HasMaxLength(20);

                entity.Property(e => e.RoofMaterial).HasMaxLength(75);

                entity.Property(e => e.Sdf1)
                    .HasColumnName("SDF1")
                    .HasMaxLength(100);

                entity.Property(e => e.Sdf1label)
                    .HasColumnName("SDF1Label")
                    .HasMaxLength(50);

                entity.Property(e => e.Sdf2)
                    .HasColumnName("SDF2")
                    .HasMaxLength(50);

                entity.Property(e => e.Sdf2label)
                    .HasColumnName("SDF2Label")
                    .HasMaxLength(50);

                entity.Property(e => e.SecuritySystem).HasMaxLength(4);

                entity.Property(e => e.Sprinklers).HasMaxLength(4);

                entity.Property(e => e.StainedGlass).HasMaxLength(25);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Stories).HasMaxLength(10);

                entity.Property(e => e.SuperSqft)
                    .HasColumnName("SuperSQFT")
                    .HasMaxLength(25);

                entity.Property(e => e.YearAcq).HasMaxLength(25);

                entity.Property(e => e.YearsBuilt).HasMaxLength(25);

                entity.Property(e => e.ZipCode).HasMaxLength(10);
            });

            modelBuilder.Entity<TblBuildingSectionPricing>(entity =>
            {
                entity.HasKey(e => e.BuildingSectionId);

                entity.ToTable("tblBuildingSectionPricing");

                entity.Property(e => e.BuildingSectionId).HasColumnName("BuildingSectionID");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.Isoclass).HasColumnName("ISOClass");

                entity.Property(e => e.SectionDesc).HasMaxLength(50);

                entity.Property(e => e.Yracq).HasColumnName("YRACQ");
            });

            modelBuilder.Entity<TblChangeType>(entity =>
            {
                entity.HasKey(e => e.ChangeTypeId);

                entity.ToTable("tblChangeType");

                entity.Property(e => e.ChangeTypeId).HasColumnName("ChangeTypeID");

                entity.Property(e => e.ChangeType).HasMaxLength(50);
            });

            modelBuilder.Entity<TblClientAttachments>(entity =>
            {
                entity.HasKey(e => e.ClientAttachmentId);

                entity.ToTable("tblClientAttachments");

                entity.Property(e => e.ClientAttachmentId).HasColumnName("ClientAttachmentID");

                entity.Property(e => e.AttachmentDescription).HasMaxLength(300);

                entity.Property(e => e.AttachmentName).HasMaxLength(300);

                entity.Property(e => e.ClientId).HasColumnName("ClientID");
            });

            modelBuilder.Entity<TblClients>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.ToTable("tblClients");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.AccountDataAsOf).HasColumnType("datetime");

                entity.Property(e => e.AccountingYear).HasMaxLength(4);

                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.AnnualDepreciationId).HasColumnName("AnnualDepreciationID");

                entity.Property(e => e.AppraisalDate).HasMaxLength(10);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.ClientName).HasMaxLength(75);

                entity.Property(e => e.ClientStatusId).HasColumnName("ClientStatusID");

                entity.Property(e => e.FileNo).HasMaxLength(20);

                entity.Property(e => e.Fmv).HasColumnName("FMV");

                entity.Property(e => e.LastUpdated).HasMaxLength(10);

                entity.Property(e => e.PointOfContact).HasMaxLength(50);

                entity.Property(e => e.ReportYear).HasMaxLength(4);

                entity.Property(e => e.Sdf1label)
                    .HasColumnName("SDF1Label")
                    .HasMaxLength(20);

                entity.Property(e => e.Sdf2label)
                    .HasColumnName("SDF2Label")
                    .HasMaxLength(20);

                entity.Property(e => e.Sdf3label)
                    .HasColumnName("SDF3Label")
                    .HasMaxLength(20);

                entity.Property(e => e.Sdf4label)
                    .HasColumnName("SDF4Label")
                    .HasMaxLength(20);

                entity.Property(e => e.State).HasMaxLength(10);

                entity.Property(e => e.Telephone).HasMaxLength(20);

                entity.Property(e => e.UpdatedTo).HasMaxLength(10);

                entity.Property(e => e.ZipCode).HasMaxLength(75);
            });

            modelBuilder.Entity<TblClientStatus>(entity =>
            {
                entity.HasKey(e => e.ClientStatusId);

                entity.ToTable("tblClientStatus");

                entity.Property(e => e.ClientStatusId).HasColumnName("ClientStatusID");

                entity.Property(e => e.ClientStatusText).HasMaxLength(50);
            });

            modelBuilder.Entity<TblDepartments>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("tblDepartments");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.DepartmentCode).HasMaxLength(10);

                entity.Property(e => e.DepartmentName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblFirstYearDepreciation>(entity =>
            {
                entity.HasKey(e => e.FirstYearDepreciationId);

                entity.ToTable("tblFirstYearDepreciation");

                entity.Property(e => e.FirstYearDepreciationId).HasColumnName("FirstYearDepreciationID");

                entity.Property(e => e.FirstYearDepreciationText).HasMaxLength(25);
            });

            modelBuilder.Entity<TblInventory>(entity =>
            {
                entity.HasKey(e => e.InventoryId);

                entity.ToTable("tblInventory");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.AdateTime)
                    .HasColumnName("ADateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.AssetClassId).HasColumnName("AssetClassID");

                entity.Property(e => e.Auser).HasColumnName("AUser");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.CdateTime)
                    .HasColumnName("CDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.Crn).HasColumnName("CRN");

                entity.Property(e => e.Cuser).HasColumnName("CUser");

                entity.Property(e => e.DdateTime)
                    .HasColumnName("DDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.Descr).HasMaxLength(2000);

                entity.Property(e => e.Duser).HasColumnName("DUser");

                entity.Property(e => e.Floor).HasMaxLength(20);

                entity.Property(e => e.Funct).HasMaxLength(5);

                entity.Property(e => e.Fund).HasMaxLength(5);

                entity.Property(e => e.ImageFileName).HasMaxLength(100);

                entity.Property(e => e.Mfg).HasMaxLength(10);

                entity.Property(e => e.Model).HasMaxLength(10);

                entity.Property(e => e.Month).HasMaxLength(2);

                entity.Property(e => e.Pono)
                    .HasColumnName("PONo")
                    .HasMaxLength(25);

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.Sdf1)
                    .HasColumnName("SDF1")
                    .HasMaxLength(10);

                entity.Property(e => e.Sdf2)
                    .HasColumnName("SDF2")
                    .HasMaxLength(5);

                entity.Property(e => e.Sdf3)
                    .HasColumnName("SDF3")
                    .HasMaxLength(10);

                entity.Property(e => e.Sdf4)
                    .HasColumnName("SDF4")
                    .HasMaxLength(10);

                entity.Property(e => e.SerNo).HasMaxLength(20);

                entity.Property(e => e.Size).HasMaxLength(5);

                entity.Property(e => e.Tag).HasMaxLength(6);

                entity.Property(e => e.ValuationId).HasColumnName("ValuationID");

                entity.Property(e => e.Year).HasMaxLength(4);
            });

            modelBuilder.Entity<TblInventoryMaintenance>(entity =>
            {
                entity.HasKey(e => e.InventoryMaintenanceId);

                entity.ToTable("tblInventoryMaintenance");

                entity.Property(e => e.InventoryMaintenanceId).HasColumnName("InventoryMaintenanceID");

                entity.Property(e => e.AdateTime)
                    .HasColumnName("ADateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Auser).HasColumnName("AUser");

                entity.Property(e => e.CdateTime)
                    .HasColumnName("CDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChangeTypeId).HasColumnName("ChangeTypeID");

                entity.Property(e => e.Cuser).HasColumnName("CUser");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            });

            modelBuilder.Entity<TblRooms>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("tblRooms");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Floor).HasMaxLength(2);

                entity.Property(e => e.RoomCode).HasMaxLength(5);

                entity.Property(e => e.RoomDescription).HasMaxLength(50);
            });

            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tblUsers");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(25);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblUsersClients>(entity =>
            {
                entity.HasKey(e => e.UsersClientId);

                entity.ToTable("tblUsersClients");

                entity.Property(e => e.UsersClientId).HasColumnName("UsersClientID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });
            modelBuilder.Query<tblTransactionsDTO>();
        }
    }
}
