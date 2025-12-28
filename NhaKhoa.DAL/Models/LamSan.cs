using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.DAL.Models
{
    [Table("LAMSAN")]
    public class LamSan
    {
        [Key]
        [StringLength(20)]
        public string MaLS { get; set; }

        // Liên kết với BENHNHAN qua MaBN (nvarchar 20)
        public string MaBN { get; set; }

        public DateTime? NgayKham { get; set; } // Default getdate()

        public string TrieuChung { get; set; } // nvarchar(MAX)

        public string MaNV { get; set; } // nvarchar(20)

        // LAMSAN có MaDT và MaCD để liên kết với DIEUTRI và CHANDOAN
        public string MaDT { get; set; } // nvarchar(20)
        public string MaCD { get; set; } // nvarchar(20)

        // --- MAPPING / NAVIGATION PROPERTIES ---
        // Mapping: REFERENCES BENHNHAN(MaBN)
        public virtual BenhNhan BenhNhan { get; set; }

        // Một lượt khám lâm sàng có thể có nhiều Chẩn đoán và Điều trị
        // Nhưng trong database, LAMSAN chỉ có 1 MaDT và 1 MaCD
        [NotMapped]
        public virtual ICollection<ChanDoan> Chandoans { get; set; }
        [NotMapped]
        public virtual ICollection<DieuTri> Dieutris { get; set; }
    }
}






