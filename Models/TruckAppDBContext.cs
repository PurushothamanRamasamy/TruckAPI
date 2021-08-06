using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TruckAPI.Models
{
    public partial class TruckAppDBContext : DbContext
    {
        public TruckAppDBContext()
        {
        }

        public TruckAppDBContext(DbContextOptions<TruckAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Truck> Trucks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KANINI-LTP-329;Database=TruckAppDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

           /* modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.EstimatedStartDate).HasColumnType("date");

                entity.Property(e => e.PickandDropCity)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TruckNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                *//*entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RequestCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__Custome__3E52440B");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.RequestManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__Manager__3F466844");*/

               /* entity.HasOne(d => d.TruckNumberNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.TruckNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__TruckNu__403A8C7D");*//*
            });
*/
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

               

                entity.Property(e => e.BookingDate).HasColumnType("date");

                entity.Property(e => e.TruckNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                /*entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ServiceCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Service__Custome__45F365D3");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ServiceManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Service__Manager__46E78A0C");*/

                /*entity.HasOne(d => d.Request)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Service__Request__44FF419A");*/

                /*entity.HasOne(d => d.TruckNumberNavigation)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.TruckNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Service__TruckNu__47DBAE45");*/
            });

            modelBuilder.Entity<Truck>(entity =>
            {
                entity.HasKey(e => e.TruckNumber)
                    .HasName("PK__Truck__32F15A05E40D9708");

                entity.ToTable("Truck");

                entity.Property(e => e.TruckNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DriverLicenceNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DriverName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DropCity)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.PickCity)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TruckType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

               /* entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Trucks)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Truck__ManagerId__3A81B327");*/
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.MobileNumber, "UQ__users__250375B1BE6891A6")
                    .IsUnique();

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserStatus).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
