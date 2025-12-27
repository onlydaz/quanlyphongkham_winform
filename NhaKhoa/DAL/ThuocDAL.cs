using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NhaKhoa.DAL
{
    public class ThuocDAL
    {
        // Dùng fully qualified name Models.Thuoc để tránh xung đột với namespace NhaKhoa.Thuoc
        public List<Models.Thuoc> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Thuocs.ToList();
            }
        }

        public List<Models.Thuoc> SearchByName(string ten)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Thuocs
                          .Where(t => t.TenThuoc.Contains(ten))
                          .ToList();
            }
        }

        public string GetNewMaThuoc()
        {
            using (var ctx = new NhaKhoaContext())
            {
                var last = ctx.Thuocs
                              .Where(t => t.MaThuoc.StartsWith("TH"))
                              .ToList();

                if (!last.Any()) return "TH001";

                // Giữ lại logic cũ: tách số phía sau
                int max = last
                    .Select(t =>
                    {
                        int n;
                        return int.TryParse(t.MaThuoc.Substring(2), out n) ? n : 0;
                    })
                    .Max();

                return "TH" + (max + 1).ToString("D3");
            }
        }

        public void Insert(Models.Thuoc t)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Thuocs.Add(t);
                ctx.SaveChanges();
            }
        }

        public void Update(Models.Thuoc t)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Entry(t).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(string maThuoc)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.Thuocs.SingleOrDefault(x => x.MaThuoc == maThuoc);
                if (entity == null) return;

                ctx.Thuocs.Remove(entity);
                ctx.SaveChanges();
            }
        }

        public Models.Thuoc GetById(string maThuoc)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Thuocs.SingleOrDefault(x => x.MaThuoc == maThuoc);
            }
        }
    }
}
