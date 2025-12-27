using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.Models;

namespace NhaKhoa.BUS
{
    public class RoleBUS
    {
        private readonly RoleDAL _dal;

        public RoleBUS()
        {
            _dal = new RoleDAL();
        }

        public List<Roles> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public Roles LayRoleTheoId(int id)
        {
            return _dal.GetById(id);
        }

        public Roles LayRoleTheoTen(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên vai trò không du?c d? tr?ng");

            return _dal.GetByName(name);
        }

        public void ThemRole(Roles role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("Tên vai trò không du?c d? tr?ng");

            // Ki?m tra trùng tên
            var existing = _dal.GetByName(role.Name);
            if (existing != null)
                throw new ArgumentException($"Vai trò {role.Name} dã t?n t?i");

            _dal.Insert(role);
        }

        public void CapNhatRole(Roles role)
        {
            if (role.Id <= 0)
                throw new ArgumentException("ID vai trò không h?p l?");

            if (string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("Tên vai trò không du?c d? tr?ng");

            var existing = _dal.GetById(role.Id);
            if (existing == null)
                throw new ArgumentException($"Không tìm th?y vai trò v?i ID {role.Id}");

            _dal.Update(role);
        }

        public void XoaRole(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID vai trò không h?p l?");

            _dal.Delete(id);
        }
    }
}
