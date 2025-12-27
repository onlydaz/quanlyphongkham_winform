using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NhaKhoa.DAL
{
    public class VatLieuDAL
    {
        public List<Models.VatLieu> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.VatLieus.ToList();
            }
        }

        public Models.VatLieu GetById(string maDC)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.VatLieus.SingleOrDefault(x => x.MaDC == maDC);
            }
        }

        public List<Models.VatLieu> SearchByName(string ten)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.VatLieus
                          .Where(x => x.TenDC.Contains(ten))
                          .ToList();
            }
        }

        public string GetNewMaDC()
        {
            using (var ctx = new NhaKhoaContext())
            {
                var last = ctx.VatLieus
                              .Where(x => x.MaDC.StartsWith("DC"))
                              .ToList();

                if (!last.Any()) return "DC001";

                int max = last
                    .Select(x =>
                    {
                        int n;
                        return int.TryParse(x.MaDC.Substring(2), out n) ? n : 0;
                    })
                    .Max();

                return "DC" + (max + 1).ToString("D3");
            }
        }

        public void Insert(Models.VatLieu vl)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.VatLieus.Add(vl);
                ctx.SaveChanges();
            }
        }

        public void Update(Models.VatLieu vl)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Entry(vl).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(string maDC)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.VatLieus.SingleOrDefault(x => x.MaDC == maDC);
                if (entity == null) return;

                ctx.VatLieus.Remove(entity);
                ctx.SaveChanges();
            }
        }
    }
}
