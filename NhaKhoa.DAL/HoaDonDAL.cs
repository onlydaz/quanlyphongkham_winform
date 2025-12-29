using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.DAL
{
    public class HoaDonDAL
    {
        public List<HoaDon> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.HoaDons
                          .Include(x => x.BenhNhan)
                          .Include(x => x.NhanVien)
                          .ToList();
            }
        }

        public HoaDon GetById(string maHD)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.HoaDons
                          .Include(x => x.BenhNhan)
                          .Include(x => x.NhanVien)
                          .SingleOrDefault(x => x.MaHD == maHD);
            }
        }

        public List<HoaDon> Search(string maHD = "", string maNV = "", string tenBN = "", string tenNV = "", DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var query = ctx.HoaDons
                              .Include(x => x.BenhNhan)
                              .Include(x => x.NhanVien)
                              .AsQueryable();

                if (!string.IsNullOrWhiteSpace(maHD))
                    query = query.Where(x => x.MaHD.Contains(maHD));

                if (!string.IsNullOrWhiteSpace(maNV))
                    query = query.Where(x => x.MaNV == maNV);

                if (!string.IsNullOrWhiteSpace(tenBN))
                    query = query.Where(x => x.BenhNhan.TenBN.Contains(tenBN));

                if (!string.IsNullOrWhiteSpace(tenNV))
                    query = query.Where(x => x.NhanVien.TenNV.Contains(tenNV));

                if (tuNgay.HasValue)
                {
                    var tuNgayStart = tuNgay.Value.Date;
                    query = query.Where(x => x.NgayLapHD >= tuNgayStart);
                }

                if (denNgay.HasValue)
                {
                    // T�nh to�n denNgayEnd tru?c khi query (kh�ng d�ng AddSeconds trong LINQ)
                    var denNgayEnd = denNgay.Value.Date.AddDays(1).AddTicks(-1);
                    query = query.Where(x => x.NgayLapHD <= denNgayEnd);
                }

                return query.OrderByDescending(x => x.NgayLapHD).ToList();
            }
        }

        public string GetNewMaHD()
        {
            using (var ctx = new NhaKhoaContext())
            {
                var last = ctx.HoaDons
                              .Where(x => x.MaHD.StartsWith("HD"))
                              .ToList();

                if (!last.Any()) return "HD001";

                int max = last
                    .Select(x =>
                    {
                        int n;
                        return int.TryParse(x.MaHD.Substring(2), out n) ? n : 0;
                    })
                    .Max();

                return "HD" + (max + 1).ToString("D3");
            }
        }

        public void Insert(HoaDon hd)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.HoaDons.Add(hd);
                ctx.SaveChanges();
            }
        }

        public void Update(HoaDon hd)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Entry(hd).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(string maHD)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.HoaDons.SingleOrDefault(x => x.MaHD == maHD);
                if (entity == null) return;

                ctx.HoaDons.Remove(entity);
                ctx.SaveChanges();
            }
        }
    }
}
