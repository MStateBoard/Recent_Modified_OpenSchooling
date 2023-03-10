using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Open_Schooling.Models
{
    public class CenterViewModel
    {
        public Tbl_Registration_Pre tbl_Registration_Pre { get; set; }
        public Tbl_Registration tbl_Registration { get; set; }
        public Tbl_payment Tbl_Payment { get; set; }
        public Center_Login_Information Center_Login { get; set; }
        //public tbl_CenterList CenterList { get; set; }
        //public Tbl_Exam_Hall_Tikit Exam_HallTikit { get; set; }

        public string Payment_Status { get; set; }
        public string Registration_Form { get; set; }
        public string Form_No { get; set; }
        public string ApplicationId { get; set; }

        public string Name { get; set; }
        public string Mobile_No { get; set; }
        public string EC_No { get; set; }
        public string Ec_Status { get; set; }
        public string Subject1 { get; set; }
        public string Subject2 { get; set; }
        public string Subject3 { get; set; }
        public string Subject4 { get; set; }
        public string Subject5 { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Exam_Form_Disable { get; set; }

        public string Seat_No { get; set; }
        public string Type_Of_User { get; set; }
        public string Photo { get; set; }
        public string Signature { get; set; }


        //--------------------------------------------
        public string EC_Year { get; set; }
        public string LastExamYear { get; set; }
        public string LastExamSeatNo { get; set; }
    }
}