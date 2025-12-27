using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class RoleBUS
    {
        private readonly RoleDAL _dal;

        public RoleBUS()
        {
            _dal = new RoleDAL();
        }

        public List<Models.Roles> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public Models.Roles LayRoleTheoId(int id)
        {
            return _dal.GetById(id);
        }

        public Models.Roles LayRoleTheoTen(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên vai trò không được để trống");

            return _dal.GetByName(name);
        }

        public void ThemRole(Models.Roles role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("Tên vai trò không được để trống");

            // Kiểm tra trùng tên
            var existing = _dal.GetByName(role.Name);
            if (existing != null)
                throw new ArgumentException($"Vai trò {role.Name} đã tồn tại");

            _dal.Insert(role);
        }

        public void CapNhatRole(Models.Roles role)
        {
            if (role.Id <= 0)
                throw new ArgumentException("ID vai trò không hợp lệ");

            if (string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("Tên vai trò không được để trống");

            var existing = _dal.GetById(role.Id);
            if (existing == null)
                throw new ArgumentException($"Không tìm thấy vai trò với ID {role.Id}");

            _dal.Update(role);
        }

        public void XoaRole(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID vai trò không hợp lệ");

            _dal.Delete(id);
        }
    }
}
