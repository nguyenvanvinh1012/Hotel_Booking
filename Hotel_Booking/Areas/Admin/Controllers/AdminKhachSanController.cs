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
    public class AdminKhachSanController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
        public static int ID = 0;
        public static int? sosao;
        public static int? dv;
        public static string temp = "";
        public static string temp1 = "";
        public static string temp2 = "";
        public static string temp3 = "";

        // GET: Admin/AdminKhachSan
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.KhachSans.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListThanhPho = context.ThanhPhoes.ToList();
            ViewBag.ListLoaiKhachSan = context.LoaiKhachSans.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(KhachSan khachSan, string GiapBien, string danhGia,string moTa, string buaAn)

        {
            if (ModelState.IsValid)
            {
                if (khachSan.ImageFile1 != null && khachSan.ImageFile1.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(khachSan.ImageFile1.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    khachSan.ImageFile1.SaveAs(filePath);
                    khachSan.UrlHinhAnh1 = "/Content/Images/" + fileName;
                }

                if (khachSan.ImageFile2 != null && khachSan.ImageFile2.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(khachSan.ImageFile2.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    khachSan.ImageFile2.SaveAs(filePath);
                    khachSan.UrlHinhAnh2 = "/Content/Images/" + fileName;
                }

                if (khachSan.ImageFile3 != null && khachSan.ImageFile3.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(khachSan.ImageFile3.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    khachSan.ImageFile3.SaveAs(filePath);
                    khachSan.UrlHinhAnh3 = "/Content/Images/" + fileName;
                }

                if (khachSan.ImageFile4 != null && khachSan.ImageFile4.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(khachSan.ImageFile4.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    khachSan.ImageFile4.SaveAs(filePath);
                    khachSan.UrlHinhAnh4 = "/Content/Images/" + fileName;
                }

                if (GiapBien == "on")
                    khachSan.GiapBien = true;
                else
                    khachSan.GiapBien = false;

                khachSan.DanhGia = int.Parse(danhGia);
                khachSan.BuaAn = int.Parse(buaAn);
                khachSan.MoTa = moTa;

                context.KhachSans.Add(khachSan);
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
            ViewBag.ListThanhPho = context.ThanhPhoes.ToList();
            ViewBag.ListLoaiKhachSan = context.LoaiKhachSans.ToList();
            KhachSan thongTinKS = context.KhachSans.FirstOrDefault(p => p.Id == id);
            if (thongTinKS == null)
            {
                return HttpNotFound();
            }
            temp = thongTinKS.UrlHinhAnh1;
            temp1 = thongTinKS.UrlHinhAnh2;
            temp2 = thongTinKS.UrlHinhAnh3;
            temp3 = thongTinKS.UrlHinhAnh4;
            sosao = thongTinKS.DanhGia;
            dv = thongTinKS.BuaAn;

            ViewBag.MoTa = thongTinKS.MoTa;
            ViewBag.GiapBien = thongTinKS.GiapBien;
            return View(thongTinKS);
        }
        [HttpPost]
        public ActionResult Edit(KhachSan khachSan,string GiapBien, string danhGia, string moTa, string buaAn)
        {
            KhachSan editkhachSan = context.KhachSans.FirstOrDefault(p => p.Id == khachSan.Id);
            if (editkhachSan == null)
            {
                return HttpNotFound();
            }

            editkhachSan.Ten = khachSan.Ten;
            editkhachSan.DiaChi = khachSan.DiaChi;
            editkhachSan.SoDienThoai = khachSan.SoDienThoai;
            editkhachSan.CachTrungTam = khachSan.CachTrungTam;
            editkhachSan.IdThanhPho = khachSan.IdThanhPho;
            editkhachSan.ThanhPho = khachSan.ThanhPho;
            editkhachSan.IdLoaiKhachSan = khachSan.IdLoaiKhachSan;
            editkhachSan.LoaiKhachSan = khachSan.LoaiKhachSan;
            if (GiapBien == "on")
                editkhachSan.GiapBien = true;
            else
                editkhachSan.GiapBien = false;
            if (int.Parse(danhGia) == -1)
                editkhachSan.DanhGia = sosao;
            else
                editkhachSan.DanhGia = int.Parse(danhGia);
            if (int.Parse(buaAn) == -1)
                editkhachSan.BuaAn = dv;
            else
                editkhachSan.BuaAn = int.Parse(buaAn);
            editkhachSan.MoTa = moTa;

            //image 1
            if (khachSan.ImageFile1 != null && khachSan.ImageFile1.ContentLength > 0)
            {
                var fileName = Path.GetFileName(khachSan.ImageFile1.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                khachSan.ImageFile1.SaveAs(filePath);
                khachSan.UrlHinhAnh1 = "/Content/Images/" + fileName;
                editkhachSan.UrlHinhAnh1= khachSan.UrlHinhAnh1;
            }
            else
            {
                editkhachSan.UrlHinhAnh1 = temp;
            }
            //image 2
            if (khachSan.ImageFile2 != null && khachSan.ImageFile2.ContentLength > 0)
            {
                var fileName = Path.GetFileName(khachSan.ImageFile2.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                khachSan.ImageFile2.SaveAs(filePath);
                khachSan.UrlHinhAnh2 = "/Content/Images/" + fileName;
                editkhachSan.UrlHinhAnh2 = khachSan.UrlHinhAnh2;
            }
            else
            {
                editkhachSan.UrlHinhAnh2 = temp1;
            }
            //image 3
            if (khachSan.ImageFile3 != null && khachSan.ImageFile3.ContentLength > 0)
            {
                var fileName = Path.GetFileName(khachSan.ImageFile3.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                khachSan.ImageFile3.SaveAs(filePath);
                khachSan.UrlHinhAnh3 = "/Content/Images/" + fileName;
                editkhachSan.UrlHinhAnh3 = khachSan.UrlHinhAnh3;
            }
            else
            {
                editkhachSan.UrlHinhAnh3 = temp2;
            }
            //anh 4
            if (khachSan.ImageFile4 != null && khachSan.ImageFile4.ContentLength > 0)
            {
                var fileName = Path.GetFileName(khachSan.ImageFile4.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                khachSan.ImageFile4.SaveAs(filePath);
                khachSan.UrlHinhAnh4 = "/Content/Images/" + fileName;
                editkhachSan.UrlHinhAnh4 = khachSan.UrlHinhAnh4;
            }
            else
            {
                editkhachSan.UrlHinhAnh4 = temp3;
            }
            context.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công !";
            return RedirectToAction("Index");
        }
        //delete
        public ActionResult Delete(int id)
        {
            KhachSan thongTinSP = context.KhachSans.FirstOrDefault(p => p.Id == id);
            if (thongTinSP == null)
            {
                return HttpNotFound();
            }
            ID = id;
            return View(thongTinSP);
        }
        [HttpPost]
        public ActionResult Delete(KhachSan KhachSan)
        {
            KhachSan deKhachSan = context.KhachSans.FirstOrDefault(p => p.Id == ID);
            if (deKhachSan == null)
            {
                return HttpNotFound();
            }
            context.KhachSans.Remove(deKhachSan);
            context.SaveChanges();
            ID = 0;
            TempData["Message"] = "Xóa thành công !";
            return RedirectToAction("Index");
        }
    }
}