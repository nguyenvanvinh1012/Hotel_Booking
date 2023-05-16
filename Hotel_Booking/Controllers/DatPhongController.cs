using Hotel_Booking.Models;
using Hotel_Booking.Other;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking.Controllers
{
    public class DatPhongController : Controller
    {
        HotelBookingContext context = new HotelBookingContext();
       public Info getInfoFromSession()
        {
            Info info = (Info)Session["Info"];
            return info;
        }
        // GET: DatPhong
        [Authorize]
        public ActionResult Confirm(int id)
        {
            Info info = getInfoFromSession();
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

        public ActionResult OrderMomo()
        {
            Info info = getInfoFromSession();
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "Đặt phòng khách sạn";
            string returnUrl = "https://localhost:44361/DatPhong/ConfirmPaymentClient";
            string notifyurl = "https://4c8d-2001-ee0-5045-50-58c1-b2ec-3123-740d.ap.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = info.tongTien.ToString();
            string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        public ActionResult ConfirmPaymentClient(Result result)
        {
            Info info = getInfoFromSession();
            string rMessage = result.message;
            string rOrderId = result.orderId;
            int suscces = int.Parse(result.errorCode);
            if (suscces == 0)
            {
                var tenTaiKhoan = Session["TenTaiKhoan"];
                TaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == tenTaiKhoan);
                DatPhong datPhong = new DatPhong();
                datPhong.TaiKhoan = taiKhoan.TenTaiKhoan;
                datPhong.IdPhong = info.idPhong;
                datPhong.ThanhTien = info.tongTien;
                datPhong.isPaid = true;
                context.DatPhongs.Add(datPhong);
                context.SaveChanges();
                TempData["Message"] = "Giao dịch thành công !!";
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            TempData["MessageErr"] = "Giao dịch thất bại, vui lòng thử lại!";
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}