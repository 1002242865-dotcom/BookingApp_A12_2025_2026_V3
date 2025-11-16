using BookingApp_A12_2025_2026.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;
using System.Diagnostics;
using System.Security.Claims;

namespace BookingApp_A12_2025_2026.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //هذه العملية هدفها الاساسي هو التمرن واستخدام جيميني
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

            //prompt=" ما هي عاصمة فرنسا؟ اعطني معلومات عن اهم فرق كرة القدم فيها";

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

        
         
         public IActionResult LoginView()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminCP", "Admin");  
            }
            else
                if (User.Identity.IsAuthenticated && User.IsInRole("HotelAdmin"))
            {
                return RedirectToAction("Index", "HotelAdmin");
            }
            else
                if (User.Identity.IsAuthenticated && User.IsInRole("ClientAdmin"))
            {
                return RedirectToAction("AdminCP", "Admin");
            }

            return View();
        }
        public async Task<IActionResult> CheckLoginAsync(string The_Username, string The_Password)
        {
            if (The_Password == "123456" && The_Username == "Admin")
            {
                var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, "1"),
                        new Claim(ClaimTypes.Role,"Admin")
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
                return RedirectToAction("AdminCP", "Admin");
            }
            else
            {


                Hotel h1 = Hotel.GetHotelByUsernameAndPassword(The_Username, The_Password);
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
                    Client c1 = Client.GetClientByUsernameAndPassword(The_Username, The_Password);
                    if (c1 != null) // if hotel login details are correct
                    {
                        var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, c1.Client_Id.ToString()),
                        new Claim(ClaimTypes.Role,"ClientlAdmin")
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
                        ViewBag.c1 = c1;
                        return RedirectToAction("ClientCP", "ClientlAdmin");
                    }
                    else
                    { 
                        ViewBag.msg = "خطأ في اسم المستخدم أو كلمة المرور، الرجاء المحاولة مجدداً";
                        return View("LoginView");
                    }
                }
            }
        }
        public async Task<IActionResult> LogoutView()
        {
            await HttpContext.SignOutAsync();
            ViewBag.msg = "تم تسجيل الخروج بنجاح";
            return View("LoginView");
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoSignUpAsync(Client c1, IFormFile Client_Photo_File)
        {
            c1.Client_Photo = "Photos/avatar.png";
            if(Client_Photo_File != null)
            {
                Task<string> tmp = UploadFile(Client_Photo_File, "Photos");
                c1.Client_Photo = "Photos/" + tmp.Result;
            }
            int x = Client.AddClient(c1);
            if (x == 0)
            {
                ViewBag.ErrorMessage = "هنالك مشكلة في التسجيل";
                return View("SignUp");
            }
            if(x==1)
            {
                //تسجيل الدخول
                 c1 = Client.GetClientByUsernameAndPassword(c1.Client_Username, c1.Client_Password);
                if (c1 != null) // if hotel login details are correct
                {
                    var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, c1.Client_Id.ToString()),
                        new Claim(ClaimTypes.Role,"ClientlAdmin")
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
                    ViewBag.c1 = c1;
                    return RedirectToAction("ClientCP", "ClientlAdmin");
                }
                else
                {
                    ViewBag.msg = "خطأ في اسم المستخدم أو كلمة المرور، الرجاء المحاولة مجدداً";
                    return View("LoginView");
                }
            }
            // من هنا تقوم بحفظ البيانات
            return View("SignUp");
        }


        public IActionResult Privacy()
        {
            return View();
        }

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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult TestClient()
        {
            //Client cn = new Client { Client_Birthdate = DateTime.Now, Client_Name = "Test", Client_Password = "123", Client_Photo = "888", Client_Username = "Tester" };
           // int c=Client.AddClient(cn);


            //Client c1=Client.GetClientByUsernameAndPassword("Yazeed", "123123");
            //int a = Client.DeleteClient(4);
            List<Client> clients = Client.GetAllClients();
            return View();
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
