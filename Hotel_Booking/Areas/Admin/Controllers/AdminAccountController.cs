using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult SignOut()
        {
            return RedirectToAction("DangXuat", "Account", new { area = "" });
        }
    }
}