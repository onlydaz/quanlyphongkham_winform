using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace NhaKhoa.DAL
{
    public class NhaKhoaContext : DbContext
    {
        // name=… phải trùng với name trong App.config
        public NhaKhoaContext() : base("name=QuanLyPhongKhamContext")
        {
        }

        // Dùng fully qualified name để tránh xung đột với namespace NhaKhoa.Thuoc, NhaKhoa.BenhNhan, NhaKhoa.NhanVien
        public DbSet<Models.Thuoc> Thuocs { get; set; }
        public DbSet<Models.BenhNhan> BenhNhans { get; set; }
        public DbSet<Models.NhanVien> NhanViens { get; set; }
        public DbSet<Models.HoaDon> HoaDons { get; set; }
        public DbSet<Models.VatLieu> VatLieus { get; set; }
        public DbSet<Models.Users> Users { get; set; }
        public DbSet<Models.Roles> Roles { get; set; }
        public DbSet<Models.UserRoles> UserRoles { get; set; }
        public DbSet<Models.ChanDoan> ChanDoans { get; set; }
        public DbSet<Models.DieuTri> DieuTris { get; set; }
        public DbSet<Models.LamSan> LamSans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình composite key cho UserRoles
            modelBuilder.Entity<Models.UserRoles>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // Cấu hình foreign key relationships
            modelBuilder.Entity<Models.UserRoles>()
                .HasRequired(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Models.UserRoles>()
                .HasRequired(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .WillCascadeOnDelete(false);

            // Cấu hình ChanDoan - theo ERD mới: chỉ có MaCD và TenChuanDoan (không có MaLS trong database)
            var chanDoanConfig = modelBuilder.Entity<Models.ChanDoan>();
            
            // MaLS đã được đánh dấu [NotMapped] trong Model, không cần config
            // Liên kết được quản lý ở bảng LAMSAN (LAMSAN có MaCD và MaDT)
            // Không config foreign key relationship với LamSan vì theo ERD mới không có
            // Relationship được quản lý ngược lại: LAMSAN -> CHANDOAN

            // Cấu hình LamSan
            var lamSanConfig = modelBuilder.Entity<Models.LamSan>();
            
            // Cấu hình relationship: LamSan -> DieuTri (LamSan là principal, có MaDT)
            lamSanConfig.HasOptional(l => l.DieuTri)
                .WithMany()
                .HasForeignKey(l => l.MaDT)
                .WillCascadeOnDelete(false);

            // Cấu hình relationship: LamSan -> ChanDoan (LamSan là principal, có MaCD)
            lamSanConfig.HasOptional(l => l.ChanDoan)
                .WithMany()
                .HasForeignKey(l => l.MaCD)
                .WillCascadeOnDelete(false);

            // Cấu hình DieuTri
            var dieuTriConfig = modelBuilder.Entity<Models.DieuTri>();
            
            // MaLS đã được đánh dấu [NotMapped] trong Model vì không còn trong database
            // Các foreign keys là optional
            dieuTriConfig.Property(d => d.MaCD).IsOptional();
            dieuTriConfig.Property(d => d.TenDieuTri).IsOptional();
            dieuTriConfig.Property(d => d.DonViTinh).IsOptional();
            dieuTriConfig.Property(d => d.DonGia).IsOptional();

            // Cấu hình relationship: DieuTri -> ChanDoan (DieuTri là principal, có MaCD)
            dieuTriConfig.HasOptional(d => d.Chandoan)
                .WithMany()
                .HasForeignKey(d => d.MaCD)
                .WillCascadeOnDelete(false);

            // Ignore navigation property Lamsan vì không có relationship từ DieuTri đến LamSan
            // Relationship chính là LamSan -> DieuTri (qua LamSan.MaDT)
            dieuTriConfig.Ignore(d => d.Lamsan);
        }
    }
}
