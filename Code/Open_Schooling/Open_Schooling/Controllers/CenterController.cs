using Newtonsoft.Json;
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

namespace Open_Schooling.Controllers
{
    public class CenterController : Controller
    {
        Db_Open_SchoolingEntities db = new Db_Open_SchoolingEntities();
        Common common = new Common();
        SqlConnection _Con;
        SqlCommand _Command;
        // GET: Center
        public ActionResult CenterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CenterLogin(Center_Login_Information center_Login)
        {
            try
            {
                var IsValid = db.Center_Login_Information.Where(c => c.UDISE_No == center_Login.UDISE_No && c.Center_Password == center_Login.Center_Password).FirstOrDefault();
                if (IsValid != null)
                {
                    Session["Center_Name"] = IsValid.Center_Name;
                    Session["Center_Code"] = IsValid.Contact_Center_Code;
                    Session["Division"] = IsValid.Division;
                    Session["Taluka"] = IsValid.Taluka;
                    
                    return RedirectToAction("CenterInformation", "Center");
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

        public ActionResult CenterInformation()
        {
            try
            {
                List<CenterViewModel> centerViewModel = new List<CenterViewModel>();
                var CenterCode = Session["Center_Code"];
                centerViewModel = (from A in db.Tbl_Registration
                                   //join B in db.Tbl_payment on A.ApplicationId equals B.merchant_param1
                                   //join C in db.Tbl_Application_Form on A.ApplicationId equals C.Form_No
                                   where A.Center_Code.Trim() == CenterCode.ToString() && A.Payment_Status == "1"
                                   select new CenterViewModel
                                   {
                                       Payment_Status = A.Payment_Status,
                                       ApplicationId = A.ApplicationId,
                                       Name = A.First_Name,
                                       Mobile_No = A.Mobile_No,
                                       Ec_Status = A.Ec_Status,
                                       //Seat_No = db.Tbl_Registration.Where(a => a.ApplicationId == A.ApplicationId).FirstOrDefault().Seat_No,
                                       EC_No = A.Enrollment_No,
                                       Exam_Form_Disable = A.Exam_Form_Disable,

                                   }).ToList();
                //centerViewModel.AddRange(centerViewModel);
                return View(centerViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EC_Form()


        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        [HttpPost]
        public ActionResult EC_Form(Tbl_Registration tbl_Registration)
        {
            try
            {
                var IsValid = db.Tbl_Registration.Where(c => c.ApplicationId == tbl_Registration.ApplicationId && c.Mobile_No == tbl_Registration.Mobile_No).FirstOrDefault();
                if (IsValid != null)
                {
                    Session["FormNo"] = IsValid.ApplicationId;
                    Session["CenterCode"] = IsValid.Center_Code;
                    return RedirectToAction("EnrollmentCertificate");
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

        public ActionResult EnrollmentCertificate(string ApplicationId)
        {
            try
            {
                Tbl_Registration registration_Model = new Tbl_Registration();
                if (ApplicationId == null)
                {
                    var formNo = Session["FormNo"];
                    var CenterCode = Session["CenterCode"];
                    string x = formNo.ToString();
                    ViewData["stand"] = x.Substring(8, 1);
                    registration_Model = db.Tbl_Registration.Where(os => os.ApplicationId == formNo.ToString()).FirstOrDefault();
                    var ecCertificate = new CenterViewModel
                    {
                        tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId == formNo.ToString()).FirstOrDefault(),
                        //Tbl_Payment = db.Tbl_payment.Where(p => p.merchant_param1 == formNo.ToString()).FirstOrDefault(),
                        Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == CenterCode.ToString()).FirstOrDefault()
                    };
                    string extension = Path.GetExtension(registration_Model.Photo.Trim());
                    if (extension == ".jpg" || extension == ".JPG" || extension == ".jpeg")
                    {
                        //ViewData["Photo"] = "/Uploads/Photo/" + registration_Model.ApplicationId.TrimEnd() + "/Profile" + ".jpeg";
                        Session["Photo"] = registration_Model.Photo;
                    }
                    else
                    {
                        ViewData["Photo"] = registration_Model.Photo;
                    }
                    return View(ecCertificate);
                }
                else
                {
                    registration_Model = db.Tbl_Registration.Where(x => x.ApplicationId == ApplicationId && x.Ec_Status != null).FirstOrDefault();
                    if (registration_Model != null)
                    {
                        var contactCeneterCode = registration_Model.Center_Code;
                        ViewData["stand"] = ApplicationId.Substring(8, 1);
                        var centerEc = new CenterViewModel
                        {
                            tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId.Trim() == ApplicationId.Trim()).FirstOrDefault(),
                            //Tbl_Payment = db.Tbl_payment.Where(p => p.merchant_param1.Trim() == UserName.Trim()).FirstOrDefault(),
                            Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == contactCeneterCode).FirstOrDefault()
                        };

                        string extension = Path.GetExtension(registration_Model.Photo);
                        if (extension == ".jpg" || extension == ".JPG" || extension == ".jpeg")
                        {
                            Session["Photo"] = registration_Model.Photo;
                        }
                        else
                        {
                            ViewData["Photo"] = "/AppFiles/Images/" + registration_Model.ApplicationId.TrimEnd() + "/Profile" + ".jpg";
                        }
                        return View(centerEc);
                    }
                    else
                    {
                        return RedirectToAction("CenterInformation");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CenterInformation");
            }
        }


        [HttpGet]
        public ActionResult Application_Form(Tbl_Registration_Pre ExamFormModel)
        {
            try
            {
                Tbl_Registration_Pre tbl_Registration_Pre = new Tbl_Registration_Pre();
                var UserName = ExamFormModel.ApplicationId;
                tbl_Registration_Pre = db.Tbl_Registration_Pre.Where(os => os.ApplicationId == UserName && os.Ec_Status == "Completed").FirstOrDefault();
                Tbl_Subject tbl_Subject1 = new Tbl_Subject();
                Tbl_Subject tbl_Subject2 = new Tbl_Subject();
                Tbl_Subject tbl_Subject3 = new Tbl_Subject();
                Tbl_Subject tbl_Subject4 = new Tbl_Subject();
                Tbl_Subject tbl_Subject5 = new Tbl_Subject();

                Tbl_Subject[] tbl_Subjects = common.Get_Nsqf_Sub(tbl_Registration_Pre);


                var examForm = new CenterViewModel
                {
                    tbl_Registration_Pre = db.Tbl_Registration_Pre.Where(os => os.ApplicationId == UserName).FirstOrDefault(),
                    Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == tbl_Registration_Pre.Center_Code).FirstOrDefault(),
                    Subject1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")",
                    Subject2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")",
                    Subject3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")",
                    Subject4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")",
                    Subject5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")",




                };



                return View(examForm);
            }
            catch(Exception ex)
            {
                return View();
            }
            

        }

        [HttpPost]

        public JsonResult Save_Application_Form(CenterViewModel model)
        {


            try
            {
                if (ModelState.IsValid)
                {
                    Tbl_Application_Form obj = new Tbl_Application_Form();
                    obj.Form_No = model.tbl_Registration.ApplicationId;
                    obj.UDISE_No = model.Center_Login.UDISE_No.ToString();
                    obj.Contact_center = model.tbl_Registration.Center_Code;
                    obj.Last_name = model.tbl_Registration.Last_Name;
                    obj.Name = model.tbl_Registration.First_Name;
                    obj.Middle_name = model.tbl_Registration.Middle_Name;
                    obj.Mother_name = model.tbl_Registration.Mother_Name;
                    obj.Address = model.tbl_Registration.Address;
                    obj.Pincode = model.tbl_Registration.Pin_Code;
                    obj.Mobile_no = model.tbl_Registration.Mobile_No;
                    obj.Place_of_birth = model.tbl_Registration.DOB_Village;
                    obj.Date_of_birth = model.tbl_Registration.Date_of_Birth;
                    obj.Adhar_no = model.tbl_Registration.Adhar_no;
                    obj.Gender = model.tbl_Registration.Gender;
                    obj.Minority_Religion = model.tbl_Registration.Minority_Religion;
                    obj.Divyang = model.tbl_Registration.Handicap;
                    obj.Medium = model.tbl_Registration.Medium;
                    obj.Type_Of_User = model.Type_Of_User;
                    obj.Subject1 = model.Subject1;
                    obj.Subject2 = model.Subject2;
                    obj.Subject3 = model.Subject3;
                    obj.Subject4 = model.Subject4;
                    obj.Subject5 = model.Subject5;
                    obj.Subjects = model.Subject1 + "," + model.Subject2 + "," + model.Subject3 + "," + model.Subject4 + "," + model.Subject5;

                    obj.EC_Number = model.tbl_Registration.Enrollment_No;
                    obj.EC_Year = model.EC_Year;
                    obj.LastExamSeatNo = model.LastExamSeatNo;
                    obj.LastExamYear = model.LastExamYear;
                    obj.DateNow = DateTime.Now.ToString();




                    db.Tbl_Application_Form.Add(obj);
                    db.SaveChanges();

                    _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr1"].ConnectionString);
                    _Con.Open();
                    _Command = new SqlCommand("Update Tbl_Registration set Exam_Form_Disable='Downloaded' where ApplicationId='" + obj.Form_No + "'", _Con);
                    _Command.ExecuteNonQuery();
                    _Con.Close();

                }
                return Json(new { Result = true, Response = "Record Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = false, Response = "Failed" + ex }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public ActionResult PrintExamApplicationForm(CenterViewModel centerViewModel)
        {
            try
            {
                Tbl_Registration_Pre oS_Form_Data = new Tbl_Registration_Pre();
                var UserName = centerViewModel.ApplicationId;
                oS_Form_Data = db.Tbl_Registration_Pre.Where(os => os.ApplicationId == UserName).FirstOrDefault();
                Tbl_Subject tbl_Subject1 = new Tbl_Subject();
                Tbl_Subject tbl_Subject2 = new Tbl_Subject();
                Tbl_Subject tbl_Subject3 = new Tbl_Subject();
                Tbl_Subject tbl_Subject4 = new Tbl_Subject();
                Tbl_Subject tbl_Subject5 = new Tbl_Subject();

                Tbl_Subject[] tbl_Subjects = common.Get_Nsqf_Sub(oS_Form_Data);


                var examForm = new CenterViewModel
                {
                    tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId == UserName).FirstOrDefault(),
                    Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == oS_Form_Data.Center_Code).FirstOrDefault(),
                    Subject1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")",
                    Subject2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")",
                    Subject3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")",
                    Subject4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")",
                    Subject5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")",




                };

                return View(examForm);
            }
            catch(Exception ex)
            {
                return View();
            }
            
        }
        public JsonResult hallTickit(String myJSON)
        {
            try
            {

                Tbl_Registration jobject = JsonConvert.DeserializeObject<Tbl_Registration>(myJSON);
                Tbl_Registration isexist = db.Tbl_Registration.Where(x => x.ApplicationId == jobject.ApplicationId).FirstOrDefault();

                if (isexist != null)
                {
                    return Json(new { Result = "Success", Message = "PrintExamApplicationForm?User_Name=" + jobject.ApplicationId }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Tbl_Subject tbl_Subject1 = new Tbl_Subject();
                    Tbl_Subject tbl_Subject2 = new Tbl_Subject();
                    Tbl_Subject tbl_Subject3 = new Tbl_Subject();
                    Tbl_Subject tbl_Subject4 = new Tbl_Subject();
                    Tbl_Subject tbl_Subject5 = new Tbl_Subject();
                    Tbl_Registration_Pre oS_Form_Data = new Tbl_Registration_Pre();
                    CenterViewModel centerVM = new CenterViewModel();
                    oS_Form_Data = db.Tbl_Registration_Pre.Where(os => os.ApplicationId == jobject.ApplicationId).FirstOrDefault();
                    string hostName = Dns.GetHostName();
                    Console.WriteLine(hostName);
                    jobject.ip = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                    var UserName = oS_Form_Data.ApplicationId;




                    Tbl_Subject[] tbl_Subjects = common.Get_Nsqf_Sub(oS_Form_Data);


                    var examForm = new CenterViewModel
                    {
                        tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId == UserName).FirstOrDefault(),
                        Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == oS_Form_Data.Center_Code).FirstOrDefault(),
                        Subject1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")",
                        Subject2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")",
                        Subject3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")",
                        Subject4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")",
                        Subject5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")",
                    };


                    db.Tbl_Registration.Add(jobject);

                    db.SaveChanges();
                    _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
                    _Con.Open();
                    _Command = new SqlCommand("Update Tbl_Registration set Hidden='Downloaded' where Id='" + oS_Form_Data.Id + "'", _Con);
                    _Command.ExecuteNonQuery();
                    _Con.Close();
                    return Json(new { Result = "Success", Message = "PrintExamApplicationForm?User_Name=" + jobject.ApplicationId }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                _Con.Close();
                return Json(new { Result = "Failed", Message = "Exam form submission failed" }, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }
    }
}