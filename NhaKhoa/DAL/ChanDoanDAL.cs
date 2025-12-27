using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NhaKhoa.DAL
{
    public class ChanDoanDAL
    {
        public List<Models.ChanDoan> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.ChanDoans.ToList();
            }
        }

        public Models.ChanDoan GetById(string maCD)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.ChanDoans.SingleOrDefault(x => x.MaCD == maCD);
            }
        }

        public List<Models.ChanDoan> SearchByName(string ten)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.ChanDoans
                          .Where(x => x.TenChuanDoan.Contains(ten))
                          .ToList();
            }
        }

        public string GetNewMaCD()
        {
            using (var ctx = new NhaKhoaContext())
            {
                var last = ctx.ChanDoans
                              .Where(x => x.MaCD != null && x.MaCD.StartsWith("CD"))
                              .ToList();

                if (!last.Any()) return "CD001";

                int max = last
                    .Select(x =>
                    {
                        int n;
                        return int.TryParse(x.MaCD.Substring(2), out n) ? n : 0;
                    })
                    .Max();

                return "CD" + (max + 1).ToString("D3");
            }
        }

        public void Insert(Models.ChanDoan cd)
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    // Theo ERD mới: CHANDOAN không có MaLS, liên kết được quản lý ở bảng LAMSAN
                    // Không cần kiểm tra MaLS nữa
                    
                    // Tạo một entity mới để tránh lỗi tracking
                    // MaLS không còn trong database, không cần set
                    var newEntity = new Models.ChanDoan
                    {
                        MaCD = cd.MaCD,
                        TenChuanDoan = cd.TenChuanDoan
                        // MaLS không còn trong database, không set
                    };
                    
                    ctx.ChanDoans.Add(newEntity);
                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    var innerEx = ex.InnerException?.InnerException;
                    string errorMsg = innerEx?.Message ?? ex.Message;
                    throw new Exception($"Lỗi khi thêm chẩn đoán: {errorMsg}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi thêm chẩn đoán: {ex.Message}", ex);
                }
            }
        }

        public void Update(Models.ChanDoan cd)
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    // Theo ERD mới: CHANDOAN không có MaLS, liên kết được quản lý ở bảng LAMSAN
                    // Không cần kiểm tra MaLS nữa
                    
                    var existing = ctx.ChanDoans.SingleOrDefault(x => x.MaCD == cd.MaCD);
                    if (existing == null)
                        throw new ArgumentException($"Không tìm thấy chẩn đoán với mã {cd.MaCD}");

                    existing.TenChuanDoan = cd.TenChuanDoan;
                    // MaLS không còn trong database, không cần cập nhật

                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    var innerEx = ex.InnerException?.InnerException;
                    string errorMsg = innerEx?.Message ?? ex.Message;
                    throw new Exception($"Lỗi khi cập nhật chẩn đoán: {errorMsg}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật chẩn đoán: {ex.Message}", ex);
                }
            }
        }

        public void Delete(string maCD)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.ChanDoans.SingleOrDefault(x => x.MaCD == maCD);
                if (entity == null) return;

                ctx.ChanDoans.Remove(entity);
                ctx.SaveChanges();
            }
        }
    }
}
