using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NhaKhoa.DAL
{
    public class UserRoleDAL
    {
        public List<Models.UserRoles> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.UserRoles
                          .Include(x => x.User)
                          .Include(x => x.Role)
                          .ToList();
            }
        }

        public List<Models.UserRoles> GetByUserId(int userId)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.UserRoles
                          .Include(x => x.Role)
                          .Where(x => x.UserId == userId)
                          .ToList();
            }
        }

        public Models.UserRoles GetById(int userId, int roleId)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.UserRoles
                          .Include(x => x.User)
                          .Include(x => x.Role)
                          .SingleOrDefault(x => x.UserId == userId && x.RoleId == roleId);
            }
        }

        public void Insert(Models.UserRoles userRole)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.UserRoles.Add(userRole);
                ctx.SaveChanges();
            }
        }

        public void Delete(int userId, int roleId)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.UserRoles.SingleOrDefault(x => x.UserId == userId && x.RoleId == roleId);
                if (entity == null) return;

                ctx.UserRoles.Remove(entity);
                ctx.SaveChanges();
            }
        }

        public void DeleteByUserId(int userId)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entities = ctx.UserRoles.Where(x => x.UserId == userId).ToList();
                ctx.UserRoles.RemoveRange(entities);
                ctx.SaveChanges();
            }
        }
    }
}
