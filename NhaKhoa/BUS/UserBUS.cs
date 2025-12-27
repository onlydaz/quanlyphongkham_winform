using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class UserBUS
    {
        private readonly UserDAL _dal;

        public UserBUS()
        {
            _dal = new UserDAL();
        }

        public List<Models.Users> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<Models.TaiKhoan> LayDanhSachTaiKhoan()
        {
            return _dal.GetAllTaiKhoan();
        }

        public Models.Users LayUserTheoId(int id)
        {
            return _dal.GetById(id);
        }

        public Models.Users LayUserTheoUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Tên đăng nhập không được để trống");

            return _dal.GetByUsername(username);
        }

        public void ThemUser(Models.Users user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Tên đăng nhập không được để trống");

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new ArgumentException("Mật khẩu không được để trống");

            // Kiểm tra trùng username
            var existing = _dal.GetByUsername(user.Username);
            if (existing != null)
                throw new ArgumentException($"Tên đăng nhập {user.Username} đã tồn tại");

            if (user.CreatedAt == default(DateTime))
                user.CreatedAt = DateTime.Now;

            _dal.Insert(user);
        }

        public void CapNhatUser(Models.Users user)
        {
            if (user.Id <= 0)
                throw new ArgumentException("ID người dùng không hợp lệ");

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Tên đăng nhập không được để trống");

            var existing = _dal.GetById(user.Id);
            if (existing == null)
                throw new ArgumentException($"Không tìm thấy người dùng với ID {user.Id}");

            _dal.Update(user);
        }

        public void XoaUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID người dùng không hợp lệ");

            _dal.Delete(id);
        }
    }
}
