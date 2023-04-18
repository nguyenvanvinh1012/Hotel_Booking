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
        HotelBookingContext context = new HotelBookingContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(TaiKhoan taiKhoan)
        {
            //context.TaiKhoans.Add(taiKhoan);
            //context.SaveChanges();
            //return RedirectToAction("DangNhap");
            if (ModelState.IsValid)
            {
                var check = context.TaiKhoans.FirstOrDefault(s => s.Email == taiKhoan.Email);
                if (check == null)
                {
                    //taiKhoan.MatKhau = GetMD5(taiKhoan.MatKhau);
                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.TaiKhoans.Add(taiKhoan);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại! Vui lòng sử dụng email khác";
                    return View();
                }


            }
            return View();
        }
    }
}