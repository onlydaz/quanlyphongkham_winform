using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.Models
{
    [Table("DIEUTRI")]
    public class DieuTri
    {
        [Key]
        [StringLength(20)]
        public string MaDT { get; set; }

        // DIEUTRI không có cột MaLS trong database
        // Liên kết được quản lý ở bảng LAMSAN (LAMSAN có MaDT và MaCD)
        [NotMapped]
        public string MaLS { get; set; }

        // Khóa ngoại liên kết tới CHANDOAN (có thể null)
        public string MaCD { get; set; }

        public string TenDieuTri { get; set; } // nvarchar(510)
        public string DonViTinh { get; set; } // nvarchar(100)
        public decimal? DonGia { get; set; } // decimal(18, 2)

        // --- MAPPING / NAVIGATION PROPERTIES ---
        // Mapping: FK_DIEUTRI_LAMSAN REFERENCES LAMSAN(MaLS)
        public virtual LamSan Lamsan { get; set; }

        // Mapping: FK_DIEUTRI_CHANDOAN REFERENCES CHANDOAN(MaCD)
        public virtual ChanDoan Chandoan { get; set; }

        // Mapping: Bảng này được tham chiếu bởi ChiTietHoaDon(MaDT)
        // public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
