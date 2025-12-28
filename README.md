# NhaKhoa

Cấu trúc Entity (Bảng) — Mô tả nhanh

Tài liệu này liệt kê cấu trúc các bảng (entity) dựa trên các lớp trong `NhaKhoa.DAL.Models`.
Kiểu dữ liệu được suy ra từ thuộc tính C# và các thuộc tính dữ liệu (`[StringLength]`, `[Column]`, ...).

Lưu ý: đây là mô tả tham khảo — kiểm tra schema thực tế trong database để chính xác.

---

Bảng: BENHNHAN
- MaBN (nvarchar(50)) — PK
- TenBN (nvarchar(MAX))
- GioiTinh (nvarchar(MAX))
- NamSinh (int)
- SDT (nvarchar(MAX))
- DiaChi (nvarchar(MAX))
- NgayKham (datetime)
- LyDoKham (nvarchar(MAX))
- TrangThai (nvarchar(50))

Bảng: CHANDOAN
- MaCD (nvarchar(20)) — PK
- TenChuanDoan (nvarchar(MAX))

Bảng: DIEUTRI
- MaDT (nvarchar(20)) — PK
- MaCD (nvarchar(MAX)) — FK (chẩn đoán)
- TenDieuTri (nvarchar(510))
- DonViTinh (nvarchar(100))
- DonGia (decimal(18,2))

Bảng: LAMSAN
- MaLS (nvarchar(10)) — PK
- MaBN (nvarchar(10)) — FK -> BENHNHAN
- NgayKham (datetime) nullable
- GioBatDau (time) nullable
- GioKetThuc (time) nullable
- TrieuChung (nvarchar(MAX))
- MaNV (nvarchar(10)) — FK -> NHANVIEN
- MaDT (nvarchar(10)) — FK -> DIEUTRI
- MaCD (nvarchar(10)) — FK -> CHANDOAN

Bảng: HOADON
- MaHD (nvarchar(50)) — PK
- NgayLapHD (datetime)
- TongTien (decimal(18,2))
- MaBN (nvarchar(50)) — FK -> BENHNHAN
- MaNV (nvarchar(50)) — FK -> NHANVIEN

Bảng: NHANVIEN
- MaNV (nvarchar(10)) — PK
- TenNV (nvarchar(100))
- MaCV (nvarchar(10))
- SDT (nvarchar(20))
- Email (nvarchar(100))
- NgayVaoLam (date)
- UserId (int?)
- GioiTinh (nvarchar(10))
- DiaChi (nvarchar(255))

Bảng: THUOC
- MaThuoc (nvarchar(50)) — PK
- TenThuoc (nvarchar(200))
- DVT (nvarchar(50))
- DonGia (decimal(18,2))
- SoLuongTon (int)

Bảng: DUNGCU (mapped class `VatLieu`)
- MaDC (nvarchar(50)) — PK
- TenDC (nvarchar(200))
- SoLuong (int)
- DVT (nvarchar(50))
- DonGia (decimal(18,2))

Bảng: Users
- Id (int) — PK
- Username (nvarchar(100))
- PasswordHash (nvarchar(255))
- FullName (nvarchar(200))
- Email (nvarchar(200))
- IsActive (bit)
- CreatedAt (datetime)

Bảng: Roles
- Id (int) — PK
- Name (nvarchar(100))

Bảng: UserRoles
- UserId (int) — FK -> Users(Id)
- RoleId (int) — FK -> Roles(Id)

Bảng: TaiKhoan (hỗ trợ/DTO)
- Id (int)
- Username (nvarchar(100))
- FullName (nvarchar(200))
- Email (nvarchar(200))
- IsActive (bit)
- Status (nvarchar(MAX))
- Roles (nvarchar(MAX))

Bảng: LICHBACSI
- Id (int) — PK
- MaBacSi (nvarchar(10)) — FK -> NHANVIEN.MaNV
- NgayTrongTuan (tinyint)
- GioBatDau (time)
- GioKetThuc (time)
- GhiChu (nvarchar(MAX))
- IsActive (bit)
- NgayTao (datetime?)
- NguoiTao (nvarchar(100))

---

Ghi chú kỹ thuật:
- Kiểu C# -> kiểu SQL suy đoán: `string` -> `nvarchar(...)` (hoặc `nvarchar(MAX)` nếu không chỉ định), `int` -> `int`, `decimal` -> `decimal(18,2)`, `DateTime` -> `datetime`, `TimeSpan` -> `time`, `bool` -> `bit`, `byte` -> `tinyint`.
- Các độ dài dùng `StringLength` trong model nếu có; nếu không có, tôi dùng `nvarchar(MAX)` hoặc một giá trị hợp lý.
- Nếu bạn cần file xuất chi tiết hơn (ví dụ loại cột NULLABLE/NOT NULL, FK constraints hoặc kiểu chính xác trong DB), cho mình biết — mình sẽ sinh bản đầy đủ.