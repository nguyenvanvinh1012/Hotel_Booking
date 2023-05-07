using Hotel_Booking.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Controllers
{
    public class LoaiKhachSanController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        // GET: LoaiKhachSan
        public ActionResult Index(int? page, int id)
        {
            LoaiKhachSan loaiKhachSan = context.LoaiKhachSans.FirstOrDefault(x => x.Id == id);
            if (loaiKhachSan == null)
            {
                return PartialView("NotFound");
            }

            int pageSize = 5;
            int pageIndex = page.HasValue ? page.Value : 1;

            int temp = context.KhachSans.Where(p => p.IdLoaiKhachSan == id).Count();
            ViewBag.soks = temp + 462;
            ViewBag.loaiks = loaiKhachSan;

            var result = context.KhachSans.Where(p => p.IdLoaiKhachSan == id).ToList().ToPagedList(pageIndex, pageSize);
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