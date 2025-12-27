using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.Models
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

        // Khóa ngoại liên kết tới DIEUTRI
        public string MaDT { get; set; }

        // Khóa ngoại liên kết tới CHANDOAN
        public string MaCD { get; set; }

        // --- MAPPING / NAVIGATION PROPERTIES ---
        // Mapping: REFERENCES BENHNHAN(MaBN)
        public virtual BenhNhan BenhNhan { get; set; }

        // Mapping: REFERENCES DIEUTRI(MaDT) - một LAMSAN có một DIEUTRI
        public virtual DieuTri DieuTri { get; set; }

        // Mapping: REFERENCES CHANDOAN(MaCD) - một LAMSAN có một CHANDOAN
        public virtual ChanDoan ChanDoan { get; set; }
    }
}
