# Hướng dẫn tạo lại Models từ Database

## Có thể tự động tạo lại Models từ Database không?

Có. Entity Framework 6 hỗ trợ reverse engineering (tạo Models từ Database). Có 2 cách chính:

---

## Cách 1: Sử dụng EF Power Tools (Khuyến nghị)

### Bước 1: Cài đặt EF Power Tools
1. Mở Visual Studio
2. Vào Tools -> Extensions and Updates (hoặc Extensions -> Manage Extensions)
3. Tìm kiếm "Entity Framework Power Tools"
4. Cài đặt extension này

### Bước 2: Reverse Engineer Database
1. Click chuột phải vào project NhaKhoa.DAL (hoặc project bạn muốn chứa Models)
2. Chọn Entity Framework -> Reverse Engineer Code First
3. Nhập thông tin kết nối database:
   - Server: (local)\SQLEXPRESS
   - Database: QuanLyPhongKham
   - Chọn authentication (Windows Authentication hoặc SQL Server Authentication)
4. Chọn các bảng bạn muốn tạo Models
5. Chọn namespace: NhaKhoa.Models
6. Click OK

EF Power Tools sẽ tự động:
- Tạo các Model classes từ các bảng trong database
- Tạo DbContext với các DbSet
- Tạo mapping configurations nếu cần

---

## Cách 2: Sử dụng Entity Data Model Wizard (EDMX) rồi convert

### Bước 1: Tạo EDMX từ Database
1. Click chuột phải vào project NhaKhoa.DAL
2. Chọn Add -> New Item
3. Chọn ADO.NET Entity Data Model
4. Chọn Code First from Database
5. Chọn connection string: QuanLyPhongKhamContext
6. Chọn các bảng cần tạo Models
7. Click Finish

### Bước 2: Convert EDMX sang Code-First (nếu cần)
- EDMX sẽ tạo Models tự động, nhưng nếu muốn pure Code-First thì có thể copy Models từ EDMX

---

## Cách 3: Sử dụng Package Manager Console (EF Core - không áp dụng cho EF 6)

Lưu ý: Dự án bạn đang dùng Entity Framework 6, không phải EF Core, nên không thể dùng Scaffold-DbContext.

---

## Thông tin Database hiện tại

Dựa vào App.config, thông tin kết nối:
- Server: (local)\SQLEXPRESS
- Database: QuanLyPhongKham
- Authentication: Windows Authentication (Trusted_Connection=True)

---

## Lưu ý quan trọng

1. Backup trước: Trước khi xóa Models, nên backup để giữ lại các custom attributes và logic đã chỉnh sửa

2. Custom Code: Khi tạo lại từ database, các custom code trong Models (như validation, computed properties, etc.) sẽ bị mất và cần thêm lại

3. Relationships: EF Power Tools sẽ tự động tạo relationships dựa trên foreign keys trong database

4. Naming Convention: Models được tạo có thể khác naming convention hiện tại (ví dụ: BENHNHAN -> BenhNhan)

---

## Script PowerShell để kiểm tra Database Schema

Nếu muốn xem cấu trúc database trước khi tạo lại Models, có thể chạy script sau:

```powershell
# Kết nối và xem danh sách bảng
$connectionString = "Server=(local)\SQLEXPRESS;Database=QuanLyPhongKham;Trusted_Connection=True;TrustServerCertificate=True;"
$query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME"
# Sử dụng sqlcmd hoặc SQL Server Management Studio để xem
```

---

## Kết luận

Có thể tự động tạo lại Models từ Database, nhưng:
- Tự động tạo được structure (properties, keys, relationships)
- Không giữ được custom code, validation logic, computed properties
- Cần review và chỉnh sửa lại sau khi tạo

Khuyến nghị: Sử dụng EF Power Tools vì nó tạo ra Code-First models sạch và dễ maintain hơn.
