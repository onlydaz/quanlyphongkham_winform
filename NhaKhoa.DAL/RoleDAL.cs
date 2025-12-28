using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.DAL
{
    public class RoleDAL
    {
        public List<Roles> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Roles.ToList();
            }
        }

        public Roles GetById(int id)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Roles.SingleOrDefault(x => x.Id == id);
            }
        }

        public Roles GetByName(string name)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Roles.SingleOrDefault(x => x.Name == name);
            }
        }

        public void Insert(Roles role)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Roles.Add(role);
                ctx.SaveChanges();
            }
        }

        public void Update(Roles role)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Entry(role).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.Roles.SingleOrDefault(x => x.Id == id);
                if (entity == null) return;

                ctx.Roles.Remove(entity);
                ctx.SaveChanges();
            }
        }
    }
}
