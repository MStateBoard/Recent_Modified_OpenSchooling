using Open_Schooling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Open_Schooling.Controllers
{
    public class Exam_HallTikitController : Controller
    {
        Db_Open_SchoolingEntities db = new Db_Open_SchoolingEntities();
        int x;
        string y, stand;
        // GET: Exam_HallTikit
        public ActionResult Halltikit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Halltikit(Tbl_Registration tbl_Registration)
        {
            try
            {
               
                var IsValid = db.Tbl_Registration.Where(c => c.ApplicationId == tbl_Registration.ApplicationId && c.Mobile_No == tbl_Registration.Mobile_No).FirstOrDefault();
                var div = db.Center_Login_Information.Where(x => x.Contact_Center_Code == IsValid.Center_Code).FirstOrDefault();
                if (IsValid != null)
                {
                    Session["FormNo"] = IsValid.ApplicationId;
                    Session["Mobile_No"] = IsValid.Mobile_No;
                    Session["Center_Code"] = IsValid.Center_Code;
                    Session["Division"] = div.Division;
                    ViewData["Class"] = IsValid.Standard;
                    return RedirectToAction("HalltikitPrint");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Details.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string NumberToWords(int number)
        {
            try
            {
                if (number == 0)
                    return "zero";

                if (number < 0)
                    return "minus " + NumberToWords(Math.Abs(number));

                string words = "";

                if ((number / 1000000) > 0)
                {
                    words += NumberToWords(number / 1000000) + " million ";
                    number %= 1000000;
                }

                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " thousand ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += NumberToWords(number / 100) + " hundred ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != "")
                        words += "and ";

                    var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                    var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)

                            words += " " + unitsMap[number % 10];
                    }
                }

                //string seat = lblseatno.Text.Substring(0, 2);
                if (stand == "5")
                {
                    ViewData["Class"] = y + " ZERO FIVE" + "-" + words.ToUpper();
                }
                else if (stand == "8")
                {
                    ViewData["Class"] = y + " ZERO EIGHT" + "- " + words.ToUpper();
                }
                return words;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult HalltikitPrint(Tbl_Registration os_Form_Data)

        {
            try
            {
                var formNo = "";
                if (os_Form_Data.ApplicationId!=null)
                {
                    formNo = os_Form_Data.ApplicationId;
                   
                }
                else
                {
                    formNo = Session["FormNo"].ToString();
                }
               
               
                
               
                
                os_Form_Data = db.Tbl_Registration.Where(os => os.ApplicationId == formNo && os.Ec_Status=="Completed").FirstOrDefault();

                
                if (os_Form_Data == null)
                {
                    ViewData["ErrorMsg"] = "Invalid credentials";
                    return View("Halltikit");
                }

                os_Form_Data.Seat_No = "MH0810014";
                var sub1 = db.Tbl_Subject.Where(os => os.Subject_Code == os_Form_Data.Subject1).FirstOrDefault();
                var sub2 = db.Tbl_Subject.Where(os => os.Subject_Code == os_Form_Data.Subject2).FirstOrDefault();
                var sub3 = db.Tbl_Subject.Where(os => os.Subject_Code == os_Form_Data.Subject3).FirstOrDefault();
                var sub4 = db.Tbl_Subject.Where(os => os.Subject_Code == os_Form_Data.Subject4).FirstOrDefault();
                var sub5 = db.Tbl_Subject.Where(os => os.Subject_Code == os_Form_Data.Subject5).FirstOrDefault();
                TempData["sub1"] = sub1.Subject_Name;
                TempData["sub2"] = sub2.Subject_Name;
                TempData["sub3"] = sub3.Subject_Name;
                TempData["sub4"] = sub4.Subject_Name;
                TempData["sub5"] = sub5.Subject_Name;



                x = Int32.Parse(os_Form_Data.Seat_No.Substring(4, 5));


                //y = hall_Tikit.seatnumber.Substring(0, 2);
                stand = os_Form_Data.Seat_No.Substring(3, 1);
                NumberToWords(x);
               

                return View(os_Form_Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
