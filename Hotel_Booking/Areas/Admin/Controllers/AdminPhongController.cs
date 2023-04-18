using Hotel_Booking.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Areas.Admin.Controllers
{
    public class AdminPhongController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        public static int ID = 0;
        // GET: Admin/AdminPhong
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.Phongs.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Phong thongTin = context.Phongs.FirstOrDefault(p => p.Id == id);
            if (thongTin == null)
            {
                return HttpNotFound("Thông tin không tồn tại...!");
            }
            return View(thongTin);
        }
        [HttpPost]
        public ActionResult Edit(Phong phong)
        {
            Phong editPhong = context.Phongs.FirstOrDefault(p => p.Id == phong.Id);
            if (editPhong == null)
            {
                return HttpNotFound("Thông tin không tồn tại...!");
            }
            editPhong.Ten = phong.Ten;
            editPhong.DienTich = phong.DienTich;
            editPhong.GiaThue = phong.GiaThue;
            editPhong.TienNghi = phong.TienNghi;
            editPhong.MoTa = phong.MoTa;
            editPhong.LoaiGiuong = phong.LoaiGiuong;
            context.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công !";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListKhachsan = context.KhachSans.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Phong phong)
        {
            context.Phongs.Add(phong);
            context.SaveChanges();
            TempData["Message"] = "Tạo mới thành công !";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Phong find = context.Phongs.FirstOrDefault(p => p.Id == id);
            if (find == null)
            {
                return HttpNotFound();
            }
            ID = id;
            return View(find);
        }
        [HttpPost]
        public ActionResult Delete(Phong phong)
        {
            Phong findPhong = context.Phongs.FirstOrDefault(p => p.Id == ID);
            List<DatPhong> list = context.DatPhongs.ToList();

            foreach (var item in list)
            {
                DatPhong find = context.DatPhongs.FirstOrDefault(p => p.IdPhong == findPhong.Id);
                context.DatPhongs.Remove(find);
                context.SaveChanges();
            }

            if (findPhong == null)
            {
                return HttpNotFound();
            }
            context.Phongs.Remove(findPhong);
            context.SaveChanges();
            ID = 0;
            TempData["Message"] = "Xóa thành công !";
            return RedirectToAction("Index");
        }
    }
}