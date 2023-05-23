using Hotel_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hotel_Booking.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        HotelBookingContext context = new HotelBookingContext();
        public string MH(string mh)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(mh);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }
            return (hasPass);
        }
        public ActionResult Index()
        {
            var TK = Session["TenTaiKhoan"];
            if (TK != null)
            {
                var taikhoan = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == TK);
                if (taikhoan != null)
                {
                    var lsDatPhong = context.DatPhongs.Where(p => p.TaiKhoan == taikhoan.TenTaiKhoan).ToList();
                    ViewBag.DatPhong = lsDatPhong;
                    return View(taikhoan);
                }

            }
            return RedirectToAction("DangNhap");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var TK = Session["TenTaiKhoan"];
            if (TK != null)
            {
                var taikhoan = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == TK);
                if (taikhoan != null)
                {
                    var lsDatPhong = context.DatPhongs.Where(p => p.TaiKhoan == taikhoan.TenTaiKhoan).ToList();
                    ViewBag.DatPhong = lsDatPhong;
                    return View(taikhoan);
                }

            }
            return RedirectToAction("DangNhap");
        }

        [HttpPost]
        public ActionResult Edit(TaiKhoan taiKhoan, string MKhienTai, string xacNhanMKmoi, string MKmoi)
        {
            TaiKhoan tk = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == taiKhoan.TenTaiKhoan);
            TaiKhoan tk1 = new TaiKhoan();
            if (tk != null)
            {
                if (MKhienTai == "")
                {
                    tk.HoTen = taiKhoan.HoTen;
                    tk.SoDienThoai = taiKhoan.SoDienThoai;
                    tk.Email = taiKhoan.Email;
                    context.SaveChanges();
                    TempData["Message"] = "Chỉnh sửa thành công !";
                    return RedirectToAction("Edit", "Account");
                }
                else
                {
                    if (MH(MKhienTai) == tk.MatKhau && MKmoi != "")
                    {
                        if (MH(MKmoi) != tk.MatKhau && MH(xacNhanMKmoi) == MH(MKmoi))
                        {
                            tk.HoTen = taiKhoan.HoTen;
                            tk.SoDienThoai = taiKhoan.SoDienThoai;
                            tk.Email = taiKhoan.Email;
                            tk.MatKhau = MH(MKmoi);
                            context.SaveChanges();
                            TempData["Message"] = "Chỉnh sửa thành công !";
                            return RedirectToAction("Edit", "Account");
                        }
                        else
                        {
                            TempData["MessageErr"] = "Xác nhận sai mật khẩu mới !";
                            return RedirectToAction("Edit", "Account");
                        }
                    }
                    else
                    {
                        TempData["MessageErr"] = "Mật khẩu chưa đúng !";
                        return RedirectToAction("Edit", "Account");
                    }
                }

            }
            return HttpNotFound();
        }
        public ActionResult Oder()
        {
            var TK = Session["TenTaiKhoan"];
            if (TK != null)
            {
                var taikhoan = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == TK);
                if (taikhoan != null)
                {
                    var lsDatPhong = context.DatPhongs.Where(p => p.TaiKhoan == taikhoan.TenTaiKhoan).ToList();
                    ViewBag.DatPhong = lsDatPhong;
                    return View(taikhoan);
                }

            }
            return RedirectToAction("DangNhap");
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(TaiKhoan tk, String xacnhanmatkhau, string matkhau)
        {
            TaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == tk.TenTaiKhoan);
            if (ModelState.IsValid)
            {
                if (taiKhoan == null)
                {
                    if (xacnhanmatkhau == matkhau)
                    {
                        TaiKhoan taiKhoan1 = new TaiKhoan
                        {
                            TenTaiKhoan = tk.TenTaiKhoan,
                            MatKhau = MH(matkhau),
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
            var mhmatkhau = MH(matkhau);
            if (ModelState.IsValid)
            {
                bool userE = context.TaiKhoans.Any(x => x.TenTaiKhoan == tentaikhoan && x.MatKhau == mhmatkhau);
                TaiKhoan t = context.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == tentaikhoan && x.MatKhau == mhmatkhau);
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