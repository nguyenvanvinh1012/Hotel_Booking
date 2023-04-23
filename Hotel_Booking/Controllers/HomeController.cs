using Hotel_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Controllers
{
    public class HomeController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        public ActionResult Index()
        {
            var listTP = context.ThanhPhoes.Where(p => p.Active == true).ToList();
            ViewBag.ListLoaKS = context.LoaiKhachSans.Where(p => p.Active == true).ToList();
            ViewBag.TPHCM = context.ThanhPhoes.FirstOrDefault(p => p.Ten == "TP. Hồ Chí Minh");
            ViewBag.HaNoi = context.ThanhPhoes.FirstOrDefault(p => p.Ten == "Hà Nội");
            ViewBag.DaNang = context.ThanhPhoes.FirstOrDefault(p => p.Ten == "Đà Nẵng");
            ViewBag.Hue = context.ThanhPhoes.FirstOrDefault(p => p.Ten == "Huế");
            ViewBag.HoiAn = context.ThanhPhoes.FirstOrDefault(p => p.Ten == "Hội An");
            return View(listTP);
        }

    }
}