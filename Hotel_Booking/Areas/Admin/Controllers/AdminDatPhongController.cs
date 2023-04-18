using Hotel_Booking.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Areas.Admin.Controllers
{
    public class AdminDatPhongController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        public static int ID = 0;
        // GET: Admin/AdminDatPhong
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.DatPhongs.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }
        public ActionResult Delete(int id)
        {
            DatPhong find = context.DatPhongs.FirstOrDefault(p => p.Id == id);
            if (find == null)
            {
                return HttpNotFound();
            }
            ID = id;
            return View(find);
        }
        [HttpPost]
        public ActionResult Delete(DatPhong datPhong)
        {
            DatPhong find = context.DatPhongs.FirstOrDefault(p => p.Id == ID);
            if (find == null)
            {
                return HttpNotFound();
            }
            context.DatPhongs.Remove(find);
            context.SaveChanges();
            ID = 0;
            TempData["Message"] = "Xóa thành công !";
            return RedirectToAction("Index");
        }
    }
}