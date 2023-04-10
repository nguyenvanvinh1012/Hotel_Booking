using Hotel_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Areas.Admin.Controllers
{
    public class AdminQuyenTruyCapController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        // GET: Admin/AdminQuyenTruyCap
        public ActionResult Index()
        {
            return View(context.QuyenTruyCaps.ToList());
        }
    }
}