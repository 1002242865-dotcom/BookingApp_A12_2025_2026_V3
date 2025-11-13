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

        public IActionResult EditHotel()
        {
            int Hotel_Id = int.Parse(User.Identity.Name);
            Hotel h1 = Hotel.GetHotelById(Hotel_Id);
            List<City> cities=City.GetAllCitiesFromDB();
            ViewBag.cities = cities;
            ViewBag.h1 = h1;
            return View(h1);
        }

        public IActionResult DoUpdateExistedHotel(Hotel h1, IFormFile Hotel_Photo_File)
        {
            int Hotel_Id = int.Parse(User.Identity.Name);
            h1.Hotel_Id= Hotel_Id;
            if (Hotel_Photo_File != null)
            {
                //upload file and get new file name
                var newFileName = UploadFile(Hotel_Photo_File, "Photos").Result;
                h1.Hotel_Photo = "Photos/" + newFileName;
            }
            int x = Hotel.UpdateHotelIncludeUsername(h1);
            if (x == -1)
                ViewBag.msg = "حدث خطأ أثناء التعديل، الرجاء المحاولة لاحقاً";
            else
                ViewBag.msg = "تم تعديل الفندق " + h1.Hotel_Name + " بنجاح";
           
            return View("HotelCP");
        }


        private async Task<string> UploadFile(IFormFile f1, string folder)
        {

            //Where to Save File and change file name
            var baseFolder = "wwwroot";
            var newFileName = DateTime.Now.Ticks.ToString() + "_" + f1.FileName;
            var filePath = baseFolder + "/" + folder + "/" + newFileName;

            ///stream is good for large files size
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //async meaning in other thread
                await f1.CopyToAsync(stream);
            }
            return newFileName;
        }


        public async Task<IActionResult> LogoutView()
        {
            await HttpContext.SignOutAsync();
            return View();
        }
    }
}
