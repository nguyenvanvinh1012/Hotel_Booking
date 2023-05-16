using Hotel_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using UnidecodeSharpFork;
using System.Globalization;
using System.Data.Common;
using System.Data.SqlClient;

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
        public ActionResult Contact()
        {
            

            return View();
        }
        public static string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
        public ActionResult Search(string keyword, DateTime ngayden, DateTime ngaydi)
        {
            Info info = new Info();
            info.ngayDen = ngayden;
            info.ngayTra = ngaydi;
            Session["Info"] = info;
            int kt = DateTime.Compare(ngayden, ngaydi);
            if(ngaydi.ToUniversalTime() < ngayden.ToUniversalTime())
            {
                TempData["MessageErr"] = "Ngày không hợp lệ!!";
                return RedirectToAction("Index", "Home");
            }
            string searchKeyword = RemoveDiacritics(keyword);
            if (!string.IsNullOrEmpty(keyword))
            {
                var findTP = context.ThanhPhoes.Where(p => p.Ten.Contains(keyword)).FirstOrDefault();
                if (findTP != null)
                {
                    return RedirectToAction("Index", "KhachSan", new { id = findTP.Id });
                }
                else
                {
                    var findTP2 = context.ThanhPhoes.ToList().Where(p => RemoveDiacritics(p.Ten).ToLower().Contains(keyword.ToLower())).FirstOrDefault();
                    if (findTP2 != null)
                    {
                        return RedirectToAction("Index", "KhachSan", new { id = findTP2.Id });
                    }
                    return PartialView("NotFound");
                }
            }
            return PartialView("NotFound");

        }

    }
}