using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NhaKhoa.DAL
{
    public class LichBacSiDAL
    {
        public List<Models.LichBacSi> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.LichBacSis.Include(x => x.BacSi).ToList();
            }
        }

        public List<Models.LichBacSi> GetByBacSi(string maBacSi)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.LichBacSis.Where(x => x.MaBacSi == maBacSi && x.IsActive).Include(x => x.BacSi).ToList();
            }
        }

        public Models.LichBacSi GetById(int id)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.LichBacSis.Include(x => x.BacSi).SingleOrDefault(x => x.Id == id);
            }
        }

        public void Insert(Models.LichBacSi lich)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.LichBacSis.Add(lich);
                ctx.SaveChanges();
            }
        }

        public void Update(Models.LichBacSi lich)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Entry(lich).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.LichBacSis.SingleOrDefault(x => x.Id == id);
                if (entity == null) return;
                ctx.LichBacSis.Remove(entity);
                ctx.SaveChanges();
            }
        }

        public bool CheckOverlap(string maBacSi, byte ngayTrongTuan, TimeSpan start, TimeSpan end, int? excludeId = null)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var query = ctx.LichBacSis.Where(x => x.MaBacSi == maBacSi && x.NgayTrongTuan == ngayTrongTuan && x.IsActive);
                if (excludeId.HasValue)
                    query = query.Where(x => x.Id != excludeId.Value);

                foreach (var item in query)
                {
                    if (!(end <= item.GioBatDau || start >= item.GioKetThuc))
                        return true;
                }

                return false;
            }
        }
    }
}
