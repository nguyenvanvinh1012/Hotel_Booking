using GoogleMaps.LocationServices;
using Hotel_Booking.Models;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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
        private readonly string apiKey = "AIzaSyAmOTTdm9TwR98k9vaolqGuONg-FaUX2lk";
        private readonly HttpClient httpClient = new HttpClient();
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
        public bool checkEmptyRoom(Phong p, DateTime ngayDen, DateTime ngayTra)
        {
            //ngayDen.CompareTo(tmp.NgayTra) > 0) || ngayTra.CompareTo(tmp.NgayDen) < 0
            var listDatPhong = context.DatPhongs.ToList();
            if (listDatPhong != null)
            {
                foreach(DatPhong tmp in listDatPhong)
                {
                    if (tmp.IdPhong == p.Id)
                    {
                        if (!(ngayDen > tmp.NgayTra || ngayTra < tmp.NgayDen))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public ActionResult Detail(int id)
        {
            KhachSan khachSan = context.KhachSans.FirstOrDefault(p => p.Id == id);
            ID2 = id;
            Info info = (Info)Session["Info"];

            var encodedAddress = Uri.EscapeDataString(khachSan.DiaChi);
            var apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={encodedAddress}&key={apiKey}";

            var response = httpClient.GetAsync(apiUrl).Result;
            response.EnsureSuccessStatusCode();

            var responseContent = response.Content.ReadAsStringAsync().Result;
            dynamic result = JsonConvert.DeserializeObject(responseContent);

            if (result.status == "OK" && result.results.Count > 0)
            {
                var location = result.results[0].geometry.location;
                var latitude = (double)location.lat;
                var longitude = (double)location.lng;

                if (info != null)
                {
                    ViewBag.ngayden = info.ngayDen;
                    ViewBag.ngaydi = info.ngayTra;
                    List<Phong> ListPhong = new List<Phong>();
                    var DSPhong = context.Phongs.Where(p => p.IdKhachSan == khachSan.Id).ToList();

                    foreach (Phong tmp in DSPhong)//id = 1, id = 2
                    {
                        if (checkEmptyRoom(tmp, info.ngayDen, info.ngayTra))
                        {
                            ListPhong.Add(tmp);
                        }
                    }

                    ViewBag.Latitude = latitude;
                    ViewBag.Longitude = longitude;
                    ViewBag.ListPhong = ListPhong;
                    return View(khachSan);
                }

                ViewBag.Latitude = latitude;
                ViewBag.Longitude = longitude;
                ViewBag.ListPhong = context.Phongs.Where(p => p.IdKhachSan == khachSan.Id).ToList();
                return View(khachSan);
            }

            if (khachSan == null)
            {
                return PartialView("NotFound");
            }

            ViewBag.ListPhong = context.Phongs.Where(p => p.IdKhachSan == khachSan.Id).ToList();
            return View(khachSan);
        }
        [Authorize]
        public ActionResult Confirm(int id)
        {
            Info info = (Info)Session["Info"];
            if (info == null)
            {
                TempData["MessageErr"] = "Vui lòng kiểm tra phòng trống khi đặt phòng!!";
                return RedirectToAction("Detail", "KhachSan", new { id = ID2 });
            }
            var tenTaiKhoan = Session["TenTaiKhoan"];
            var taiKhoan = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == tenTaiKhoan);
            Phong Phong = context.Phongs.FirstOrDefault(p => p.Id == id);
            KhachSan ks = context.KhachSans.FirstOrDefault(p => p.Id == Phong.IdKhachSan);
            ThanhPho tp = context.ThanhPhoes.FirstOrDefault(p => p.Id == ks.IdThanhPho);
            ViewBag.NgayDen = info.ngayDen;
            ViewBag.NgayDi = info.ngayTra;
            DateTime ngayden = info.ngayDen;
            DateTime ngaydi = info.ngayTra;
            ViewBag.ThanhPho = tp;
            ViewBag.Phong = Phong;
            info.idPhong = id;
            info.giaThue = (int)Phong.GiaThue;
            ViewBag.TongTien = info.tongTien;
            return View(taiKhoan);
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
            Info info = new Info();
            info.ngayDen = ngayden;
            info.ngayTra = ngaydi;
            Session["Info"] = info;
            return RedirectToAction("Detail", "KhachSan", new { id = ID2 });
        }
    }
}