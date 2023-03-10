using Open_Schooling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Open_Schooling.Controllers
{
    public class ResultController : Controller
    {
        Db_Open_SchoolingEntities db = new Db_Open_SchoolingEntities();
        // GET: Result
        [HttpGet]
        public ActionResult ResultCredentials()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResultCredentials(string seatno, string mother)
        {
            return View();
        }

        public JsonResult VerifyStudent(string seatno, string mother)
        {
            try
            {
                if (seatno.Trim() == "" || mother.Trim() == "")
                {
                    return Json(new { Result = false, Message = "Please Enter Details." }, JsonRequestBehavior.AllowGet);
                }
                var found = db.Tbl_OpenSch_Result.Where(x => x.seatnumber.ToUpper() == seatno.ToUpper() && x.Mother_Name.ToUpper() == mother.Trim().ToUpper()).FirstOrDefault();
                if (found != null)
                {
                    Session["Seat_No"] = found.seatnumber;
                    return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
                }
                else if (found == null)
                {
                    return Json(new { Result = false, Message = "Invalid Seat No / Mobile Number" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
           
        }

        public ActionResult ResultPrint(string Seat_No)
        {
            string ss = Seat_No;//Session["Seat_No"].ToString();
            var tbl = db.Tbl_OpenSch_Result.Where(a => a.seatnumber == ss).FirstOrDefault();
            return View(tbl);
        }
    }
}