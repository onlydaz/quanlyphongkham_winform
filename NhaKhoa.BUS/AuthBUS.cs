using System;
using NhaKhoa.DAL;
using NhaKhoa.Models;

namespace NhaKhoa.BUS
{
    public class AuthBUS
    {
        private readonly UserDAL _userDal;

        public AuthBUS()
        {
            _userDal = new UserDAL();
        }

        public string Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Vui lòng nh?p d?y d? tên dang nh?p và m?t kh?u!");

            var userInfo = _userDal.GetPasswordHashAndRole(username);
            if (userInfo == null)
                return null;

            var (hash, role) = userInfo.Value;

            bool ok = global::BCrypt.Net.BCrypt.Verify(password, hash);
            return ok ? role : null;
        }
    }
}
