using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.DAL
{
    public class DieuTriDAL
    {
        public List<DieuTri> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    
                    var result = ctx.DieuTris
                        .AsNoTracking()
                        .ToList();
                    
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy danh sách điều trị: {ex.Message}", ex);
                }
            }
        }

        public DieuTri GetById(string maDT)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.DieuTris.AsNoTracking().SingleOrDefault(x => x.MaDT == maDT);
            }
        }

        public List<DieuTri> SearchByName(string ten)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.DieuTris
                          .AsNoTracking()
                          .Where(x => x.TenDieuTri.Contains(ten))
                          .ToList();
            }
        }

        public string GetNewMaDT()
        {
            using (var ctx = new NhaKhoaContext())
            {
                var last = ctx.DieuTris
                              .AsNoTracking()
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

        public void Insert(DieuTri dt)
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    ctx.Configuration.AutoDetectChangesEnabled = false;
                    
                    var newEntity = new DieuTri
                    {
                        MaDT = dt.MaDT,
                        TenDieuTri = dt.TenDieuTri ?? string.Empty,
                        DonViTinh = dt.DonViTinh ?? string.Empty,
                        DonGia = dt.DonGia,
                        MaCD = string.IsNullOrWhiteSpace(dt.MaCD) ? null : dt.MaCD
                    };
                    
                    ctx.DieuTris.Add(newEntity);
                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    var innerEx = ex.InnerException?.InnerException;
                    throw new Exception($"Lỗi khi thêm điều trị: {innerEx?.Message ?? ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi thêm điều trị: {ex.Message}", ex);
                }
                finally
                {
                    ctx.Configuration.ValidateOnSaveEnabled = true;
                    ctx.Configuration.AutoDetectChangesEnabled = true;
                }
            }
        }

        public void Update(DieuTri dt)
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    ctx.Configuration.AutoDetectChangesEnabled = false;
                    
                    var entity = new DieuTri
                    {
                        MaDT = dt.MaDT,
                        TenDieuTri = dt.TenDieuTri,
                        DonViTinh = dt.DonViTinh,
                        DonGia = dt.DonGia,
                        MaCD = string.IsNullOrWhiteSpace(dt.MaCD) ? null : dt.MaCD
                    };
                    
                    ctx.DieuTris.Attach(entity);
                    ctx.Entry(entity).State = EntityState.Modified;

                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    var innerEx = ex.InnerException?.InnerException;
                    throw new Exception($"Lỗi khi cập nhật điều trị: {innerEx?.Message ?? ex.Message}", ex);
                }
                catch (System.InvalidOperationException)
                {
                    try
                    {
                        var existing = ctx.DieuTris.SingleOrDefault(x => x.MaDT == dt.MaDT);
                        if (existing == null)
                            throw new ArgumentException($"Không tìm thấy điều trị với mã {dt.MaDT}");

                        existing.TenDieuTri = dt.TenDieuTri;
                        existing.DonViTinh = dt.DonViTinh;
                        existing.DonGia = dt.DonGia;
                        existing.MaCD = string.IsNullOrWhiteSpace(dt.MaCD) ? null : dt.MaCD;

                        ctx.SaveChanges();
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception($"Lỗi khi cập nhật điều trị: {ex2.Message}", ex2);
                    }
                }
                finally
                {
                    ctx.Configuration.ValidateOnSaveEnabled = true;
                    ctx.Configuration.AutoDetectChangesEnabled = true;
                }
            }
        }

        public void Delete(string maDT)
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    ctx.Configuration.AutoDetectChangesEnabled = false;
                    
                    var entity = new DieuTri { MaDT = maDT };
                    ctx.DieuTris.Attach(entity);
                    ctx.DieuTris.Remove(entity);
                    ctx.SaveChanges();
                }
                catch (System.InvalidOperationException)
                {
                    var existing = ctx.DieuTris.SingleOrDefault(x => x.MaDT == maDT);
                    if (existing != null)
                    {
                        ctx.DieuTris.Remove(existing);
                        ctx.SaveChanges();
                    }
                }
                finally
                {
                    ctx.Configuration.ValidateOnSaveEnabled = true;
                    ctx.Configuration.AutoDetectChangesEnabled = true;
                }
            }
        }
    }
}
