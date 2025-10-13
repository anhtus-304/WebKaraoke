# Dự án WebKaraoke

Hệ thống quản lý quán karaoke trên nền tảng web, được phát triển cho môn học Nhập môn Công nghệ Phần mềm. Dự án cho phép khách hàng đặt phòng, gọi món và nhân viên quản lý vận hành.

## Công nghệ sử dụng

* **Backend:** ASP.NET Core Web API (C#)
* **Frontend:** HTML, CSS, TypeScript
* **Database:** Microsoft SQL Server
* **Kiến trúc:** 3 lớp (Presentation, Business Logic, Data Access)
* **ORM:** Entity Framework Core

## Yêu cầu cài đặt

Trước khi bắt đầu, hãy đảm bảo bạn đã cài đặt các công cụ sau:
* .NET 8 SDK (hoặc phiên bản tương ứng với project)
* SQL Server Express (hoặc phiên bản bất kỳ)
* Một trình soạn thảo code như Visual Studio hoặc VS Code

## Hướng dẫn chạy dự án

1.  **Sao chép (clone) repository:**
    ```bash
    git clone [https://github.com/TonyTheSlacker/WebKaraoke.git](https://github.com/TonyTheSlacker/WebKaraoke.git)
    cd WebKaraoke
    ```

2.  **Cấu hình kết nối CSDL:**
    * Mở file `WebKaraoke.API/appsettings.json`.
    * Cập nhật chuỗi kết nối `DefaultConnection` với thông tin SQL Server của bạn.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=TEN_SERVER_CUA_BAN;Database=WebKaraokeDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
    }
    ```

3.  **Cài đặt CSDL:**
    * Chạy script SQL để tạo CSDL (file này sẽ được đặt tại `/database-scripts/init.sql` - bạn sẽ tạo file này sau) trên SQL Server để tạo database và các bảng cần thiết.

4.  **Xây dựng và chạy dự án:**
    * Mở terminal trong thư mục `WebKaraoke.API`.
    * Chạy ứng dụng bằng lệnh sau:
    ```bash
    dotnet run
    ```
    * API sẽ chạy trên địa chỉ `https://localhost:

