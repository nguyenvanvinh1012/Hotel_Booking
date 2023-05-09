using Hotel_Booking.Models;
using PagedList;
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
        public static string tentaikhoan = null;
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.TaiKhoans.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
            //return View(context.TaiKhoans.ToList());
        }

        [HttpGet]
        public ActionResult Disable(string tk)
        {
            TaiKhoan findThongTin = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == tk);
            if (findThongTin == null)
            {
                return HttpNotFound("Thông tin không tồn tại!");
            }
            tentaikhoan = tk;
            return View(findThongTin);
        }
        [HttpPost]
        public ActionResult Disable(TaiKhoan fkhachHang)
        {
            TaiKhoan dekhachhang = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == tentaikhoan);
            if (dekhachhang == null)
            {
                return HttpNotFound("Thông tin không tồn tại...!");
            }
            if (dekhachhang.Active == true)
            {
                dekhachhang.Active = false;
                context.SaveChanges();
                TempData["Message"] = "Chặn tài khoản thành công !";
            }
            else
            {
                dekhachhang.Active = true;
                context.SaveChanges();
                TempData["Message"] = "Mở chặn tài khoản thành công !";
            }
            return RedirectToAction("Index");
        }

    }
}