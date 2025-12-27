using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NhaKhoa.DAL
{
    public class BenhNhanDAL
    {
        public List<Models.BenhNhan> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.BenhNhans.ToList();
            }
        }

        public Models.BenhNhan GetById(string maBN)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.BenhNhans.SingleOrDefault(x => x.MaBN == maBN);
            }
        }

        public List<Models.BenhNhan> Search(string ma = "", string ten = "", string sdt = "")
        {
            using (var ctx = new NhaKhoaContext())
            {
                var query = ctx.BenhNhans.AsQueryable();

                // Nếu có keyword, tìm kiếm theo OR logic (mã HOẶC tên HOẶC SDT chứa keyword)
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

        public List<Models.BenhNhan> GetByTrangThai(string trangThai)
        {
            using (var ctx = new NhaKhoaContext())
            {
                // Thử cách 1: Query thông thường
                try
                {
                    var query = ctx.BenhNhans.AsQueryable();
                    
                    if (string.IsNullOrEmpty(trangThai))
                    {
                        // Nếu trangThai là null hoặc empty, lấy các record có TrangThai là null hoặc empty
                        query = query.Where(x => x.TrangThai == null || x.TrangThai == "" || x.TrangThai == trangThai);
                    }
                    else
                    {
                        // So sánh chính xác, không phân biệt hoa thường
                        query = query.Where(x => x.TrangThai != null && x.TrangThai.Trim() == trangThai.Trim());
                    }
                    
                    var result = query.OrderBy(x => x.NgayKham).ToList();
                    
                    // Debug
                    System.Diagnostics.Debug.WriteLine($"GetByTrangThai('{trangThai}') - Số kết quả: {result.Count}");
                    if (result.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"Ví dụ TrangThai: '{result.First().TrangThai ?? "NULL"}'");
                    }
                    
                    return result;
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, thử load tất cả rồi filter trong memory
                    System.Diagnostics.Debug.WriteLine($"Lỗi query TrangThai: {ex.Message}");
                    var all = ctx.BenhNhans.ToList();
                    System.Diagnostics.Debug.WriteLine($"Tổng số bệnh nhân: {all.Count}");
                    
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

        public void Insert(Models.BenhNhan bn)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.BenhNhans.Add(bn);
                ctx.SaveChanges();
            }
        }

        public void Update(Models.BenhNhan bn)
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
