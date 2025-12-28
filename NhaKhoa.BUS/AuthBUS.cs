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

        public string Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Vui l�ng nh?p d?y d? t�n dang nh?p v� m?t kh?u!");

            var userInfo = _userDal.GetPasswordHashAndRole(username);
            if (userInfo == null)
                return null;

            var (hash, role) = userInfo.Value;

            bool ok = global::BCrypt.Net.BCrypt.Verify(password, hash);
            return ok ? role : null;
        }
    }
}
