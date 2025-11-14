using BookingApp_A12_2025_2026.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;


namespace BookingApp_A12_2025_2026.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestHotelMethods()
        {
            string str = "skill1, skill2,skill3";
            var skills = str.Split(',').Select(s => s.Trim()).ToList();
            //1 Test GetAllHotelsFromDB
            //List<Hotel> holtels = Hotel.GetAllHotelsFromDB();

            //////2 test GetHotelsByCityId
            //Hotel h1 = new Hotel
            //{
            //    City_Id = 1,
            //    Hotel_Name = "A12 Test Hotel",
            //    Hotel_Stars = 4,
            //    Hotel_Photo = "test_photo.jpg",
            //    Hotel_Video = "test_video.mp4",
            //    Hotel_Description = "This is a test hotel.",
            //    Hotel_Lat = 25.276987,
            //    Hotel_Lng = 55.296249,
            //    Hotel_Phone = "+1234567890",
            //    Hotel_Username = "testuser",
            //    Hotel_Password = "testpass",
            //    Hotel_Status = 1
            //};
            //int newHotelId = Hotel.AddNewHotel(h1);

            //3 test DeleteHotelById
            //int x = Hotel.DeleteHotelById(15);

            //4+5 test GetHotelById & UpdateHotel
            //Hotel h2 = Hotel.GetHotelById(7);
            //h2.Hotel_Name += " Updated Hotel Name";
            //h2.Hotel_Description += DateTime.Now.ToLongTimeString();
            //int y = Hotel.UpdateHotel(h2);

            //6 test GetAllHotelByCityId
            // List<Hotel> hotelsInCity = Hotel.GetHotelsByCityId(2);

            //7 test search by name and stars
            //List<Hotel> searchedHotels = Hotel.SearchByHotelNameAndStars("abr", 5);

            return View();
        }   

        public IActionResult AddNewHotelView()
        {
            List<City> cities = City.GetAllCitiesFromDB();
            ViewBag.cities = cities;
            ViewBag.cities = City.GetAllCitiesFromDB();
            
            return View();
        }

        
        

        public IActionResult DoAddNewHotel(Hotel h1, IFormFile Hotel_Photo_File)
        {
            if (Hotel_Photo_File != null)
            {
                 //upload file and get new file name
                var newFileName = UploadFile(Hotel_Photo_File, "Photos").Result;
                h1.Hotel_Photo = "Photos/" + newFileName;
            }
            int x = Hotel.AddNewHotel(h1);
            if (x == -1)
                ViewBag.msg = "حدث خطأ أثناء الاضافة، الرجاء المحاولة لاحقاً";
            else
                ViewBag.msg = "تم اضافة الفندق " + h1.Hotel_Name + " بنجاح";

            List<Hotel> hotels = Hotel.GetAllHotelsFromDB();
            ViewBag.hotels = hotels;
            return View("ManageHotels");
        }

        public IActionResult DeleteHotel(int Hotel_Id)
        {
            int x = Hotel.DeleteHotelById(Hotel_Id);
            if (x == -1)
                ViewBag.msg = "حدث خطأ أثناء الحذف، الرجاء المحاولة لاحقاً";
            else
                ViewBag.msg = "تم حذف الفندق بنجاح";
            List<Hotel> hotels = Hotel.GetAllHotelsFromDB();
            ViewBag.hotels = hotels;
            return View("ManageHotels");
        }

        public IActionResult EditHotel(int Hotel_Id)
        {
            Hotel h1 = Hotel.GetHotelById(Hotel_Id);
            ViewBag.cities = City.GetAllCitiesFromDB();
            return View(h1);
        }

        public IActionResult DoUpdateExistedHotel(Hotel h1, IFormFile Hotel_Photo_File)
        {
            if (Hotel_Photo_File != null)
            {
                //upload file and get new file name
                var newFileName = UploadFile(Hotel_Photo_File, "Photos").Result;
                h1.Hotel_Photo = "Photos/" + newFileName;
            }
            int x = Hotel.UpdateHotel(h1);
            if (x == -1)
                ViewBag.msg = "حدث خطأ أثناء التعديل، الرجاء المحاولة لاحقاً";
            else
                ViewBag.msg = "تم تعديل الفندق " + h1.Hotel_Name + " بنجاح";
            List<Hotel> hotels = Hotel.GetAllHotelsFromDB();
            ViewBag.hotels = hotels;
            return View("ManageHotels");
        }


        public IActionResult HotelDetails(int Hotel_Id)
        {
            Hotel h = Hotel.GetHotelById(Hotel_Id);
            ViewBag.h = h;
            return View(h);
        }

        public IActionResult LoginHotel()
        {
            return View();
        }

        public IActionResult CheckLogin(string Hotel_Username, string Hotel_Password)
        {
            Hotel h1 = Hotel.GetHotelByUsernameAndPassword(Hotel_Username, Hotel_Password);
            if (h1 != null)
            {
                //save hotel data in session
                //HttpContext.Session.SetInt32("Hotel_Id", h1.Hotel_Id);
                //HttpContext.Session.SetString("Hotel_Name", h1.Hotel_Name);
                //return RedirectToAction("Index", "HotelDashboard");

                return View();
            }
            else
            {
                ViewBag.msg = "خطأ في اسم المستخدم أو كلمة المرور، الرجاء المحاولة مجدداً";
                return View("LoginHotel");
            }
        }


        public async Task<IActionResult> CheckLogin2Async(string Hotel_Username, string Hotel_Password)
        {
            Hotel h1 = Hotel.GetHotelByUsernameAndPassword(Hotel_Username, Hotel_Password);
            if (h1 != null) // if hotel login details are correct
            {
                var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, h1.Hotel_Id.ToString()),
                        new Claim(ClaimTypes.Role,"HotelAdmin")
                        };
                var userIdentity = new ClaimsIdentity(claims, "SecureLogin");
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(5),
                    IsPersistent = false,
                    AllowRefresh = false
                });
                /////
                return RedirectToAction("Index", "HotelAdmin");
            }
            else
            {
                ViewBag.msg = "خطأ في اسم المستخدم أو كلمة المرور، الرجاء المحاولة مجدداً";
                return View("LoginHotel");
            }
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
    }
}
