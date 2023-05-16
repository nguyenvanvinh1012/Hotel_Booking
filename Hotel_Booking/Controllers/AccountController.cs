using Hotel_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hotel_Booking.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        HotelBookingContext context = new HotelBookingContext();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(TaiKhoan tk,String xacnhanmatkhau, string matkhau)
        {
            TaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == tk.TenTaiKhoan);
            if (ModelState.IsValid)
            {
                if (taiKhoan == null)
                {
                    if(xacnhanmatkhau == matkhau)
                    {
                        TaiKhoan taiKhoan1 = new TaiKhoan
                        {
                            TenTaiKhoan = tk.TenTaiKhoan,
                            MatKhau = matkhau,
                            HoTen = tk.HoTen,
                            SoDienThoai = tk.SoDienThoai,
                            Email = tk.Email,
                            MaQuyenTryCap = 3,
                            Active = true,

                        };
                        context.TaiKhoans.Add(taiKhoan1);
                        context.SaveChanges();
                        TempData["Message"] = "Đăng ký thành công !";
                        return RedirectToAction("DangNhap", "Account", new { area = "" });
                    }
                    else
                    {
                        TempData["MessageErr"] = "Xác nhận mật khẩu không hợp lệ !";
                        return RedirectToAction("DangKy", "Account", new { area = "" });
                    }
                }
                else
                {
                    TempData["MessageErr"] = "Tên tài khoản đã tồn tại! Vui lòng sử dụng tên tài khoản khác";
                    return RedirectToAction("DangKy", "Account", new { area = "" });
                }
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(String tentaikhoan, String matkhau)
        {
            if (ModelState.IsValid)
            {
                bool userE = context.TaiKhoans.Any(x => x.TenTaiKhoan == tentaikhoan && x.MatKhau == matkhau);
                TaiKhoan t = context.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == tentaikhoan && x.MatKhau == matkhau);
                if (userE)
                {
                    if (t.MaQuyenTryCap == 3)
                    {
                        if (t.Active == false)
                        {
                            TempData["MessageErr"] = "Tài khoản đã bị chặn!!"; 
                            return RedirectToAction("DangNhap", "Account", new { area = "" });
                        }
                        else
                        {
                            Session["TenTaiKhoan"] = t.TenTaiKhoan;
                            FormsAuthentication.SetAuthCookie(t.TenTaiKhoan, false);
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }
                    }
                    else if (t.MaQuyenTryCap == 1 || t.MaQuyenTryCap == 2)
                    {
                        FormsAuthentication.SetAuthCookie(t.TenTaiKhoan, false);
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }
                TempData["MessageErr"] = "Tài khoản hoặc mật khẩu sai!!";
                return RedirectToAction("DangNhap", "Account", new { area = "" });
            }
            else
            {
                return RedirectToAction("DangNhap");
            }         
        }
        public ActionResult DangXuat()
        {
            Session["TenTaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap");
        }
    }
}