using Microsoft.AspNetCore.Mvc;

namespace BookingApp_A12_2025_2026.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginClient()
        {
            return View();
        }


        public IActionResult CheckLogin(string Client_Username, string Client_Password)
        {
            if(Client_Username=="Admin" && Client_Password=="123456")
            {//

            }
            return View();
        }
    }
}
