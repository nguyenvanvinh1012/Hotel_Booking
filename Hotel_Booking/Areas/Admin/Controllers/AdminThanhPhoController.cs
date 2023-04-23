using Hotel_Booking.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Hotel_Booking.Areas.Admin.Controllers
{
    public class AdminThanhPhoController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        public static int ID = 0;
        public static string temp = "";

        // GET: Admin/AdminThanhPho
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.ThanhPhoes.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }

        //create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ThanhPho thanhPho)
        {
            if (thanhPho.ImageFile != null && thanhPho.ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(thanhPho.ImageFile.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                thanhPho.ImageFile.SaveAs(filePath);
                thanhPho.UrlHinhAnh = "/Content/Images/" + fileName;
            }
            thanhPho.Active = true;
            context.ThanhPhoes.Add(thanhPho);
            context.SaveChanges();
            TempData["Message"] = "Thêm thành công !";
            return RedirectToAction("Index");
        }

        //edit
        public ActionResult Edit(int id)
        {
           ThanhPho thongTinTP = context.ThanhPhoes.FirstOrDefault(p => p.Id == id);
            if (thongTinTP == null)
            {
                return HttpNotFound();
            }
            temp = thongTinTP.UrlHinhAnh;
            ViewBag.Active = thongTinTP.Active;
            return View(thongTinTP);
        }
        [HttpPost]
        public ActionResult Edit(ThanhPho thanhPho, string Active)
        {
           ThanhPho editSanPham = context.ThanhPhoes.FirstOrDefault(p => p.Id == thanhPho.Id);
            if (editSanPham == null)
            {
                return HttpNotFound();
            }

            editSanPham.Ten = thanhPho.Ten;
            editSanPham.MoTa = thanhPho.MoTa;
            if (Active == "on")
                editSanPham.Active = true;
            else
                editSanPham.Active = false;
            //ảnh
            if (thanhPho.ImageFile != null && thanhPho.ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(thanhPho.ImageFile.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                thanhPho.ImageFile.SaveAs(filePath);
                thanhPho.UrlHinhAnh = "/Content/Images/" + fileName;
                editSanPham.UrlHinhAnh = thanhPho.UrlHinhAnh;
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
            ThanhPho thongTinTP = context.ThanhPhoes.FirstOrDefault(p => p.Id == id);
            if (thongTinTP == null)
            {
                return HttpNotFound();
            }
            ID = id;
            return View(thongTinTP);
        }
        [HttpPost]
        public ActionResult Delete(ThanhPho ThanhPho)
        {
            ThanhPho deThanhPho = context.ThanhPhoes.FirstOrDefault(p => p.Id == ID);
            if (deThanhPho == null)
            {
                return HttpNotFound();
            }
            context.ThanhPhoes.Remove(deThanhPho);
            context.SaveChanges();
            ID = 0;
            TempData["Message"] = "Xóa thành công !";
            return RedirectToAction("Index");
        }
    }
}