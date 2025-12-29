using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NhaKhoa.DAL
{
    public class UserDAL
    {
        public List<Models.Users> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Users
                          .Include(x => x.UserRoles)
                          .ToList();
            }
        }

        public Models.Users GetById(int id)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Users
                          .Include(x => x.UserRoles)
                          .SingleOrDefault(x => x.Id == id);
            }
        }

        public Models.Users GetByUsername(string username)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Users
                          .Include(x => x.UserRoles)
                          .SingleOrDefault(x => x.Username == username);
            }
        }

        public (int UserId, string PasswordHash, string RoleName)? GetPasswordHashAndRole(string username)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var user = ctx.Users
                             .Include(x => x.UserRoles.Select(ur => ur.Role))
                             .SingleOrDefault(x => x.Username == username && x.IsActive);

                if (user == null || !user.UserRoles.Any())
                    return null;

                var role = user.UserRoles.First().Role;
                return (user.Id, user.PasswordHash, role.Name);
            }
        }

        // GetAllTaiKhoan removed; use GetAll() and UserRoleBUS to assemble role strings in UI/BUS layer.

        public void Insert(Models.Users user)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }

        public void Update(Models.Users user)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Entry(user).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var entity = ctx.Users.SingleOrDefault(x => x.Id == id);
                if (entity == null) return;

                ctx.Users.Remove(entity);
                ctx.SaveChanges();
            }
        }
    }
}
