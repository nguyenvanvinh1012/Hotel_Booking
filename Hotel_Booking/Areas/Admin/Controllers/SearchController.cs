using Hotel_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;

namespace Hotel_Booking.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {
        // GET: Admin/Search
        public HotelBookingContext context = new HotelBookingContext();
        [HttpPost]
        public ActionResult FindThanhPho(string keyword)
        {
            List<ThanhPho> list = new List<ThanhPho>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                int count = 0;
                List<ThanhPho> list2 = new List<ThanhPho>();
                foreach (var item in context.ThanhPhoes.ToList())
                {
                    list2.Add(item);
                    count++;
                    if (count == 7)
                        break;
                }
                return PartialView("FindThanhPho", list2);

            }
            list = context.ThanhPhoes.AsNoTracking()
                //.Include(a => a.Id)
                .Where(x =>x.Ten.Contains(keyword))
                .OrderByDescending(x => x.Ten)
                .Take(10)
                .ToList();

            if (list == null)
            {
                return PartialView("FindThanhPho", null);
            }
            else
            {
                return PartialView("FindThanhPho", list);
            }
        }

        [HttpPost]
        public ActionResult FindKhachSan(string keyword)
        {
            List<KhachSan> list = new List<KhachSan>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                int count = 0;
                List<KhachSan> list2 = new List<KhachSan>();
                foreach (var item in context.KhachSans.ToList())
                {
                    list2.Add(item);
                    count++;
                    if (count == 7)
                        break;
                }
                return PartialView("FindKhachSan", list2);

            }
            list = context.KhachSans.AsNoTracking()
                //.Include(a => a.LoaiKhachSan)
                .Where(x => x.Ten.Contains(keyword))
                .OrderByDescending(x => x.Ten)
                .Take(10)
                .ToList();

            if (list == null)
            {
                return PartialView("FindKhachSan", null);
            }
            else
            {
                return PartialView("FindKhachSan", list);
            }
        }

        [HttpPost]
        public ActionResult FindPhong(string keyword)
        {
            List<Phong> list = new List<Phong>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                int count = 0;
                List<Phong> list2 = new List<Phong>();
                foreach (var item in context.Phongs.ToList())
                {
                    list2.Add(item);
                    count++;
                    if (count == 7)
                        break;
                }
                return PartialView("FindPhong", list2);

            }
            list = context.Phongs.AsNoTracking()
                //.Include(a => a.Id)
                .Where(x => x.Ten.Contains(keyword))
                .OrderByDescending(x => x.Ten)
                .Take(10)
                .ToList();

            if (list == null)
            {
                return PartialView("FindPhong", null);
            }
            else
            {
                return PartialView("FindPhong", list);
            }
        }

        [HttpPost]
        public ActionResult FindLoaiKhachSan(string keyword)
        {
            List<LoaiKhachSan> list = new List<LoaiKhachSan>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                int count = 0;
                List<LoaiKhachSan> list2 = new List<LoaiKhachSan>();
                foreach (var item in context.LoaiKhachSans.ToList())
                {
                    list2.Add(item);
                    count++;
                    if (count == 7)
                        break;
                }
                return PartialView("FindLoaiKhachSan", list2);

            }
            list = context.LoaiKhachSans.AsNoTracking()
                //.Include(a => a.Id)
                .Where(x => x.Ten.Contains(keyword))
                .OrderByDescending(x => x.Ten)
                .Take(10)
                .ToList();

            if (list == null)
            {
                return PartialView("FindLoaiKhachSan", null);
            }
            else
            {
                return PartialView("FindLoaiKhachSan", list);
            }
        }

        [HttpPost]
        public ActionResult FindDatPhong(string keyword)
        {
            List<DatPhong> list = new List<DatPhong>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                int count = 0;
                List<DatPhong> list2 = new List<DatPhong>();
                foreach (var item in context.DatPhongs.ToList())
                {
                    list2.Add(item);
                    count++;
                    if (count == 7)
                        break;
                }
                return PartialView("FindDatPhong", list2);

            }
            list = context.DatPhongs.AsNoTracking()
                //.Include(a => a.Id)
                .Where(x => x.TaiKhoan.Contains(keyword))
                .OrderByDescending(x => x.TaiKhoan)
                .Take(10)
                .ToList();

            if (list == null)
            {
                return PartialView("FindDatPhong", null);
            }
            else
            {
                return PartialView("FindDatPhong", list);
            }
        }
    }
}