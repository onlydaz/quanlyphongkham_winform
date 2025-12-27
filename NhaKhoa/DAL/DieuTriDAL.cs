using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NhaKhoa.DAL
{
    public class DieuTriDAL
    {
        public List<Models.DieuTri> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    // Tắt lazy loading để tránh lỗi khi load navigation properties
                    ctx.Configuration.LazyLoadingEnabled = false;
                    
                    // Sử dụng AsNoTracking để tránh tracking và không load navigation properties
                    return ctx.DieuTris
                        .AsNoTracking()
                        .ToList();
                }
                catch (Exception ex)
                {
                    // Wrap exception để có thông tin chi tiết hơn
                    var innerEx = ex.InnerException ?? ex;
                    throw new Exception($"Lỗi khi tải danh sách điều trị: {innerEx.Message}", ex);
                }
            }
        }

        public Models.DieuTri GetById(string maDT)
        {
            using (var ctx = new NhaKhoaContext())
            {
                // Tắt lazy loading để tránh lỗi khi load navigation properties
                ctx.Configuration.LazyLoadingEnabled = false;
                
                return ctx.DieuTris.SingleOrDefault(x => x.MaDT == maDT);
            }
        }

        public List<Models.DieuTri> SearchByName(string ten)
        {
            using (var ctx = new NhaKhoaContext())
            {
                // Tắt lazy loading để tránh lỗi khi load navigation properties
                ctx.Configuration.LazyLoadingEnabled = false;
                
                return ctx.DieuTris
                    .Where(x => x.TenDieuTri.Contains(ten))
                    .ToList();
            }
        }

        public string GetNewMaDT()
        {
            using (var ctx = new NhaKhoaContext())
            {
                var last = ctx.DieuTris
                              .Where(x => x.MaDT != null && x.MaDT.StartsWith("DT"))
                              .ToList();

                if (!last.Any()) return "DT001";

                int max = last
                    .Select(x =>
                    {
                        int n;
                        return int.TryParse(x.MaDT.Substring(2), out n) ? n : 0;
                    })
                    .Max();

                return "DT" + (max + 1).ToString("D3");
            }
        }

        public void Insert(Models.DieuTri dt)
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    // Tắt validation để tránh lỗi foreign key khi null
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    
                    // Tạo một entity mới để tránh lỗi tracking
                    var newEntity = new Models.DieuTri
                    {
                        MaDT = dt.MaDT,
                        TenDieuTri = dt.TenDieuTri,
                        DonViTinh = dt.DonViTinh,
                        DonGia = dt.DonGia,
                        // Chỉ set foreign key nếu có giá trị, không set navigation properties
                        // MaLS không còn trong database, không cần set
                        MaCD = string.IsNullOrWhiteSpace(dt.MaCD) ? null : dt.MaCD
                    };
                    
                    ctx.DieuTris.Add(newEntity);
                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    // Lấy lỗi chi tiết từ inner exception
                    var innerEx = ex.InnerException?.InnerException;
                    throw new Exception($"Lỗi khi thêm điều trị: {innerEx?.Message ?? ex.Message}", ex);
                }
                finally
                {
                    ctx.Configuration.ValidateOnSaveEnabled = true;
                }
            }
        }

        public void Update(Models.DieuTri dt)
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    
                    var existing = ctx.DieuTris.SingleOrDefault(x => x.MaDT == dt.MaDT);
                    if (existing == null)
                        throw new ArgumentException($"Không tìm thấy điều trị với mã {dt.MaDT}");

                    existing.TenDieuTri = dt.TenDieuTri;
                    existing.DonViTinh = dt.DonViTinh;
                    existing.DonGia = dt.DonGia;
                    // MaLS không còn trong database, không cần set
                    existing.MaCD = string.IsNullOrWhiteSpace(dt.MaCD) ? null : dt.MaCD;

                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    var innerEx = ex.InnerException?.InnerException;
                    throw new Exception($"Lỗi khi cập nhật điều trị: {innerEx?.Message ?? ex.Message}", ex);
                }
                finally
                {
                    ctx.Configuration.ValidateOnSaveEnabled = true;
                }
            }
        }

        public void Delete(string maDT)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.DieuTris.SingleOrDefault(x => x.MaDT == maDT);
                if (entity == null) return;

                ctx.DieuTris.Remove(entity);
                ctx.SaveChanges();
            }
        }
    }
}
