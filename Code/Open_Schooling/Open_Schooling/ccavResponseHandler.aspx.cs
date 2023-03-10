using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using CCA.Util;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class ResponseHandler : System.Web.UI.Page
{
    SqlConnection _Con, _con2;
    SqlCommand _Command, _command2, _command3;

    protected void Page_Load(object sender, EventArgs e)
    {
        string workingKey = "12B336DB90B1CCF5D5477B0F3E919C05";//put in the 32bit alpha numeric key in the quotes provided here
        CCACrypto ccaCrypto = new CCACrypto();
        string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
        NameValueCollection Params = new NameValueCollection();
        string[] segments = encResponse.Split('&');
        foreach (string seg in segments)
        {
            string[] parts = seg.Split('=');
            if (parts.Length > 0)
            {
                string Key = parts[0].Trim();
                string Value = parts[1].Trim();
                Params.Add(Key, Value);
            }
        }
       
        //for (int i = 0; i < Params.Count; i++)
        //{

        //    Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
        //}

        //----------------extra code----------------------------

        try
         {


            _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr1"].ConnectionString);
            _Con.Open();
            _Command = new SqlCommand("insert into Tbl_payment Values ('" + Params[0] + "',	'" + Params[1] + "',	'" + Params[2] + "',	'" + Params[3] + "',	'" + Params[4] + "',	'" + Params[5] + "',	'" + Params[6] + "',	'" + Params[7] + "',	'" + Params[8] + "',	'" + Params[9] + "',	'" + Params[10] + "',	'" + Params[11] + "',	'" + Params[12] + "',	'" + Params[13] + "',	'" + Params[14] + "',	'" + Params[15] + "',	'" + Params[16] + "',	'" + Params[17] + "',	'" + Params[18] + "',	'" + Params[19] + "',	'" + Params[20] + "',	'" + Params[21] + "',	'" + Params[22] + "',	'" + Params[23] + "',	'" + Params[24] + "',	'" + Params[25] + "',	'" + Params[26] + "','" + Params[27] + "','" + Params[28] + "','" + Params[29] + "','" + Params[30] + "',	'" + Params[31] + "',	'" + Params[32] + "',	'" + Params[33] + "',	'" + Params[34] + "',	'" + Params[35] + "',	'" + Params[36] + "','" + Params[37] + "','" + Params[38] + "','" + Params[39] + "','" + Params[40] + "','" + Params[41] + "','1')", _Con);
            _Command.ExecuteNonQuery();



            //DataTable dt = new DataTable();
            //SqlDataAdapter _dataAdapter = new SqlDataAdapter("select * from Tbl_payment where order_id='" + Params[0] + "' and amount_money='" + Params[10] + "'", _Con);
            //dt = new DataTable();
            //_dataAdapter.Fill(dt);
            //string ss = "";


            if (Params[3].ToString() == "Aborted") 
            {
                Response.Write("<div style='border: 1px double; background-color:#152f49; color:white;'  align='center' ><center>  <h1>  OPEN SCHOOLING </h1>  <h1>  Maharashtra State Board of Secondary & Higher Secondary Education Pune 411004 </h1> Survey No. 832-A, Final Plot No. 178 & 179, Near Balchitrawani, Behind Agharkar Research Institute, Bhamburda, Shivajinagar, Pune - 411 004.Maharashtra(INDIA) Phone - 020 - 25705172</div></center></div>  <div style='margin-bottom:40px;'></div>  <div style='text - align:center'><center> <jumbotron style = 'color:red;font-size:2em;text-align:center;' ><i class='fa fa-check' aria-hidden='true'></i> Your payment for order ID<i class='fa fa-inr' aria-hidden='true'></i> " + Params[0] + " is Failed !</jumbotron></center></div>    <div style='margin-bottom:10px;'></div>   <div style = 'background:#F0F0F0; padding:10px; border:2px solid #c6c6c6; margin-right:400px; margin-left:400px' ><div style = 'background:#ffffff; margin:auto 2em 0 2em;border:2px solid #c6c6c6'><div style='margin-right: 40px; margin-left: 30px;'>   <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black;font-size:18; font-weight: 600;'>Transaction Date</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>   <span id = 'Transaction_Date' > " + Params[40] + "</span> </h3> </div>      <div style='margin-bottom:30px;'></div> <div style='  margin-right: 40px; margin-left: 30px;'>  <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black; font-size:18; font-weight: 600;'>Transaction Amount</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>    <span id = 'Transaction_Amount' > " + Params[10] + "</span>  </h3> </div>  </div> <div class='transact_box' style='background:#ffffff; margin:30px 2em 0 2em;border:2px solid #c6c6c6'> <div style = 'padding: 10px;' > <p class='message' style='margin-bottom:2px; color: #06213F; font-weight: 600;  margin: 2px 10px 20px 1px;  text-align: center;'> Your payment is <u>Failed</u>.We have sent an email confirmation to <span style='color:blue'> " + Params[18] + "</span> and a SMS   to " + Params[17] + "  confirming your transaction.   </p> </div>  <div style = 'padding: 10px; padding-bottom:0px' > <p class='message'>  If you don’t get an Email or SMS from us within 1 hour then, write to us at<span style='color:blue'> support@sabpaisa.in.</span>  </p>  </div> </div> </div> <div style='margin-bottom:20px;'></div> <center>  <div ><a href='http://msbos.mh-ssc.ac.in'>Try Again</a></div>");

            }


            //else if (Params[3].ToString() == "Success" &&  Params[10].ToString() == "1.00")
            else if (Params[3].ToString() == "Failure")
            {

                
                
                

                Response.Write("<div style='border: 1px double; background-color:#152f49; color:white;'  align='center' ><center>  <h1> OPEN SCHOOLING </h1>  <h1> Maharashtra State Board of Secondary & Higher Secondary Education Pune 411004 </h1> Survey No. 832-A, Final Plot No. 178 & 179, Near Balchitrawani, Behind Agharkar Research Institute, Bhamburda, Shivajinagar, Pune - 411 004.Maharashtra(INDIA) Phone - 020 - 25705172</div></center></div>  <div style='margin-bottom:40px;'></div>  <div style='text - align:center'><center> <jumbotron style = 'color:red;font-size:2em;text-align:center;' ><i class='fa fa-check' aria-hidden='true'></i> Your payment for order ID<i class='fa fa-inr' aria-hidden='true'></i> " + Params[0] + " is Failed !</jumbotron></center></div>    <div style='margin-bottom:10px;'></div>   <div style = 'background:#F0F0F0; padding:10px; border:2px solid #c6c6c6; margin-right:400px; margin-left:400px' ><div style = 'background:#ffffff; margin:auto 2em 0 2em;border:2px solid #c6c6c6'><div style='margin-right: 40px; margin-left: 30px;'>   <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black;font-size:18; font-weight: 600;'>Transaction Date</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>   <span id = 'Transaction_Date' > " + Params[40] + "</span> </h3> </div>      <div style='margin-bottom:30px;'></div> <div style='  margin-right: 40px; margin-left: 30px;'>  <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black; font-size:18; font-weight: 600;'>Transaction Amount</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>    <span id = 'Transaction_Amount' > " + Params[10] + "</span>  </h3> </div>  </div> <div class='transact_box' style='background:#ffffff; margin:30px 2em 0 2em;border:2px solid #c6c6c6'> <div style = 'padding: 10px;' > <p class='message' style='margin-bottom:2px; color: #06213F; font-weight: 600;  margin: 2px 10px 20px 1px;  text-align: center;'> Your payment is <u>Failed</u>.We have sent an email confirmation to <span style='color:blue'> " + Params[18] + "</span> and a SMS   to " + Params[17] + "  confirming your transaction.   </p> </div>  <div style = 'padding: 10px; padding-bottom:0px' > <p class='message'>  If you don’t get an Email or SMS from us within 1 hour then, write to us at<span style='color:blue'> support@sabpaisa.in.</span>  </p>  </div> </div> </div> <div style='margin-bottom:20px;'></div> <center>  <div ><a href='http://msbos.mh-ssc.ac.in/'>Try Again2</a></div>"); 



            }
            else if (Params[3].ToString() == "Success")
            {

                _con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr1"].ConnectionString);
                _con2.Open();
                _command2 = new SqlCommand("Update Tbl_Registration_Pre set Payment_Status = '1',Ec_Status='Completed' Order_Id = '" + Params[0] + "'where Id = (SELECT MAX(Id) FROM Tbl_Registration_Pre where First_Name = '" + Params[11] + "'  and Email='" + Params[18] + "')  ", _Con);
                _command2.ExecuteNonQuery();

                string query =
            " INSERT INTO [Tbl_Registration] ([Enrollment_No]  ,[ApplicationId],[First_Name],[Middle_Name],[Last_Name],                      " +
            " [Mother_Name],[Adhar_no],[Mobile_No],[Address],[Village],[District],[Taluka],[State],[Pin_Code],[Date_of_Birth],               " +
            " [DOB_Words],[DOB_Village],[DOB_Taluka],[DOB_District],[Email],[Gender],[Standard],[Medium],[Age_Certified_Proof],              " +
            " [District_Center],[Taluka_Center],[Center],[Center_Code],[Previous_Attend_School_YN],[Self_Declaration_Not_Gone_School],       " +
            " [Previous_Attend_School],[Date_Of_Leaving_Last_Attended_School],[Handicap],[Candidate_Handicaped_YN],[Subject_List],           " +
            " [Subject1],[Subject2],[Subject3],[Subject4],[Subject5],[Minority_Religion],[Exam_Form_Disable],[Type_Of_User],[Nsqf_Subject],  " +
            " [Hall_Ticket],[Photo],[Password],[Signature],[Payment_Status],[Ec_Status],[ip],[DateNow],[Year_Id],[Reference_Id])             " +
            "  SELECT[Enrollment_No],[ApplicationId],[First_Name],[Middle_Name],[Last_Name],                                                 " +
            " [Mother_Name],[Adhar_no],[Mobile_No],[Address],[Village],[District],[Taluka],[State],[Pin_Code],[Date_of_Birth],               " +
            " [DOB_Words],[DOB_Village],[DOB_Taluka],[DOB_District],[Email],[Gender],[Standard],[Medium],[Age_Certified_Proof],              " +
            " [District_Center],[Taluka_Center],[Center],[Center_Code],[Previous_Attend_School_YN],[Self_Declaration_Not_Gone_School],       " +
            " [Previous_Attend_School],[Date_Of_Leaving_Last_Attended_School],[Handicap],[Candidate_Handicaped_YN],[Subject_List],           " +
            " [Subject1],[Subject2],[Subject3],[Subject4],[Subject5],[Minority_Religion],[Exam_Form_Disable],[Type_Of_User],[Nsqf_Subject],  " +
            " [Hall_Ticket],[Photo],[Password],[Signature],[Payment_Status],[Ec_Status],[ip],[DateNow] ,[Year_Id],[Id]                       " +
            " FROM[Tbl_Registration_Pre] a  where  a.Order_Id='" + Params[0] + "'  ";
                _command3 = new SqlCommand(query, _Con);

             
                _command3.ExecuteNonQuery();
                _con2.Close();
                Response.Write("<div style='border: 1px double; background-color:#152f49; color:white;'  align='center' ><center>  <h1> <h1>  OPEN SCHOOLING</h1>  <h1>  Maharashtra State Board of Secondary & Higher Secondary Education Pune 411004 </h1> Survey No. 832-A, Final Plot No. 178 & 179, Near Balchitrawani, Behind Agharkar Research Institute, Bhamburda, Shivajinagar, Pune - 411 004.Maharashtra(INDIA) Phone - 020 - 25705172</div></center></div>  <div style='margin-bottom:40px;'></div>  <div style='text - align:center'><center> <jumbotron style = 'color:green;font-size:2em;text-align:center;' ><i class='fa fa-check' aria-hidden='true'></i> Your payment for order ID<i class='fa fa-inr' aria-hidden='true'></i> " + Params[0] + " is successful !</jumbotron></center></div>    <div style='margin-bottom:10px;'></div>   <div style = 'background:#F0F0F0; padding:10px; border:2px solid #c6c6c6; margin-right:400px; margin-left:400px' ><div style = 'background:#ffffff; margin:auto 2em 0 2em;border:2px solid #c6c6c6'><div style='margin-right: 40px; margin-left: 30px;'>   <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black;font-size:18; font-weight: 600;'>Transaction Date</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>   <span id = 'Transaction_Date' > " + Params[40] + "</span> </h3> </div>      <div style='margin-bottom:30px;'></div> <div style='  margin-right: 40px; margin-left: 30px;'>  <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black; font-size:18; font-weight: 600;'>Transaction Amount</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>    <span id = 'Transaction_Amount' > " + Params[10] + "</span>  </h3> </div>  </div> <div class='transact_box' style='background:#ffffff; margin:30px 2em 0 2em;border:2px solid #c6c6c6'> <div style = 'padding: 10px;' > <p class='message' style='margin-bottom:2px; color: #06213F; font-weight: 600;  margin: 2px 10px 20px 1px;  text-align: center;'> Your payment is <u>Successful</u>.We have sent an email confirmation to <span style='color:blue'> " + Params[18] + "</span> and a SMS   to " + Params[17] + "  confirming your transaction.   </p> </div>  <div style = 'padding: 10px; padding-bottom:0px' > <p class='message'>  If you don’t get an Email or SMS from us within 1 hour then, write to us at<span style='color:blue'> support@sabpaisa.in.</span>  </p>  </div> </div> </div> <div style='margin-bottom:20px;'></div> <center>  <button ><a href='../Home/PrintForm'> Print Form</a></button>");

            }
            else
            {
                Response.Write("<div align='center'><center> <h1>Payment Transaction Details</h1><br/> <span style=" + " font-size:x-large; font-weight:bold " + ">आपली रक्कम स्वीकारण्यात आली नाही पुन्हा पर्यंत करावा 3  <a href='http://msbos.mh-ssc.ac.in/'>Click Here</a></span></center> </div>");
            }
            _Con.Close();

        }
        catch (Exception exe)
        {

            Response.Write("<div align='center'><center> <h1>Payment Transaction Details</h1><br/> <span style=" + " font-size:x-large; font-weight:bold " + ">आपली रक्कम स्वीकारण्यात आली नाही पुन्हा पर्यंत करावा 3 "+exe+"<a href='http://msbos.mh-ssc.ac.in/'>Click Here</a></span></center> </div>");
            _Con.Close();
        }


    }
}
