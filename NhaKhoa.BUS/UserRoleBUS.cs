using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.BUS
{
    public class UserRoleBUS
    {
        private readonly UserRoleDAL _dal;

        public UserRoleBUS()
        {
            _dal = new UserRoleDAL();
        }

        public List<UserRoles> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<UserRoles> LayUserRolesTheoUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("ID ngu?i d�ng kh�ng h?p l?");

            return _dal.GetByUserId(userId);
        }

        public void GanRoleChoUser(int userId, int roleId)
        {
            if (userId <= 0)
                throw new ArgumentException("ID ngu?i d�ng kh�ng h?p l?");

            if (roleId <= 0)
                throw new ArgumentException("ID vai tr� kh�ng h?p l?");

            // Ki?m tra d� c� chua
            var existing = _dal.GetById(userId, roleId);
            if (existing != null)
                throw new ArgumentException("Ngu?i d�ng d� c� vai tr� n�y");

            var userRole = new UserRoles
            {
                UserId = userId,
                RoleId = roleId
            };

            _dal.Insert(userRole);
        }

        public void XoaRoleKhoiUser(int userId, int roleId)
        {
            if (userId <= 0)
                throw new ArgumentException("ID ngu?i d�ng kh�ng h?p l?");

            if (roleId <= 0)
                throw new ArgumentException("ID vai tr� kh�ng h?p l?");

            _dal.Delete(userId, roleId);
        }

        public void XoaTatCaRoleCuaUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("ID ngu?i d�ng kh�ng h?p l?");

            _dal.DeleteByUserId(userId);
        }
    }
}
