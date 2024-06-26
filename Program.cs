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
    name: "blog",
    pattern: "blog/*",
    defaults: new { controller = "Blog", action = "thuexemay" }
);
app.MapControllerRoute(
    name: "travel",
    pattern: "travel/*",
    defaults: new { controller = "Travel", action = "blogdulich" }
);
app.MapControllerRoute(
   name: "product",
    pattern: "product/*",
    defaults: new { controller = "Product", action = "ProductDetail" }
);
app.MapControllerRoute(
    name: "system",
    pattern: "system/*",
    defaults: new { controller = "System", action = "Index" }
);
app.MapControllerRoute(
    name: "allcode",
    pattern: "allcode/*",
    defaults: new { controller = "AllCode", action = "Index" }
);
app.MapControllerRoute(
    name: "rentedbike",
    pattern: "rentedbike/*",
    defaults: new { controller = "RentedBike", action = "Index" }
);
app.MapControllerRoute(
    name: "export",
    pattern: "export/*",
    defaults: new { controller = "Export", action = "Index" }
);
app.MapControllerRoute(
    name: "pricecars",
    pattern: "pricecars/*",
    defaults: new { controller = "PriceCars", action = "Index" }
);

app.MapControllerRoute(
    name: "infoadmin",
    pattern: "infoadmin/*",
    defaults: new { controller = "InfoAdmin", action = "Index" }
);

app.MapControllerRoute(
    name: "managerbike",
    pattern: "managerBike/*",
    defaults: new { controller = "ManagerBike", action = "Index" }
);
app.Run();
