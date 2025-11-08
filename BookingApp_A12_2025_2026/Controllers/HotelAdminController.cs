using BookingApp_A12_2025_2026.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp_A12_2025_2026.Controllers
{
    public class HotelAdminController : Controller
    {
        [Authorize (Roles = "HotelAdmin")]
        public IActionResult Index()
        {
            int Hotel_Id = int.Parse(User.Identity.Name);
            Hotel h1=Hotel.GetHotelById(Hotel_Id);
            ViewBag.h1 = h1;
            return View("HotelCP");
        }

        public async Task<IActionResult> LogoutView()
        {
            await HttpContext.SignOutAsync();
            return View();
        }
    }
}
