using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.DAL.Models
{
    [Table("CHANDOAN")]
    public class ChanDoan
    {
        [Key]
        [StringLength(20)]
        public string MaCD { get; set; }

        // Theo ERD mới: CHANDOAN không có cột MaLS trong database
        // Liên kết được quản lý ở bảng LAMSAN (LAMSAN có MaCD và MaDT)
        [NotMapped]
        public string MaLS { get; set; }

        public string TenChuanDoan { get; set; } // nvarchar(MAX)

        // --- MAPPING / NAVIGATION PROPERTIES ---
        // Theo ERD mới: Không có foreign key từ CHANDOAN đến LAMSAN
        // Relationship được quản lý ngược lại: LAMSAN -> CHANDOAN
        [NotMapped]
        public virtual LamSan Lamsan { get; set; }

        // Một Chẩn đoán có thể có nhiều dòng Điều trị chi tiết
        public virtual ICollection<DieuTri> Dieutris { get; set; }
    }
}






