using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NhaKhoa.Models;

namespace NhaKhoa.DAL
{
    public class BenhNhanDAL
    {
        public List<BenhNhan> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.BenhNhans.ToList();
            }
        }

        public BenhNhan GetById(string maBN)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.BenhNhans.SingleOrDefault(x => x.MaBN == maBN);
            }
        }

        public List<BenhNhan> Search(string ma = "", string ten = "", string sdt = "")
        {
            using (var ctx = new NhaKhoaContext())
            {
                var query = ctx.BenhNhans.AsQueryable();

                // N?u có keyword, tìm ki?m theo OR logic (mã HO?C tên HO?C SDT ch?a keyword)
                bool hasMa = !string.IsNullOrWhiteSpace(ma);
                bool hasTen = !string.IsNullOrWhiteSpace(ten);
                bool hasSdt = !string.IsNullOrWhiteSpace(sdt);

                if (hasMa || hasTen || hasSdt)
                {
                    query = query.Where(x =>
                        (hasMa && x.MaBN != null && x.MaBN.Contains(ma)) ||
                        (hasTen && x.TenBN != null && x.TenBN.Contains(ten)) ||
                        (hasSdt && x.SDT != null && x.SDT.Contains(sdt))
                    );
                }

                return query.ToList();
            }
        }

        public List<BenhNhan> GetByTrangThai(string trangThai)
        {
            using (var ctx = new NhaKhoaContext())
            {
                // Th? cách 1: Query thông thu?ng
                try
                {
                    var query = ctx.BenhNhans.AsQueryable();
                    
                    if (string.IsNullOrEmpty(trangThai))
                    {
                        // N?u trangThai là null ho?c empty, l?y các record có TrangThai là null ho?c empty
                        query = query.Where(x => x.TrangThai == null || x.TrangThai == "" || x.TrangThai == trangThai);
                    }
                    else
                    {
                        // So sánh chính xác, không phân bi?t hoa thu?ng
                        query = query.Where(x => x.TrangThai != null && x.TrangThai.Trim() == trangThai.Trim());
                    }
                    
                    var result = query.OrderBy(x => x.NgayKham).ToList();
                    
                    // Debug
                    System.Diagnostics.Debug.WriteLine($"GetByTrangThai('{trangThai}') - S? k?t qu?: {result.Count}");
                    if (result.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"Ví d? TrangThai: '{result.First().TrangThai ?? "NULL"}'");
                    }
                    
                    return result;
                }
                catch (Exception ex)
                {
                    // N?u có l?i, th? load t?t c? r?i filter trong memory
                    System.Diagnostics.Debug.WriteLine($"L?i query TrangThai: {ex.Message}");
                    var all = ctx.BenhNhans.ToList();
                    System.Diagnostics.Debug.WriteLine($"T?ng s? b?nh nhân: {all.Count}");
                    
                    if (string.IsNullOrEmpty(trangThai))
                    {
                        return all.Where(x => x.TrangThai == null || x.TrangThai == "").OrderBy(x => x.NgayKham).ToList();
                    }
                    else
                    {
                        return all.Where(x => x.TrangThai != null && x.TrangThai.Trim().Equals(trangThai.Trim(), StringComparison.OrdinalIgnoreCase))
                                  .OrderBy(x => x.NgayKham).ToList();
                    }
                }
            }
        }

        public string GetNewMaBN()
        {
            using (var ctx = new NhaKhoaContext())
            {
                var last = ctx.BenhNhans
                              .Where(x => x.MaBN.StartsWith("BN"))
                              .ToList();

                if (!last.Any()) return "BN001";

                int max = last
                    .Select(x =>
                    {
                        int n;
                        return int.TryParse(x.MaBN.Substring(2), out n) ? n : 0;
                    })
                    .Max();

                return "BN" + (max + 1).ToString("D3");
            }
        }

        public void Insert(BenhNhan bn)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.BenhNhans.Add(bn);
                ctx.SaveChanges();
            }
        }

        public void Update(BenhNhan bn)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Entry(bn).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(string maBN)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.BenhNhans.SingleOrDefault(x => x.MaBN == maBN);
                if (entity == null) return;

                ctx.BenhNhans.Remove(entity);
                ctx.SaveChanges();
            }
        }
    }
}
