using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.Models;

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
                throw new ArgumentException("Tên dang nh?p không du?c d? tr?ng");

            return _dal.GetByUsername(username);
        }

        public void ThemUser(Users user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Tên dang nh?p không du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new ArgumentException("M?t kh?u không du?c d? tr?ng");

            // Ki?m tra trùng username
            var existing = _dal.GetByUsername(user.Username);
            if (existing != null)
                throw new ArgumentException($"Tên dang nh?p {user.Username} dã t?n t?i");

            if (user.CreatedAt == default(DateTime))
                user.CreatedAt = DateTime.Now;

            _dal.Insert(user);
        }

        public void CapNhatUser(Users user)
        {
            if (user.Id <= 0)
                throw new ArgumentException("ID ngu?i dùng không h?p l?");

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Tên dang nh?p không du?c d? tr?ng");

            var existing = _dal.GetById(user.Id);
            if (existing == null)
                throw new ArgumentException($"Không tìm th?y ngu?i dùng v?i ID {user.Id}");

            _dal.Update(user);
        }

        public void XoaUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID ngu?i dùng không h?p l?");

            _dal.Delete(id);
        }
    }
}
