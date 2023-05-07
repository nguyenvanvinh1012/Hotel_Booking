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
        public static string temp = "";
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
            ViewBag.MoTa = thongTin.MoTa;
            ViewBag.TienNghi = thongTin.TienNghi;
            temp = thongTin.UrlHinhAnh;
            return View(thongTin);
        }
        [HttpPost]
        public ActionResult Edit(Phong phong, string moTa, string tienNghi)
        {
            Phong editPhong = context.Phongs.FirstOrDefault(p => p.Id == phong.Id);
            if (editPhong == null)
            {
                return HttpNotFound("Thông tin không tồn tại...!");
            }
            editPhong.Ten = phong.Ten;
            editPhong.DienTich = phong.DienTich;
            editPhong.GiaThue = phong.GiaThue;
            editPhong.TienNghi = tienNghi;
            editPhong.MoTa = moTa;
            editPhong.LoaiGiuong = phong.LoaiGiuong;
            //ảnh
            if (phong.ImageFile != null && phong.ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(phong.ImageFile.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                phong.ImageFile.SaveAs(filePath);
                phong.UrlHinhAnh = "/Content/Images/" + fileName;
                editPhong.UrlHinhAnh = phong.UrlHinhAnh;
            }
            else
                editPhong.UrlHinhAnh = temp;
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
        public ActionResult Create(Phong phong, string moTa, string tienNghi)
        {
            if (phong.ImageFile != null && phong.ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(phong.ImageFile.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                phong.ImageFile.SaveAs(filePath);
                phong.UrlHinhAnh = "/Content/Images/" + fileName;
            }
            phong.MoTa = moTa;
            phong.TienNghi = tienNghi;
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