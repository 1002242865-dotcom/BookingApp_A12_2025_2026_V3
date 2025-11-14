using BookingApp_A12_2025_2026.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp_A12_2025_2026.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult AdminCP()
        {
            return View();
        }

        public IActionResult ManageCities()
        {
            List<City> cities = City.GetAllCitiesFromDB();
            ViewBag.cities = cities;
            return View();
        }

        public IActionResult DeleteCity(int City_Id)
        {
            int x = City.DeleteCityById(City_Id);
            if (x == -1)
                ViewBag.msg = "حدث خطأ أثناء الحذف، الرجاء المحاولة لاحقاً";
            else if (x == 0)
                ViewBag.msg = "لم يتم حذف اي سجل";
            else
                ViewBag.msg = "تم حذف " + x + " سجلات بنجاح";
            //حسب قيمة x 
            //نقرر كيف والى اين نكمل
            List<City> cities = City.GetAllCitiesFromDB();
            ViewBag.cities = cities;
            return View("ManageCities");
        }


        public async Task<IActionResult> CityDetailsAsync(int City_Id)
        {
            City ct = City.GetCityById(City_Id);
            ViewBag.ct = ct;
            var gemini = new GeminiService();
            string prompt = "give details about 100 words in arabic about this city " + ct.City_Name;
            var result = await gemini.AskAsync(prompt);
            ViewBag.Response = result;
            return View();
        }


        public IActionResult AddNewCityView()
        {
            return View();
        }

        public IActionResult DoAddNewCity(City ct, IFormFile City_Photo_File)
        {
            //if City Photo File Was seletced 
            if (City_Photo_File != null)
            {
                Task<string> tmp = UploadFile(City_Photo_File, "Photos");
                ct.City_Photo = "Photos/" + tmp.Result;
            }
            int x = City.AddCityToDB(ct);
            if (x == -1)
                ViewBag.msg = "حدث خطأ أثناء الاضافة، الرجاء المحاولة لاحقاً";
            else
                ViewBag.msg = "تم اضافة مدينة " + ct.City_Name + " بنجاح";
            List<City> cities = City.GetAllCitiesFromDB();
            ViewBag.cities = cities;
            return View("ManageCities");
            // return View();
        }


        public IActionResult CityUpdate(int City_Id)
        {

            City ct = City.GetCityById(City_Id);
            return View(ct);
        }


        public IActionResult DoUpdateExistedCity(City ct, IFormFile City_Photo_File)
        {
            //if City Photo File Was seletced 
            if (City_Photo_File != null)
            {
                Task<string> tmp = UploadFile(City_Photo_File, "Photos");
                ct.City_Photo = "Photos/" + tmp.Result;
            }
            int x = City.UpdateCityInDB(ct);
            if (x == -1)
                ViewBag.msg = "حدث خطأ أثناء التعديل، الرجاء المحاولة لاحقاً";
            else
                ViewBag.msg = "تم تعديل مدينة " + ct.City_Name + " بنجاح";

            List<City> cities = City.GetAllCitiesFromDB();
            ViewBag.cities = cities;
            return View("ManageCities");
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
