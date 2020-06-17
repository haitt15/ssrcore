using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ssrcore.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<FcmToken> FcmToken { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<RequestHistory> RequestHistory { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceRequest> ServiceRequest { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:tungduong.database.windows.net,1433;Initial Catalog=SSR_DB;Persist Security Info=False;User ID=tungduong;Password=Sonheo123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.InsBy).IsUnicode(false);

                entity.Property(e => e.InsDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TicketId).IsUnicode(false);

                entity.Property(e => e.UpdBy).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK_Comment_Request");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.RoomNum)
                    .HasName("UQ__Departme__BD7F63D5EE0A8CAA")
                    .IsUnique();

                entity.Property(e => e.DepartmentId).IsUnicode(false);

                entity.Property(e => e.Hotline).IsUnicode(false);

                entity.Property(e => e.InsBy).IsUnicode(false);

                entity.Property(e => e.InsDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoomNum).IsUnicode(false);

                entity.Property(e => e.UpdBy).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_Department_Manager");
            });

            modelBuilder.Entity<FcmToken>(entity =>
            {
                entity.Property(e => e.FcmToken1).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FcmToken)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FCM_User");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.InsBy).IsUnicode(false);

                entity.Property(e => e.InsDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsRead).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdBy).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FromUserNavigation)
                    .WithMany(p => p.NotificationFromUserNavigation)
                    .HasForeignKey(d => d.FromUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Noti_User");

                entity.HasOne(d => d.ToUserNavigation)
                    .WithMany(p => p.NotificationToUserNavigation)
                    .HasForeignKey(d => d.ToUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Noti2_User");
            });

            modelBuilder.Entity<RequestHistory>(entity =>
            {
                entity.Property(e => e.FromStatus).IsUnicode(false);

                entity.Property(e => e.TicketId).IsUnicode(false);

                entity.Property(e => e.ToStatus).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.RequestHistory)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_History_Comment");

                entity.HasOne(d => d.FromStaffNavigation)
                    .WithMany(p => p.RequestHistoryFromStaffNavigation)
                    .HasForeignKey(d => d.FromStaff)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_From_Staff");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.RequestHistory)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK_Request_id");

                entity.HasOne(d => d.ToStaffNavigation)
                    .WithMany(p => p.RequestHistoryToStaffNavigation)
                    .HasForeignKey(d => d.ToStaff)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_To_Staff");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).IsUnicode(false);

                entity.Property(e => e.InsBy).IsUnicode(false);

                entity.Property(e => e.InsDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleNm).IsUnicode(false);

                entity.Property(e => e.UpdBy).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceId).IsUnicode(false);

                entity.Property(e => e.DepartmentId).IsUnicode(false);

                entity.Property(e => e.InsBy).IsUnicode(false);

                entity.Property(e => e.InsDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdBy).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Services_Department");
            });

            modelBuilder.Entity<ServiceRequest>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("PK__ServiceR__712CC6079D327BE8");

                entity.Property(e => e.TicketId).IsUnicode(false);

                entity.Property(e => e.InsBy).IsUnicode(false);

                entity.Property(e => e.InsDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ServiceId).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.UpdBy).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceRequest)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Service");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.ServiceRequest)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Request_Staff");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ServiceRequest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_User");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffId).ValueGeneratedNever();

                entity.Property(e => e.DepartmentId).IsUnicode(false);

                entity.Property(e => e.InsBy).IsUnicode(false);

                entity.Property(e => e.InsDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdBy).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.DepartmentNavigation)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_Department");

                entity.HasOne(d => d.StaffNavigation)
                    .WithOne(p => p.Staff)
                    .HasForeignKey<Staff>(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_User");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.UserNo)
                    .HasName("UQ__Users__1788955EA586BCB5")
                    .IsUnique();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.InsBy).IsUnicode(false);

                entity.Property(e => e.InsDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phonenumber).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.RoleId).IsUnicode(false);

                entity.Property(e => e.Uid).IsUnicode(false);

                entity.Property(e => e.UpdBy).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserNo).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
