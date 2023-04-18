using Hotel_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Areas.Admin.Controllers
{
    public class AdminTaiKhoanController : Controller
    {
        // GET: Admin/AdminTaiKhoan
        HotelBookingContext context = new HotelBookingContext();
        public ActionResult Index()
        {
            return View(context.TaiKhoans.ToList());
        }
    }
}