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

        public ActionResult DangKy(String tentaikhoan,String matkhau,String hoten,String sodienthoai,String email,String xacnhanmatkhau)
        {
            TaiKhoan tk = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == tentaikhoan);
            if (ModelState.IsValid)
            {
                if (tk == null)
                {
                    if(xacnhanmatkhau == matkhau)
                    {
                        TaiKhoan taiKhoan1 = new TaiKhoan
                        {
                            TenTaiKhoan = tentaikhoan,
                            MatKhau = matkhau,
                            HoTen = hoten,
                            SoDienThoai = sodienthoai,
                            Email = email,
                            MaQuyenTryCap = 3,
                            Active = true,

                        };
                        context.TaiKhoans.Add(taiKhoan1);
                        context.SaveChanges();
                        TempData["Message"] = "Đăng ký thành công !";
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else
                    {
                        TempData["MessageErr"] = "Xác nhận mật khẩu không hợp lệ !";
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                }
                else
                {
                    ViewBag.error = "Tên tài khoản đã tồn tại! Vui lòng sử dụng tên tài khoản khác";
                    return View();
                }
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

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
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }
                        else
                        {
                            Session["TenTaiKhoan"] = t.TenTaiKhoan;
                            FormsAuthentication.SetAuthCookie(t.TenTaiKhoan, false);
                            //return RedirectToAction("Dashborad", "Account");
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }
                    }
                    else if (t.MaQuyenTryCap == 1 || t.MaQuyenTryCap == 2)
                    {
                        FormsAuthentication.SetAuthCookie(t.TenTaiKhoan, false);
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }
                TempData["MessageErr"] = "UserName or PassWord is wrong!!";
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap");
            }         
        }
    }
}