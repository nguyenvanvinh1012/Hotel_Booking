using Hotel_Booking.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Hotel_Booking.Controllers
{
    public class KhachSanController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        public static int ID;
        public static int ID2;
        // GET: KhanhSan
        public ActionResult Index(int? page, int id)
        {
            ThanhPho tp = context.ThanhPhoes.FirstOrDefault(x => x.Id == id);
            ID = id;
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
            Info info = (Info)Session["Info"];
            if (info != null)
            {
                ViewBag.ngayden = info.ngayDen;
                ViewBag.ngaydi = info.ngayTra;
            }
            var result = context.KhachSans.Where(p => p.IdThanhPho == id).ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }
        public ActionResult Detail(int id)
        {
            KhachSan khachSan = context.KhachSans.FirstOrDefault(p => p.Id == id);
            Info info = (Info)Session["Info"];
            if (info != null)
            {
                ViewBag.ngayden = info.ngayDen;
                ViewBag.ngaydi = info.ngayTra;
            }
            if (khachSan == null)
            {
                return PartialView("NotFound");
            }
            ID2 = id;
            //kiem tra cho nay
            ViewBag.ListPhong = context.Phongs.Where(p => p.IdKhachSan == khachSan.Id).ToList();
            return View(khachSan);
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
            if (!string.IsNullOrEmpty(keyword))
            {
                string searchKeyword = RemoveDiacritics(keyword);
                var findTP2 = context.ThanhPhoes.ToList().Where(p => RemoveDiacritics(p.Ten).ToLower().Contains(searchKeyword.ToLower())).FirstOrDefault();
                if (findTP2 != null)
                {
                    return RedirectToAction("Index", "KhachSan", new { id = findTP2.Id });
                }
            }
            return PartialView("NotFound");
        }
        public ActionResult SearchDetail(DateTime ngayden, DateTime ngaydi)
        {
            if (ngaydi.ToUniversalTime() < ngayden.ToUniversalTime())
            {
                return RedirectToAction("Index", "KhachSan", new { id = ID });
            }
            return RedirectToAction("Detail", "KhachSan", new { id = ID2 });
        }
        public ActionResult Filter(int[] starRating)
        {
            var filteredHotels = context.KhachSans.ToList();

            if (starRating != null && starRating.Length > 0)
            {

            }

            return View(filteredHotels);
        }
    }
}