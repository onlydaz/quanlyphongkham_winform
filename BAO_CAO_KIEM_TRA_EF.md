# Báo Cáo Kiểm Tra Entity Framework

## KẾT QUẢ KIỂM TRA

### 1. Đồ án CÓ đang dùng Entity Framework

Bằng chứng:
- Có class NhaKhoaContext kế thừa DbContext (Entity Framework 6)
- Có package EntityFramework.6.5.1 trong packages.config
- Có cấu hình Entity Framework trong App.config
- Tất cả các DAL classes sử dụng NhaKhoaContext để truy cập database
- Có DbSet cho các Models: Thuoc, BenhNhan, NhanVien, HoaDon, VatLieu, Users, Roles, UserRoles, ChanDoan, DieuTri, LamSan

Chi tiết:
- Version: Entity Framework 6.5.1
- Approach: Code-First (tạo Models trước, database phải có sẵn)
- DbContext: NhaKhoa.DAL.NhaKhoaContext
- Connection String: QuanLyPhongKhamContext trong App.config

---

### 2. Tự động tạo database/bảng khi chạy

Sau khi thêm code vào Program.cs, ứng dụng sẽ:
- Tự động tạo database QuanLyPhongKham nếu chưa tồn tại
- Tự động tạo các bảng dựa trên Models nếu chưa có
- Không xóa dữ liệu hiện có (chỉ tạo mới nếu chưa có)

Code đã được thêm vào Program.cs:
```csharp
Database.SetInitializer(new CreateDatabaseIfNotExists<NhaKhoaContext>());
```

Lưu ý: Code này sử dụng CreateDatabaseIfNotExists, nghĩa là:
- Chỉ tạo database và bảng nếu chưa tồn tại
- Không xóa hoặc thay đổi dữ liệu hiện có
- An toàn cho cả môi trường development và production

---

## THÔNG TIN DATABASE

Connection String:
```
Server=(local)\SQLEXPRESS
Database=QuanLyPhongKham
Authentication: Windows Authentication (Trusted_Connection=True)
```

Các bảng sẽ được tự động tạo trong database:
1. BENHNHAN
2. NHANVIEN
3. THUOC
4. VATLIEU
5. HOADON
6. USERS
7. ROLES
8. USERROLES
9. CHANDOAN
10. DIEUTRI
11. LAMSAN

---

## HÀNH VI KHI CHẠY ỨNG DỤNG

### Nếu database KHÔNG tồn tại:
- Ứng dụng sẽ tự động tạo database QuanLyPhongKham
- Tự động tạo tất cả các bảng dựa trên Models
- Sau đó chạy bình thường

### Nếu database TỒN TẠI nhưng thiếu bảng:
- Ứng dụng sẽ tự động tạo các bảng còn thiếu
- Giữ nguyên dữ liệu trong các bảng đã có
- Sau đó chạy bình thường

### Nếu database và bảng ĐỀU TỒN TẠI:
- Ứng dụng chạy bình thường
- Không thay đổi gì trong database
- Có thể thực hiện CRUD operations

---

## CÁC LOẠI DATABASE INITIALIZER KHÁC (Nếu cần thay đổi)

Nếu muốn thay đổi hành vi, có thể sửa code trong Program.cs:

1. CreateDatabaseIfNotExists (Đang dùng - AN TOÀN)
   - Chỉ tạo database nếu chưa có
   - Không xóa dữ liệu
   - Phù hợp cho production

2. DropCreateDatabaseIfModelChanges (NGUY HIỂM)
   - Xóa và tạo lại database nếu model thay đổi
   - Sẽ mất hết dữ liệu khi model thay đổi
   - Chỉ dùng cho development

3. DropCreateDatabaseAlways (RẤT NGUY HIỂM)
   - Luôn xóa và tạo lại database mỗi lần chạy
   - Mất hết dữ liệu mỗi lần chạy
   - Chỉ dùng cho testing

Code mẫu để thay đổi:
```csharp
// Tự động tạo lại database nếu model thay đổi (CẨN THẬN - sẽ xóa dữ liệu!)
// Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NhaKhoaContext>());

// Luôn tạo lại database mỗi lần chạy (CẢNH BÁO - sẽ mất hết dữ liệu!)
// Database.SetInitializer(new DropCreateDatabaseAlways<NhaKhoaContext>());
```

---

## KHUYẾN NGHỊ

### Cho môi trường Development:
- Có thể dùng CreateDatabaseIfNotExists để tự động tạo database lần đầu
- Nếu cần test với database sạch, có thể tạm thời dùng DropCreateDatabaseAlways
- Nhớ đổi lại CreateDatabaseIfNotExists sau khi test xong

### Cho môi trường Production:
- Nên dùng CreateDatabaseIfNotExists (an toàn, không mất dữ liệu)
- Hoặc tắt Database Initializer hoàn toàn và tạo database thủ công
- Sử dụng Migrations để quản lý thay đổi schema (nếu cần)

---

## KIỂM TRA NHANH

Để kiểm tra xem database có tồn tại không, chạy lệnh SQL sau trong SQL Server Management Studio:

```sql
-- Kiểm tra database
SELECT name FROM sys.databases WHERE name = 'QuanLyPhongKham'

-- Kiểm tra các bảng
USE QuanLyPhongKham
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'
```

---

## TÓM TẮT

Câu hỏi: Có dùng Entity Framework không?
Trả lời: CÓ - Entity Framework 6.5.1

Câu hỏi: Có tự động tạo database không?
Trả lời: CÓ - Đã thêm code CreateDatabaseIfNotExists vào Program.cs

Câu hỏi: Có tự động tạo bảng không?
Trả lời: CÓ - Tự động tạo bảng dựa trên Models khi database được tạo

Câu hỏi: Có dùng Migrations không?
Trả lời: KHÔNG - Không có Migrations, dùng Database Initializer

Câu hỏi: Cần làm gì trước khi chạy?
Trả lời: KHÔNG CẦN - Ứng dụng sẽ tự động tạo database và bảng nếu chưa có
