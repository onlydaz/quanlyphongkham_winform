using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.DAL
{
    public class NhanVienDAL
    {
        public List<NhanVien> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.NhanViens.ToList();
            }
        }

        public NhanVien GetById(string maNV)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.NhanViens.SingleOrDefault(x => x.MaNV == maNV);
            }
        }

        public List<NhanVien> Search(string ma = "", string ten = "")
        {
            using (var ctx = new NhaKhoaContext())
            {
                var query = ctx.NhanViens.AsQueryable();

                // Nếu có keyword, tìm kiếm theo OR logic (mã HOẶC tên chứa keyword)
                bool hasMa = !string.IsNullOrWhiteSpace(ma);
                bool hasTen = !string.IsNullOrWhiteSpace(ten);

                if (hasMa || hasTen)
                {
                    query = query.Where(x =>
                        (hasMa && x.MaNV != null && x.MaNV.Contains(ma)) ||
                        (hasTen && x.TenNV != null && x.TenNV.Contains(ten))
                    );
                }

                return query.ToList();
            }
        }

        public string GetNewMaNV()
        {
            using (var ctx = new NhaKhoaContext())
            {
                var last = ctx.NhanViens
                              .Where(x => x.MaNV.StartsWith("NV"))
                              .ToList();

                if (!last.Any()) return "NV001";

                int max = last
                    .Select(x =>
                    {
                        int n;
                        return int.TryParse(x.MaNV.Substring(2), out n) ? n : 0;
                    })
                    .Max();

                return "NV" + (max + 1).ToString("D3");
            }
        }

        public void Insert(NhanVien nv)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.NhanViens.Add(nv);
                ctx.SaveChanges();
            }
        }

        public void Update(NhanVien nv)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Entry(nv).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(string maNV)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.NhanViens.SingleOrDefault(x => x.MaNV == maNV);
                if (entity == null) return;

                ctx.NhanViens.Remove(entity);
                ctx.SaveChanges();
            }
        }

        // Helper class để load ChucVu
        public class ChucVuInfo
        {
            public string MaCV { get; set; }
            public string TenCV { get; set; }
        }

        public List<ChucVuInfo> GetChucVuList()
        {
            using (var ctx = new NhaKhoaContext())
            {
                // Load ChucVu từ database sử dụng EF
                var chucVuList = ctx.Database.SqlQuery<ChucVuInfo>(
                    "SELECT MaCV, TenCV FROM CHUCVU ORDER BY TenCV"
                ).ToList();

                return chucVuList;
            }
        }
    }
}
