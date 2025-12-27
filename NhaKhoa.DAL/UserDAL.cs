using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NhaKhoa.Models;

namespace NhaKhoa.DAL
{
    public class UserDAL
    {
        public List<Users> GetAll()
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Users
                          .Include(x => x.UserRoles)
                          .ToList();
            }
        }

        public Users GetById(int id)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Users
                          .Include(x => x.UserRoles)
                          .SingleOrDefault(x => x.Id == id);
            }
        }

        public Users GetByUsername(string username)
        {
            using (var ctx = new NhaKhoaContext())
            {
                return ctx.Users
                          .Include(x => x.UserRoles)
                          .SingleOrDefault(x => x.Username == username);
            }
        }

        public (string PasswordHash, string RoleName)? GetPasswordHashAndRole(string username)
        {
            using (var ctx = new NhaKhoaContext())
            {
                var user = ctx.Users
                             .Include(x => x.UserRoles.Select(ur => ur.Role))
                             .SingleOrDefault(x => x.Username == username && x.IsActive);

                if (user == null || !user.UserRoles.Any())
                    return null;

                var role = user.UserRoles.First().Role;
                return (user.PasswordHash, role.Name);
            }
        }

        public List<TaiKhoan> GetAllTaiKhoan()
        {
            using (var ctx = new NhaKhoaContext())
            {
                // Load v? memory tru?c d? có th? dùng string.Join
                var users = ctx.Users
                              .Include(x => x.UserRoles.Select(ur => ur.Role))
                              .Where(x => x.IsActive)
                              .ToList();

                var result = new List<TaiKhoan>();

                foreach (var u in users)
                {
                    var roles = u.UserRoles?
                                 .Select(ur => ur.Role?.Name)
                                 .Where(r => !string.IsNullOrEmpty(r))
                                 .ToList() ?? new List<string>();

                    result.Add(new TaiKhoan
                    {
                        Id = u.Id,
                        Username = u.Username,
                        FullName = u.FullName,
                        Email = u.Email,
                        IsActive = u.IsActive,
                        Status = u.IsActive ? "Ho?t d?ng" : "Không ho?t d?ng",
                        Roles = string.Join(", ", roles)
                    });
                }

                return result;
            }
        }

        public void Insert(Users user)
        {
            using (var ctx = new NhaKhoaContext())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }

        public void Update(Users user)
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
