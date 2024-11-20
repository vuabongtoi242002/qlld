namespace Database.Framework
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    public partial class UserDbContext : DbContext
    {
        public UserDbContext()
            : base("name=UserDbContext")
        {
        }

        public virtual DbSet<GIAOVIEN> GIAOVIENs { get; set; }
        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<LichDay> LichDays { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<Nganh> Nganhs { get; set; }
        public virtual DbSet<PHONG> PHONGs { get; set; }
        public virtual DbSet<QuanTri> QuanTris { get; set; }
        public virtual DbSet<SuKien> SuKiens { get; set; }
        public virtual DbSet<THU> THUs { get; set; }
        public virtual DbSet<TietHoc> TietHocs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GIAOVIEN>()
                .Property(e => e.BoMonPhuTrach)
                .IsUnicode(false);

            modelBuilder.Entity<GIAOVIEN>()
                .Property(e => e.SodtGV)
                .IsUnicode(false);

            modelBuilder.Entity<GIAOVIEN>()
                .Property(e => e.EmailGV)
                .IsUnicode(false);

            modelBuilder.Entity<GIAOVIEN>()
                .Property(e => e.MatKhauGV)
                .IsUnicode(false);

            modelBuilder.Entity<GIAOVIEN>()
                .HasMany(e => e.LichDays)
                .WithOptional(e => e.GIAOVIEN)
                .HasForeignKey(e => e.GVDay);

            modelBuilder.Entity<GIAOVIEN>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.GIAOVIEN)
                .HasForeignKey(e => e.NguoiGui)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GIAOVIEN>()
                .HasMany(e => e.SuKiens)
                .WithOptional(e => e.GIAOVIEN1)
                .HasForeignKey(e => e.GiaoVien);

            modelBuilder.Entity<Khoa>()
                .Property(e => e.MonHoc)
                .IsUnicode(false);

            modelBuilder.Entity<Khoa>()
                .HasMany(e => e.LichDays)
                .WithRequired(e => e.HocPhan)
                .HasForeignKey(e => e.NhomHP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Khoa>()
                .HasMany(e => e.SuKiens)
                .WithRequired(e => e.HocPhan)
                .HasForeignKey(e => e.Nhom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LichDay>()
                .Property(e => e.TenMH)
                .IsUnicode(false);

            modelBuilder.Entity<LichDay>()
                .Property(e => e.PhongHoc)
                .IsUnicode(false);

            modelBuilder.Entity<LichDay>()
                .Property(e => e.ThuNgay)
                .IsUnicode(false);

            modelBuilder.Entity<LichDay>()
                .Property(e => e.GioBatDau)
                .IsUnicode(false);

            modelBuilder.Entity<LichDay>()
                .Property(e => e.GioKetThuc)
                .IsUnicode(false);

            modelBuilder.Entity<MonHoc>()
                .Property(e => e.MaMH)
                .IsUnicode(false);

            modelBuilder.Entity<MonHoc>()
                .Property(e => e.Nganh)
                .IsUnicode(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.GIAOVIENs)
                .WithRequired(e => e.MonHoc)
                .HasForeignKey(e => e.BoMonPhuTrach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.HocPhans)
                .WithRequired(e => e.MonHoc1)
                .HasForeignKey(e => e.MonHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.LichDays)
                .WithRequired(e => e.MonHoc)
                .HasForeignKey(e => e.TenMH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.SuKiens)
                .WithRequired(e => e.MonHoc)
                .HasForeignKey(e => e.TenHP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Nganh>()
                .Property(e => e.MaNganh)
                .IsUnicode(false);

            modelBuilder.Entity<Nganh>()
                .HasMany(e => e.MonHocs)
                .WithRequired(e => e.Nganh1)
                .HasForeignKey(e => e.Nganh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHONG>()
                .Property(e => e.MaPhong)
                .IsUnicode(false);

            modelBuilder.Entity<PHONG>()
                .HasMany(e => e.LichDays)
                .WithOptional(e => e.PHONG)
                .HasForeignKey(e => e.PhongHoc);

            modelBuilder.Entity<QuanTri>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<QuanTri>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<QuanTri>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<SuKien>()
                .Property(e => e.TenHP)
                .IsUnicode(false);

            modelBuilder.Entity<THU>()
                .Property(e => e.MaThu)
                .IsUnicode(false);

            modelBuilder.Entity<THU>()
                .HasMany(e => e.LichDays)
                .WithOptional(e => e.THU)
                .HasForeignKey(e => e.ThuNgay);

            modelBuilder.Entity<TietHoc>()
                .Property(e => e.MaTiet)
                .IsUnicode(false);

            modelBuilder.Entity<TietHoc>()
                .Property(e => e.TGianBD)
                .IsUnicode(false);

            modelBuilder.Entity<TietHoc>()
                .Property(e => e.TGianKT)
                .IsUnicode(false);

            modelBuilder.Entity<TietHoc>()
                .HasMany(e => e.LichDays)
                .WithOptional(e => e.TietHoc)
                .HasForeignKey(e => e.GioBatDau);

            modelBuilder.Entity<TietHoc>()
                .HasMany(e => e.LichDays1)
                .WithOptional(e => e.TietHoc1)
                .HasForeignKey(e => e.GioKetThuc);
        }
    }
}
