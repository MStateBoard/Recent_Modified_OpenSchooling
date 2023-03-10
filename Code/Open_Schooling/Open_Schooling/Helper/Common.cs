using Open_Schooling.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Open_Schooling.Helper
{
    public class Common
    {
        Db_Open_SchoolingEntities db = new Db_Open_SchoolingEntities();
        public Registration_Model Get_Login_Details()
        {
            //string login_string= System.Web.HttpContext.Current.User.Identity.Name;
            string login_string = HttpContext.Current.User.Identity.Name;
            Registration_Model login_model = JsonConvert.DeserializeObject<Registration_Model>(login_string);
            return login_model;
        }
        public static string Get_Year_Id()
        {
            return "22";
        }

        public static string Get_Year()
        {
            return "2022-23";
        }
        public void CreateExcelFile(DataTable dt_list, string filename)
        {
            try
            {
                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt_list);
                using (DataSet ds = ds1)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        string rootFolder = HttpContext.Current.Server.MapPath("~/StateData").ToString();
                        string fileName = @"" + filename + ".xlsx";

                        System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
                        using (ExcelPackage xp = new ExcelPackage(file))
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                ExcelWorksheet ws = xp.Workbook.Worksheets.Add(dt.TableName);
                                int rowstart = 1;
                                int colstart = 1;
                                int rowend = rowstart;
                                int colend = colstart + dt.Columns.Count;
                                rowend = rowstart + dt.Rows.Count;
                                ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                                int i = 1;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    i++;
                                    if (dc.DataType == typeof(decimal))
                                        ws.Column(i).Style.Numberformat.Format = "#0.00";
                                }
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                            }
                            xp.Save();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public Tbl_Registration_Pre getSubList(Tbl_Registration_Pre model)
        {
            Tbl_Registration_Pre obj1 = new Tbl_Registration_Pre();
            if (model.SUBNSQF == null)
            {
                var comman1 = model.SUB.ToList();
                int commanCount1 = comman1.Count;
                Tbl_Subject obj = new Tbl_Subject();
                if (commanCount1 == 5)
                {
                    model.Subject1 = comman1[0].ToString();
                    model.Subject2 = comman1[1].ToString();
                    model.Subject3 = comman1[2].ToString();
                    model.Subject4 = comman1[3].ToString();
                    model.Subject5 = comman1[4].ToString();
                    model.Subject_List = model.Subject1 + ", " + model.Subject2 + ", " + model.Subject3 + ", " + model.Subject4 + ", " + model.Subject5;
                    obj1.Subject1 = comman1[0].ToString();
                    obj1.Subject2 = comman1[1].ToString();
                    obj1.Subject3 = comman1[2].ToString();
                    obj1.Subject4 = comman1[3].ToString();
                    obj1.Subject5= comman1[4].ToString();
                    obj1.Subject_List= model.Subject1 + ", " + model.Subject2 + ", " + model.Subject3 + ", " + model.Subject4 + ", " + model.Subject5;
                    var sub1 = "";
                    var sub2 = "";
                    var sub3 = "";
                    var sub4 = "";
                    var sub5 = "";

                    Tbl_Subject[] tbl_Subjects = Get_Nsqf_Sub(model);
                    sub1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")";
                    sub2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")";
                    sub3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")";
                    sub4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")";
                    sub5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")";

                    model.Subject_List= sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;
                    obj1.Subject_List= sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;

                    //model.Subject_List = TempData["Subject_List"].ToString();
                }
                else
                {

                   

                }
            }
            else
            {
                var comman = model.SUB.ToList();
                var nsqfSub = model.SUBNSQF.ToList();
                int nsqfsubCount = nsqfSub.Count;
                int commanCount = comman.Count;
                var totalSubject = commanCount + nsqfsubCount;

                var sub1 = "";
                var sub2 = "";
                var sub3 = "";
                var sub4 = "";
                var sub5 = "";

                Tbl_Subject[] tbl_Subjects = Get_Nsqf_Sub(model);
                sub1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")";
                sub2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")";
                sub3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")";
                sub4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")";
                sub5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")";
                model.Subject_List = sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;
                obj1.Subject_List= sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;
                //model.Subject_List = TempData["Subject_List"].ToString();
                if (totalSubject == 5)
                {
                    if (nsqfsubCount <= 2)
                    {
                        if (nsqfSub.Count == 1)
                        {
                            model.Nsqf_Subject = nsqfSub[0].ToString();

                            model.Subject1 = comman[0].ToString();
                            model.Subject2 = comman[1].ToString();
                            model.Subject3 = comman[2].ToString();
                            model.Subject4 = comman[3].ToString();
                            //model.Subject_List = model.Subject1 + ", " + model.Subject2 + ", " + model.Subject3 + ", " + model.Subject4;
                            model.Subject_List = sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;
                            obj1.Subject_List= sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;
                        }
                        else
                        {
                            var NSQF_Subject1 = nsqfSub[0].ToString();
                            var NSQF_Subject2 = nsqfSub[1].ToString();
                            model.Nsqf_Subject = NSQF_Subject1 + " " + NSQF_Subject2;

                            model.Subject1 = comman[0].ToString();
                            model.Subject2 = comman[1].ToString();
                            model.Subject3 = comman[2].ToString();
                            //model.Subject_List = model.Subject1 + ", " + model.Subject2 + ", " + model.Subject3;
                            model.Subject_List = sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;
                            obj1.Subject_List= sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;
                        }
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    
                }
            }
            return obj1;
        }


        public Tbl_Subject[] Get_Nsqf_Sub(Tbl_Registration_Pre oS_Form_Data)
        {
            Tbl_Subject[] model = new Tbl_Subject[5];
            


            if (oS_Form_Data.SUBNSQF != null )
            {
                var normal_sub = oS_Form_Data.SUB.ToList();
                var nsqfSub = oS_Form_Data.SUBNSQF.ToList();
                //string[] nsqfsub = oS_Form_Data.Nsqf_Subject.Split(' ');
                if (nsqfSub.Count == 2)
                {
                    string sub1 = "";
                    sub1 = normal_sub[0];
                    string sub2 = "";
                    sub2 = normal_sub[1];
                    string sub3 = "";
                    sub3 = normal_sub[2];
                    model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == sub1).FirstOrDefault();
                    model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == sub2).FirstOrDefault();
                    model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == sub3).FirstOrDefault();
                    string nsqf1 = "";
                    nsqf1 = nsqfSub[0];
                    string nsqf2 = "";
                    nsqf2 = nsqfSub[1];
                    model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == nsqf1).FirstOrDefault();
                    model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == nsqf2).FirstOrDefault();
                }
                else if (nsqfSub.Count == 1)
                {
                    string sub1 = "";
                    sub1 = normal_sub[0];
                    string sub2 = "";
                    sub2 = normal_sub[1];
                    string sub3 = "";
                    sub3 = normal_sub[2];
                    string sub4 = "";
                    sub4 = normal_sub[3];
                    model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == sub1).FirstOrDefault();
                    model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == sub2).FirstOrDefault();
                    model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == sub3).FirstOrDefault();
                    model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == sub4).FirstOrDefault();
                    string nsqf = "";
                    nsqf = nsqfSub[0];
                    model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == nsqf).FirstOrDefault();
                }
                else
                {
                    model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject1).FirstOrDefault();
                    model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject2).FirstOrDefault();
                    model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject3).FirstOrDefault();
                    model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject4).FirstOrDefault();
                    model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject5).FirstOrDefault();
                }
            }
            else
            {
                model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject1).FirstOrDefault();
                model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject2).FirstOrDefault();
                model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject3).FirstOrDefault();
                model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject4).FirstOrDefault();
                model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject5).FirstOrDefault();
            }
          

            return model;
        }



       
    }
}