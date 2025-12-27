using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using NhaKhoa.Models;

namespace NhaKhoa.DAL
{
    public class NhaKhoaContext : DbContext
    {
        public NhaKhoaContext() : base("name=QuanLyPhongKhamContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Thuoc> Thuocs { get; set; }
        public DbSet<BenhNhan> BenhNhans { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<VatLieu> VatLieus { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<ChanDoan> ChanDoans { get; set; }
        public DbSet<DieuTri> DieuTris { get; set; }
        public DbSet<LamSan> LamSans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoles>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRoles>()
                .HasRequired(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRoles>()
                .HasRequired(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .WillCascadeOnDelete(false);

            var dieuTriConfig = modelBuilder.Entity<DieuTri>();
            dieuTriConfig.Property(d => d.MaCD).IsOptional();
            dieuTriConfig.Property(d => d.TenDieuTri).IsOptional();
            dieuTriConfig.Property(d => d.DonViTinh).IsOptional();
            dieuTriConfig.Property(d => d.DonGia).IsOptional();
            
            var lamSanConfig = modelBuilder.Entity<LamSan>();
            lamSanConfig.Property(l => l.MaDT).IsOptional();
            lamSanConfig.Property(l => l.MaCD).IsOptional();
        }
    }
}
