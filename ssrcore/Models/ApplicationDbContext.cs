using Microsoft.EntityFrameworkCore;

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
                optionsBuilder.UseSqlServer("Server=tcp:tranthanhhai.database.windows.net,1433;Initial Catalog=StudentServiceRequest_Database;Persist Security Info=False;User ID=thanhhai;Password=Matkhau12999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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
                    .HasName("UQ__Departme__BD7F63D58EAD97DB")
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
                entity.Property(e => e.ContentHistory).IsUnicode(false);

                entity.Property(e => e.TicketId).IsUnicode(false);

                entity.Property(e => e.UpdDatetime).HasDefaultValueSql("(getdate())");
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
                    .HasName("PK__ServiceR__712CC607076D3012");

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
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Users__536C85E489A6E405")
                    .IsUnique();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.InsBy).IsUnicode(false);

                entity.Property(e => e.InsDatetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phonenumber).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.RoleId).IsUnicode(false);

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
