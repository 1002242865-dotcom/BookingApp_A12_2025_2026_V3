using BookingApp_A12_2025_2026.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp_A12_2025_2026.Controllers
{

    [Authorize(Roles = "ClientAdmin")]
    public class ClientAdminController : Controller
    {
        public IActionResult ClientCP()
        {
            int Client_Id = int.Parse(User.Identity.Name);
           Client c1=Client.GetClientById(Client_Id);
            ViewBag.c1 = c1;
            return View();
        }
    }
}
