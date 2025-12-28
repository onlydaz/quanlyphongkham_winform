using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.DAL
{
    public class ChanDoanDAL
    {
        public List<ChanDoan> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.ChanDoans.AsNoTracking().ToList();
            }
        }

        public ChanDoan GetById(string maCD)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.ChanDoans.AsNoTracking().SingleOrDefault(x => x.MaCD == maCD);
            }
        }

        public List<ChanDoan> GetByMaBN(string maBN)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var maCDList = ctx.LamSans
                    .AsNoTracking()
                    .Where(l => l.MaBN == maBN && l.MaCD != null)
                    .Select(l => l.MaCD)
                    .Distinct()
                    .ToList();

                return ctx.ChanDoans
                    .AsNoTracking()
                    .Where(x => maCDList.Contains(x.MaCD))
                    .ToList();
            }
        }

        public List<ChanDoan> SearchByName(string ten)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.ChanDoans
                          .AsNoTracking()
                          .Where(x => x.TenChuanDoan.Contains(ten))
                          .ToList();
            }
        }

        public string GetNewMaCD()
        {
            using (var ctx = new NhaKhoaContext())
            {
                var last = ctx.ChanDoans
                              .AsNoTracking()
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

        public void Insert(ChanDoan cd)
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    var newEntity = new ChanDoan
                    {
                        MaCD = cd.MaCD,
                        TenChuanDoan = cd.TenChuanDoan
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

        public void Update(ChanDoan cd)
        {
            using (var ctx = new NhaKhoaContext())
            {
                try
                {
                    var existing = ctx.ChanDoans.SingleOrDefault(x => x.MaCD == cd.MaCD);
                    if (existing == null)
                        throw new ArgumentException($"Không tìm thấy chẩn đoán với mã {cd.MaCD}");

                    existing.TenChuanDoan = cd.TenChuanDoan;

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
