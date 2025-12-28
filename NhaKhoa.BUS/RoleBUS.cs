using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

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
                throw new ArgumentException("T�n vai tr� kh�ng du?c d? tr?ng");

            return _dal.GetByName(name);
        }

        public void ThemRole(Roles role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("T�n vai tr� kh�ng du?c d? tr?ng");

            // Ki?m tra tr�ng t�n
            var existing = _dal.GetByName(role.Name);
            if (existing != null)
                throw new ArgumentException($"Vai tr� {role.Name} d� t?n t?i");

            _dal.Insert(role);
        }

        public void CapNhatRole(Roles role)
        {
            if (role.Id <= 0)
                throw new ArgumentException("ID vai tr� kh�ng h?p l?");

            if (string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("T�n vai tr� kh�ng du?c d? tr?ng");

            var existing = _dal.GetById(role.Id);
            if (existing == null)
                throw new ArgumentException($"Kh�ng t�m th?y vai tr� v?i ID {role.Id}");

            _dal.Update(role);
        }

        public void XoaRole(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID vai tr� kh�ng h?p l?");

            _dal.Delete(id);
        }
    }
}
