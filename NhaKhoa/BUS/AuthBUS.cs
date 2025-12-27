using System;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class AuthBUS
    {
        private readonly UserDAL _userDal;

        public AuthBUS()
        {
            _userDal = new UserDAL();
        }

        // Returns (UserId, Role) if success, otherwise (null, null)
        public (int? UserId, string Role) Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!");

            var userInfo = _userDal.GetPasswordHashAndRole(username);
            if (userInfo == null)
                return (null, null);

            var (userId, hash, role) = userInfo.Value;

            bool ok = global::BCrypt.Net.BCrypt.Verify(password, hash);
            if (ok)
                return (userId, role);
            return ((int?)null, (string)null);
        }
    }
}
