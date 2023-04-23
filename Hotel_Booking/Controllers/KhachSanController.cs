using Hotel_Booking.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Hotel_Booking.Controllers
{
    public class KhachSanController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        // GET: KhanhSan
        public ActionResult Index(int? page, int id)
        {
            ThanhPho tp = context.ThanhPhoes.FirstOrDefault(x => x.Id == id);
            if (tp == null)
            {
                return PartialView("NotFound");
            }

            ViewBag.tp = tp;
            int pageSize = 5;
            int pageIndex = page.HasValue ? page.Value : 1;

            int temp = context.KhachSans.Where(p => p.IdThanhPho == id).Count();
            ViewBag.soks = temp + 462;
            ViewBag.ThanhPho = tp;
            var result = context.KhachSans.Where(p => p.IdThanhPho == id).ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }
        public ActionResult Detail(int id)
        {
            KhachSan khachSan = context.KhachSans.FirstOrDefault(p => p.Id == id);
            if (khachSan == null)
            {
                return PartialView("NotFound");
            }

            return View(khachSan);
        }
    }
}