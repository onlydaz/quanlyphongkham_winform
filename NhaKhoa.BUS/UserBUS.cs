using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.BUS
{
    public class UserBUS
    {
        private readonly UserDAL _dal;

        public UserBUS()
        {
            _dal = new UserDAL();
        }

        public List<Users> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<TaiKhoan> LayDanhSachTaiKhoan()
        {
            return _dal.GetAllTaiKhoan();
        }

        public Users LayUserTheoId(int id)
        {
            return _dal.GetById(id);
        }

        public Users LayUserTheoUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("T�n dang nh?p kh�ng du?c d? tr?ng");

            return _dal.GetByUsername(username);
        }

        public void ThemUser(Users user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("T�n dang nh?p kh�ng du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new ArgumentException("M?t kh?u kh�ng du?c d? tr?ng");

            // Ki?m tra tr�ng username
            var existing = _dal.GetByUsername(user.Username);
            if (existing != null)
                throw new ArgumentException($"T�n dang nh?p {user.Username} d� t?n t?i");

            if (user.CreatedAt == default(DateTime))
                user.CreatedAt = DateTime.Now;

            _dal.Insert(user);
        }

        public void CapNhatUser(Users user)
        {
            if (user.Id <= 0)
                throw new ArgumentException("ID ngu?i d�ng kh�ng h?p l?");

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("T�n dang nh?p kh�ng du?c d? tr?ng");

            var existing = _dal.GetById(user.Id);
            if (existing == null)
                throw new ArgumentException($"Kh�ng t�m th?y ngu?i d�ng v?i ID {user.Id}");

            _dal.Update(user);
        }

        public void XoaUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID ngu?i d�ng kh�ng h?p l?");

            _dal.Delete(id);
        }
    }
}
