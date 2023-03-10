using Open_Schooling.Helper;
using Open_Schooling.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Open_Schooling.Controllers
{
    public class StateController : Controller
    {
        Db_Open_SchoolingEntities db = new Db_Open_SchoolingEntities();
        Common common = new Common();
        // GET: State
        public ActionResult StateLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StateLogin(State_Login_Model state_Login_Model)
        {
            try
            {
                if (state_Login_Model.User_Name == "admin" && state_Login_Model.State_Password == "admin")
                {
                    return RedirectToAction("StateDashboard");
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
        public ActionResult StateDashboard()
        {
            //string count = "";
            //count = "select count( * ) as countData from Tbl_Registration a join tbl_payment b on  a.ApplicationId = b.merchant_param1 and b.order_status = 'success'  and a.Payment_Status = '1'";
            //List<Division_List_Model> model = db.Database.SqlQuery<Division_List_Model>(count).ToList();
            //foreach (var item in model)
            //{
            //    TempData["count"] = "Total Registered Students are  " + item.countData;
            //}

            return View();
        }
        [HttpPost]
        public JsonResult StateDashboard(string Div_Code, string Excel)
        {
            string Query = "", fileName = "";
            try
            {
                Query = "select * from Tbl_Registration A join Center_Login_Information B on A.Center_Code=B.Contact_Center_Code join Tbl_payment P on A.ApplicationId=P.merchant_param1 where B.Div_Code='" + Div_Code + "'and A.Payment_Status='1' and P.order_status='Success' and Ec_Status is null ";

                List<Division_List_Model> model = db.Database.SqlQuery<Division_List_Model>(Query).ToList();

                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "District_Batch_List";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_Batch_List";
                    common.CreateExcelFile(dt, fileName);
                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = true, Response = "Unable to Fetch Record", }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult RegistrationMode()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegistrationMode(string id)
        {
            try
            {
                Tbl_Site_Status tbl_Site_Status = new Tbl_Site_Status();
                tbl_Site_Status.RegisterEorD = id;
                tbl_Site_Status.Start_Date = DateTime.Now;
                db.Tbl_Site_Status.Add(tbl_Site_Status);
                db.SaveChanges();
                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
            
        }


        public ActionResult PendingRegistration()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult PendingRegistration(Tbl_Registration_Pre tbl_Registration_Pre)
        {
            try
            {
                var res = db.Tbl_Registration_Pre.Where(x => x.Adhar_no == tbl_Registration_Pre.Adhar_no).OrderByDescending(x => x.DateNow).FirstOrDefault();

                string Query = "SBO ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr1"].ConnectionString);
                con.Open();


                string insertSPPay = "Sp_Insert_Tbl_Pay";
                SqlCommand command1 = new SqlCommand(insertSPPay, con);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@Id               ", res.Id            );
                command1.Parameters.AddWithValue("@First_Name       ", res.First_Name    );
                command1.Parameters.AddWithValue("@billing_city     ", res.Village       );
                command1.Parameters.AddWithValue("@billing_state    ", res.State         );
                command1.Parameters.AddWithValue("@billing_tel      ", res.Mobile_No     );
                command1.Parameters.AddWithValue("@billing_email    ", res.Email         );
                command1.Parameters.AddWithValue("@merchant_param1  ", res.ApplicationId );
                command1.Parameters.AddWithValue("@trans_date       ", res.DateNow       );
       
                int a= command1.ExecuteNonQuery();
                string insertSPR = "Sp_Insert_Tbl_Regi";
                SqlCommand command = new SqlCommand(insertSPR, con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Adhar_no", res.Adhar_no);
                command.Parameters.AddWithValue("@DateNow", res.DateNow);
                int b = 0;
               b=  command.ExecuteNonQuery();
                con.Close();
               

                //List<Division_List_Model> model = db.Database.SqlQuery<Division_List_Model>(Query).ToList();

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public ActionResult VerifyEligible()
        {
            return View();
        }
        [HttpPost]
        public JsonResult VerifyEligible(int ID)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr1"].ConnectionString);
                con.Open();
                string query = "update Tbl_Registration set Ec_Status='Completed' where Id="+ID+" ";
                SqlCommand sqlCommand = new SqlCommand(query,con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
               
               
                return Json(new { Result = true, Response = "", }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = true, Response = "Unable to Fetch Record", }, JsonRequestBehavior.AllowGet);
            }
        }
            public ActionResult ForgotPassword()
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
        public JsonResult getPassword(double udiseNo)
        {
            try
            {
                var password = db.Center_Login_Information.Where(x => x.UDISE_No == udiseNo).FirstOrDefault();
                if (password == null)
                {
                    string Error = "Invalid UDISE Number.";
                    return Json(Error, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(password.Center_Password, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}