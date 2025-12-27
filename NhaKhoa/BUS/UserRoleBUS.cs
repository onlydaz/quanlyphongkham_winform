using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class UserRoleBUS
    {
        private readonly UserRoleDAL _dal;

        public UserRoleBUS()
        {
            _dal = new UserRoleDAL();
        }

        public List<Models.UserRoles> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<Models.UserRoles> LayUserRolesTheoUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("ID người dùng không hợp lệ");

            return _dal.GetByUserId(userId);
        }

        public void GanRoleChoUser(int userId, int roleId)
        {
            if (userId <= 0)
                throw new ArgumentException("ID người dùng không hợp lệ");

            if (roleId <= 0)
                throw new ArgumentException("ID vai trò không hợp lệ");

            // Kiểm tra đã có chưa
            var existing = _dal.GetById(userId, roleId);
            if (existing != null)
                throw new ArgumentException("Người dùng đã có vai trò này");

            var userRole = new Models.UserRoles
            {
                UserId = userId,
                RoleId = roleId
            };

            _dal.Insert(userRole);
        }

        public void XoaRoleKhoiUser(int userId, int roleId)
        {
            if (userId <= 0)
                throw new ArgumentException("ID người dùng không hợp lệ");

            if (roleId <= 0)
                throw new ArgumentException("ID vai trò không hợp lệ");

            _dal.Delete(userId, roleId);
        }

        public void XoaTatCaRoleCuaUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("ID người dùng không hợp lệ");

            _dal.DeleteByUserId(userId);
        }
    }
}
