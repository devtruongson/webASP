using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseExceptionHandler("/error/500");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "about",
    pattern: "about/*",
    defaults: new { controller = "About", action = "Index" }
);
app.MapControllerRoute(
    name: "system",
    pattern: "system/*",
    defaults: new { controller = "System", action = "Index" }
);
/*
ConnectionService connectionService = new ConnectionService();

if (connectionService.cn != null)
{
    connectionService.cn.Open();
    try
    {
        // Tạo truy vấn SQL để lấy dữ liệu từ bảng admin
        string query = "SELECT * FROM Products LEFT JOIN [All-code] ON Products.location_id = [All-code].id ";

        // Tạo một SqlCommand để thực thi truy vấn
        SqlCommand command = new SqlCommand(query, connectionService.cn);

        // Thực thi truy vấn và nhận một SqlDataReader để đọc dữ liệu
        SqlDataReader reader = command.ExecuteReader();

        // Duyệt qua từng dòng trong kết quả và xử lý dữ liệu
        while (reader.Read())
        {
            // Ví dụ: Đọc dữ liệu từ mỗi cột và in ra console
            string adminId = reader["id"].ToString();
            string username = reader["model"].ToString();
            string password = reader["content_title"].ToString();
            Console.WriteLine($"AdminId: {adminId}, Username: {username}, Password: {password}");
        }

        // Đóng SqlDataReader sau khi hoàn thành
        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.ToString());
    }
    finally
    {
        // Đóng kết nối sau khi sử dụng xong
        connectionService.cn.Close();
    }
}
*/
app.Run();
