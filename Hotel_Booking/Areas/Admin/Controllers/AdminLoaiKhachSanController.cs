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
    public class AdminLoaiKhachSanController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        public static int ID = 0;
        public static string temp = "";
        // GET: Admin/AdminLoaiKhachSan
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.LoaiKhachSans.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }
        //create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LoaiKhachSan loaiKhachSan)
        {
            if (ModelState.IsValid)
            {
                if (loaiKhachSan.ImageFile != null && loaiKhachSan.ImageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(loaiKhachSan.ImageFile.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    loaiKhachSan.ImageFile.SaveAs(filePath);
                    loaiKhachSan.UrlHinhAnh = "/Content/Images/" + fileName;
                }
                loaiKhachSan.Active = true;
                context.LoaiKhachSans.Add(loaiKhachSan);
                context.SaveChanges();
                TempData["Message"] = "Thêm thành công !";
                return RedirectToAction("Index");

            }  
            else
            {
                return RedirectToAction("Create");
            }
               
        }

        //edit
        public ActionResult Edit(int id)
        {
            LoaiKhachSan thongTinTP = context.LoaiKhachSans.FirstOrDefault(p => p.Id == id);
            if (thongTinTP == null)
            {
                return HttpNotFound();
            }
            temp = thongTinTP.UrlHinhAnh;
            return View(thongTinTP);
        }
        [HttpPost]
        public ActionResult Edit(LoaiKhachSan loaiKhachSan)
        {
            LoaiKhachSan editSanPham = context.LoaiKhachSans.FirstOrDefault(p => p.Id == loaiKhachSan.Id);
            if (editSanPham == null)
            {
                return HttpNotFound();
            }

            editSanPham.Ten = loaiKhachSan.Ten;
            editSanPham.MoTa = loaiKhachSan.MoTa;
            //ảnh
            if (loaiKhachSan.ImageFile != null && loaiKhachSan.ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(loaiKhachSan.ImageFile.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                loaiKhachSan.ImageFile.SaveAs(filePath);
                loaiKhachSan.UrlHinhAnh = "/Content/Images/" + fileName;
                editSanPham.UrlHinhAnh = loaiKhachSan.UrlHinhAnh;
            }
            else
                editSanPham.UrlHinhAnh = temp;

            context.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công !";
            return RedirectToAction("Index");
        }
        //delete
        public ActionResult Delete(int id)
        {
            LoaiKhachSan thongTinKS = context.LoaiKhachSans.FirstOrDefault(p => p.Id == id);
            if (thongTinKS == null)
            {
                return HttpNotFound();
            }
            ID = id;
            return View(thongTinKS);
        }
        [HttpPost]
        public ActionResult Delete(LoaiKhachSan loaiKhachSanKhachSan)
        {
            LoaiKhachSan deLoai = context.LoaiKhachSans.FirstOrDefault(p => p.Id == ID);
            if (deLoai == null)
            {
                return HttpNotFound();
            }
            context.LoaiKhachSans.Remove(deLoai);
            context.SaveChanges();
            ID = 0;
            TempData["Message"] = "Xóa thành công !";
            return RedirectToAction("Index");
        }
    }
}