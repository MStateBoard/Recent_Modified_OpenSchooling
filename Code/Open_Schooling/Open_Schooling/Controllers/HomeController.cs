using Open_Schooling.Helper;
using Open_Schooling.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Open_Schooling.Controllers
{
    public class HomeController : Controller
    {
        Db_Open_SchoolingEntities db = new Db_Open_SchoolingEntities();
        Common common = new Common();
        SqlConnection _Con;
        SqlCommand _Command;
        public ActionResult HomeDashboard()
        {
            var res = db.Tbl_Site_Status.ToList().OrderByDescending(x => x.ID).FirstOrDefault();

            if (res.RegisterEorD == "Enable")
            {
                Session["Registration"] = 1;
            }
            



            return View();
        }

        public ActionResult Registration()
        {
            bindDistrict();
            return View();
        }

        [HttpPost]
        public JsonResult Registration(Tbl_Registration_Pre model)
        {
            try
            {
                Tbl_Registration_Pre tbl_Subjects = common.getSubList(model);
                model.Subject_List = tbl_Subjects.Subject_List;
                model.Subject1 = tbl_Subjects.Subject1;
                model.Subject2 = tbl_Subjects.Subject2;
                model.Subject3 = tbl_Subjects.Subject3;
                model.Subject4 = tbl_Subjects.Subject4;
                model.Subject5 = tbl_Subjects.Subject5;
             

                string hostName = Dns.GetHostName();
                model.ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
                model.DateNow = DateTime.Now;
                Random r = new Random();
                int genRand = r.Next(1000, 9999);
                if (model.Standard == "5th")
                {
                    model.ApplicationId = "MSBOSPR05" + Common.Get_Year_Id() + genRand;
                }
                else
                {
                    model.ApplicationId = "MSBOSPR08" + Common.Get_Year_Id() + genRand;
                }
              
              
                model.Year_Id = Common.Get_Year();
                db.Tbl_Registration_Pre.Add(model);
                db.SaveChanges();


                var data = db.Tbl_Registration_Pre.Where(x => x.ApplicationId == model.ApplicationId).FirstOrDefault();
                if (model.Upload_Photo != null || model.Upload_Sign != null || model.AgeCertificate != null)
                {
                    string file = Path.GetExtension(model.Upload_Photo.FileName);

                    string Filename = data.ApplicationId + file;
                    model.Photo = "../Uploads/Photo/" + Filename;
                    model.Upload_Photo.SaveAs(Path.Combine(Server.MapPath("~/Uploads/Photo/"), Filename));


                    string file1 = Path.GetExtension(model.Upload_Sign.FileName);

                    string Filename1 = data.ApplicationId + file1;
                    model.Signature = "../Uploads/Signature/" + Filename1;
                    model.Upload_Sign.SaveAs(Path.Combine(Server.MapPath("~/Uploads/Signature/"), Filename1));

                    string file2 = Path.GetExtension(model.AgeCertificate.FileName);

                    string Filename2 = data.ApplicationId + file2;
                    model.Age_Certified_Proof = "../Uploads/AgeCertificate/" + Filename2;
                    model.AgeCertificate.SaveAs(Path.Combine(Server.MapPath("~/Uploads/AgeCertificate/"), Filename2));

                    if (model.Candidate_Handicaped_YN == "No")
                    {
                        model.Handicap = "N.A.";
                    }

                    FormsAuthentication.SetAuthCookie(model.ApplicationId, false);
                    var user = this.User.Identity.Name;


                    var aa = db.Center_Login_Information.Where(x => x.Center_Name == model.Center).FirstOrDefault().Contact_Center_Code;
                    model.Center_Code = aa;
                    var DivCode = db.Tbl_CenterList.Where(x => x.center_code == aa).FirstOrDefault().div_code;
                    int id = model.Id;
                    string Enrollment_No = Common.Get_Year_Id() + "0" + DivCode.ToString() + (100000 + id).ToString();
                    model.Enrollment_No = Enrollment_No;

                    db.Tbl_Registration_Pre.Attach(model);
                    db.Entry(model).Property(x => x.Center_Code).IsModified = true;
                    db.Entry(model).Property(x => x.Enrollment_No).IsModified = true;
                    db.Entry(model).Property(x => x.ApplicationId).IsModified = true;
                    db.Entry(model).Property(x => x.Handicap).IsModified = true;
                    db.Entry(model).Property(x => x.Photo).IsModified = true;
                    db.Entry(model).Property(x => x.Signature).IsModified = true;
                    db.Entry(model).Property(x => x.Age_Certified_Proof).IsModified = true;
                    db.SaveChanges();


                }
               
                return Json(new { Result = "Submitted", Message = "../dataFrom.htm?Id="+data.Id+"" }, JsonRequestBehavior.AllowGet);

              

            }
            catch(Exception ex)
            {
                return Json(new { Result = false, Response = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }




        public ActionResult PrintForm(Tbl_Registration tbl_Registration, CenterViewModel model, Tbl_payment tbl_Payment)
        {

            //TempData["Subject_List"] = Session["Subject_List"].ToString();
            //string mobileno = TempData["Mobile_No"].ToString();



            tbl_Registration = db.Tbl_Registration.Where(x => x.ApplicationId == tbl_Registration.ApplicationId).SingleOrDefault();
            if (tbl_Registration == null)
            {
                tbl_Payment = db.Tbl_payment.Where(x => x.order_id == tbl_Payment.order_id).FirstOrDefault();
            }
            else
            {
                tbl_Payment = db.Tbl_payment.Where(x => x.billing_name == tbl_Registration.First_Name && x.billing_email == tbl_Registration.Email && x.order_status=="Success").SingleOrDefault();
            }

           

            tbl_Payment.merchant_param1 = tbl_Registration.ApplicationId;
            db.Tbl_payment.Attach(tbl_Payment);
            db.Entry(tbl_Payment).Property(x => x.merchant_param1).IsModified = true;
            if (tbl_Registration.Id != 0)
            {
                tbl_Registration.Payment_Status = "1";
            }
            else
            {

                tbl_Registration.Payment_Status = "0";

            }
            var cc = db.Center_Login_Information.Where(x => x.Center_Name == tbl_Registration.Center).FirstOrDefault();
            TempData["Amount"] = tbl_Payment.amount_money;
            TempData["order_status"] = tbl_Payment.order_status;
            TempData["Card_name"] = tbl_Payment.card_name;
            TempData["tracking_id"] = tbl_Payment.tracking_id;
            TempData["bank_ref_no"] = tbl_Payment.bank_ref_no;
            TempData["trans_date"] = tbl_Payment.trans_date;
            TempData["UDISE_No"] = cc.UDISE_No;
            TempData["Contact_Center_Code"] = tbl_Registration.Center_Code;
            TempData["Subject_List"] = tbl_Registration.Subject_List;
            db.Tbl_Registration.Attach(tbl_Registration);
            db.Entry(tbl_Registration).Property(x => x.Payment_Status).IsModified = true;
            db.SaveChanges();
            
            return View("PrintForm", tbl_Registration);

        }


        public void bindDistrict()
        {
            try
            {
                var districtList = db.Tbl_District.ToList();
                List<SelectListItem> li = new List<SelectListItem>();
                li.Add(new SelectListItem { Text = "-Select District-", Value = "0" });

                foreach (var m in districtList)
                {
                    li.Add(new SelectListItem { Text = m.District, Value = m.Id.ToString() });
                    ViewBag.districtList = li;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public JsonResult getTaluka(string id)
        {
            try
            {
                var talukaList = db.All_Taluka.Where(s => s.District == id).GroupBy(x => x.Taluka).Select(grp => grp.FirstOrDefault()).ToList();
                List<SelectListItem> licent = new List<SelectListItem>();
                licent.Add(new SelectListItem { Text = "-Select Taluka-", Value = "0" });
                if (talukaList != null)
                {
                    foreach (var x in talukaList)
                    {
                        licent.Add(new SelectListItem { Text = x.Taluka, Value = x.Taluka.ToString() });
                    }
                }
                return Json(new SelectList(licent, "Value", "Text", JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult getCenter(string id)
        {
            try
            {
                var centerList = db.Tbl_CenterList.Where(x => x.taluka == id).ToList();
                List<SelectListItem> licent = new List<SelectListItem>();
                licent.Add(new SelectListItem { Text = "-Select Center-", Value = "0" });
                if (centerList != null)
                {
                    foreach (var x in centerList)
                    {
                        licent.Add(new SelectListItem { Text = x.center_name, Value = x.center_name.ToString() });
                    }
                }
                return Json(new SelectList(licent, "Value", "Text", JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JsonResult getCenterCode(string center)
        {
            try
            {
                var centerCode = db.Tbl_CenterList.Where(x => x.center_name == center).FirstOrDefault();
                return Json(centerCode.center_code, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult getSubject(string id, string handicaped, Tbl_Registration subject)
        {
            try
            {
                subject.SubjectListA = db.Tbl_Subject.Where(x => x.Class == id && x.Handicaped == handicaped && x.Subject_Group == "A").ToList<Tbl_Subject>();
                subject.SubjectListB = db.Tbl_Subject.Where(x => x.Class == id && x.Handicaped == handicaped && x.Subject_Group == "B").ToList<Tbl_Subject>();
                subject.SubjectListC = db.Tbl_Subject.Where(x => x.Class == id && x.Handicaped == handicaped && x.Subject_Group == "C").ToList<Tbl_Subject>();
                if (handicaped == "No" && id == "8th")
                {
                    subject.SubjectListD = db.Tbl_Subject.Where(x => x.Class == id && x.Handicaped == handicaped && x.Subject_Group == "D").ToList<Tbl_Subject>();
                    var allList = new { SubjectListA = subject.SubjectListA, SubjectListB = subject.SubjectListB, SubjectListC = subject.SubjectListC, SubjectListD = subject.SubjectListD };
                    return Json(allList, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var allList = new { SubjectListA = subject.SubjectListA, SubjectListB = subject.SubjectListB, SubjectListC = subject.SubjectListC };
                    return Json(allList, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult getNSQFSubject(string standard, Tbl_Registration subject)
        {
            try
            {
                subject.SubjectListD_NSQF = db.Tbl_Subject.Where(x => x.Class == standard && x.Subject_Group == "D-NSQF").ToList<Tbl_Subject>();
                return Json(subject.SubjectListD_NSQF, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}