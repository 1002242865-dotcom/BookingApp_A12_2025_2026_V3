using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookingApp_A12_2025_2026
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // 1 Shadi: اضافة خدمة  Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); //Shadi: set session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //////

            // 2 Shadi: إضافة خدمة المصادقة باستخدام ملفات تعريف الارتباط
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/LoginView"; // الصفحة التي يوجّه إليها عند الحاجة لتسجيل الدخول
                    options.LogoutPath = "/Home/LogoutView"; // صفحة الخروج
                    options.AccessDeniedPath = "/Home/AccessDenied"; // صفحة الرفض إن لم يكن لديه صلاحية
                });

            builder.Services.AddControllersWithViews();
            /////

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // 3 Shadi: اضافة خدمة تسجيل الدخول
            app.UseAuthentication();
            /////
            //4 Shadi: تفعيل خدمة الجلسات
            app.UseSession();
            ////

            app.UseHttpsRedirection();
            app.UseStaticFiles();
           app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
