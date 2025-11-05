using BookingApp_A12_2025_2026.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;
using System.Diagnostics;

namespace BookingApp_A12_2025_2026.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
           



            //Connector cn = new Connector("AppData/BookingDB.accdb");
            //int x = cn.RunScalar("select count(*) from Cities");
            //OleDbDataReader od = cn.RunSelect("select * from Cities");
            ////
            var gemini = new GeminiService();
            List<Student> students = Student.GetDemoStudents();
            var listText = string.Join("\n- ", students);
            var prompt = $"هذه قائمة طلاب:\n- {listText}\n\nهل يمكنك ترتيبهم أبجدياً حسب الاسم وعرضهم في جدول؟";
            //prompt = "write short story in arabic for 4th class";
            //prompt = "just the final output int x=10; int y=9; c.wl(x*y);";
            //prompt = "just the final number: كم كالوري في ساندويش فلافل";
            //prompt = "just the final number: كم كالوري في صحن كبير من مقلوبة فلسطينية";
            //prompt = "show just the final number of calories: كم كالوري في باجيت متوسط مع 250 غرام شوراما";
            //prompt = "no details' just show th final amount of calories: شربت كاس نسكافيه مكون من 200 ملم حليب وملعقتين نس وملعقة عسل صغيرة";
            //prompt = "in arabic: ماهي فؤائد اكلة المسخن الفلسطيني";
            //prompt = "أكتب باللغة العربية وبدون مقدمات: نصيحة ذهبية للطلاب";
            prompt = "أكتب باللغة العربية وبدون مقدمات: بيت شعر مشهور";



            var result = await gemini.AskAsync(prompt);

            ViewBag.Prompt = prompt;
            ViewBag.Response = result;
            //

            Student st1 = new Student
            {
                Id = 100,
                Name = "Omar",
                Sora = "Photos/st11.png"
            };

            ViewBag.st1 = st1;




            int t = City.GetTotalCities();
            ViewBag.t = t;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShowCities()
        {
            ////Call the GetAllCity Method from the City Model
            List<City> cities = City.GetAllCitiesFromDB();
            //then send the List via ViewBag
            ViewBag.cities = cities;
            return View();
        }

        public IActionResult ManageCities()
        {
            //City ct = new City
            //{
            //    City_Name = "Test",
            //    City_Location = "11111",
            //    City_Photo = "7483748",
            //    City_Description = "Test Desc",
            //    City_Video = "47387483",
            //    City_IsSafe = true,
            //};
            //int x = City.AddCityToDB(ct);


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

        //public IActionResult CityDetails(int City_Id)
        //{
        //    City ct = City.GetCityById(City_Id);
        //    ViewBag.ct = ct;
        //       return View();
        //}


        public IActionResult Index1()
        {
            int t = City.GetTotalCities();
            ViewBag.t = t;
            return View();
        }
        public IActionResult ShowStudents()
        {

            List<Student> lst1 = Student.GetDemoStudents();
            //ViewData["students"] = students;

            ViewBag.lst1 = lst1;
            return View();
        }

        public IActionResult AddNewCityView()
        {
            return View();
        }


        public IActionResult DoAddNewCity2(City ct)
        {
            int x = City.AddCityToDB(ct);
            if(x>0)
                ViewBag.msg = "تمت إضافة المدينة بنجاح";
            else
                ViewBag.msg = "حدث خطأ أثناء الإضافة، الرجاء المحاولة لاحقاً";
            List<City> cities = City.GetAllCitiesFromDB();
            ViewBag.cities = cities;
            return View("ManageCities");
        }

        public IActionResult DoAddNewCity(City ct,IFormFile City_Photo_File)
        {
            //if City Photo File Was seletced 
            if (City_Photo_File != null)
            {
                Task<string> tmp = UploadFile(City_Photo_File, "Photos");
                ct.City_Photo = "Photos/"+tmp.Result;
            }
           int x= City.AddCityToDB(ct);
            if (x == -1)
                ViewBag.msg = "حدث خطأ أثناء الاضافة، الرجاء المحاولة لاحقاً";
            else
                ViewBag.msg = "تم اضافة مدينة "+ct.City_Name+" بنجاح";
            List<City> cities = City.GetAllCitiesFromDB();
            ViewBag.cities = cities;
            return View("ManageCities");
           // return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public IActionResult CityUpdate(int City_Id)
        {

            City ct= City.GetCityById(City_Id);
            return View(ct);
        }


        public IActionResult DoUpdateExistedCity(City ct,IFormFile City_Photo_File)
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
