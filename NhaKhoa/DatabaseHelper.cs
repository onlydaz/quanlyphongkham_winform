using Microsoft.Data.SqlClient;

namespace NhaKhoa.GUI
{
    public static class DatabaseHelper
    {
        // SỬA SERVER CHO PHÙ HỢP VỚI MÁY BẠN
        public static string ConnectionString =
            @"Server=(local)\SQLEXPRESS;Database=QuanLyPhongKham;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}