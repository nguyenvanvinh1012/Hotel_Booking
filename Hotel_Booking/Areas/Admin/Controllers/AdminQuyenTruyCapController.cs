using Hotel_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Areas.Admin.Controllers
{
    public class AdminQuyenTruyCapController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        public static int ID = 0;
        // GET: Admin/AdminQuyenTruyCap
        public ActionResult Index()
        {
            return View(context.QuyenTruyCaps.ToList());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            QuyenTruyCap thongTin = context.QuyenTruyCaps.FirstOrDefault(p => p.MaQuyenTryCap == id);
            if (thongTin == null)
            {
                return HttpNotFound("Thông tin không tồn tại...!");
            }
            return View(thongTin);
        }
        [HttpPost]
        public ActionResult Edit(QuyenTruyCap quyenTruyCap)
        {
            QuyenTruyCap editQuyenTruyCap = context.QuyenTruyCaps.FirstOrDefault(p => p.MaQuyenTryCap == quyenTruyCap.MaQuyenTryCap);
            if (editQuyenTruyCap == null)
            {
                return HttpNotFound("Thông tin không tồn tại...!");
            }
            editQuyenTruyCap.TenQuyenTryCap = quyenTruyCap.TenQuyenTryCap;
            editQuyenTruyCap.Mota = quyenTruyCap.Mota;
            context.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công !";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(QuyenTruyCap quyenTruyCap)
        {
            context.QuyenTruyCaps.Add(quyenTruyCap);
            context.SaveChanges();
            TempData["Message"] = "Thêm thành công !";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            QuyenTruyCap findThongTin = context.QuyenTruyCaps.FirstOrDefault(p => p.MaQuyenTryCap == id);
            if (findThongTin == null)
            {
                return HttpNotFound("Thông tin không tồn tại!");
            }
            ID = id;
            return View(findThongTin);
        }
        [HttpPost]
        public ActionResult Delete(QuyenTruyCap quyenTruyCap)
        {
            QuyenTruyCap deQuyenTruyCap = context.QuyenTruyCaps.FirstOrDefault(p => p.MaQuyenTryCap == ID);
            if (deQuyenTruyCap == null)
            {
                return HttpNotFound("Thông tin không tồn tại...!");
            }
            context.QuyenTruyCaps.Remove(deQuyenTruyCap);
            context.SaveChanges();
            ID = 0;
            TempData["Message"] = "Xóa thành công !";
            return RedirectToAction("Index");
        }
    }
}