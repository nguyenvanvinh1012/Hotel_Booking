using Hotel_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account


        public ActionResult DangNhap(String email)
        {

                return View();
        }
    }
}