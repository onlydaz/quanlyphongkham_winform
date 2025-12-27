CREATE TABLE LICHBACSI (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MaBacSi NVARCHAR(10) NOT NULL,
    NgayTrongTuan TINYINT NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    GhiChu NVARCHAR(500) NULL,
    IsActive BIT NOT NULL DEFAULT(1),
    NgayTao DATETIME NULL,
    NguoiTao NVARCHAR(100) NULL,
    CONSTRAINT FK_LichBacSi_NhanVien FOREIGN KEY (MaBacSi) REFERENCES NHANVIEN(MaNV)
);

-- LƯU Ý: Ràng buộc FK trên cột MaBacSi phụ thuộc vào kiểu khóa chính của bảng NHANVIEN.
-- Model `NhanVien` trong code có `MaNV` (string) và `UserId` (int?). Nếu bạn muốn FK tới `NHANVIEN.MaNV`, chuyển `MaBacSi` sang cùng kiểu dữ liệu.
-- Nếu DB không có trường `UserId` trong NHANVIEN, hãy chỉnh sửa constraint phù hợp (ví dụ REFERENCES NHANVIEN(MaNV)).
