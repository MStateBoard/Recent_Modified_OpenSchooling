using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Open_Schooling.Models
{
    public class Registration_Model
    {
        public int Id { get; set; }
        public string Enrollment_No { get; set; }
        public string ApplicationId { get; set; }
        [Required(ErrorMessage = "  First_Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "  Middle_Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Middle_Name { get; set; }
        [Required(ErrorMessage = "  Last_Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "  Mother_Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Mother_Name { get; set; }
        [RegularExpression(@"^(\d{12})$", ErrorMessage = "Not a valid Adhar number")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Please fill the valid Adhar number")]
        public string Adhar_no { get; set; }
        [Required(ErrorMessage = "You must provide a mobile number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid  number")]
        public string Mobile_No { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = " Village  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid Village name")]
       
        public string Village { get; set; }
        [DataType(DataType.Text)]

        [Required(ErrorMessage = "  District  is required")]
        public string District { get; set; }
        [DataType(DataType.Text)]

        [Required(ErrorMessage = "  Taluka  is required")]
        public string Taluka { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "Not a valid Pin Code number")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Please fill the valid Pin Code number")]
        [Required(ErrorMessage = "  pin_code  is required")]
        public string Pin_Code { get; set; }
        [Required(ErrorMessage = "  dob  is required")]
        public string Date_of_Birth { get; set; }
        [Required(ErrorMessage = "  dob  is required")]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Enter DOB in Words only")]

        public string DOB_Words { get; set; }
        [Required(ErrorMessage = " required.....")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid Village ")]
        public string DOB_Village { get; set; }
        [Required(ErrorMessage = " required.....")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid Taluka ")]
        public string DOB_Taluka { get; set; }
        [Required(ErrorMessage = " required.....")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid District")]
        public string DOB_District { get; set; }
        [Required(ErrorMessage = " Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = " required.....")]
        public string Gender { get; set; }
        [Required(ErrorMessage = " required.....")]
        public string Standard { get; set; }
        [Required(ErrorMessage = " required.....")]
        public string Medium { get; set; }

        public string Age_Certified_Proof { get; set; }
        [Required(ErrorMessage = " required.....")]
        public string District_Center { get; set; }
        [Required(ErrorMessage = " required.....")]
        public string Taluka_Center { get; set; }
        [Required(ErrorMessage = " required.....")]
        public string Center { get; set; }
        public string Center_Code { get; set; }
        public string Previous_Attend_School_YN { get; set; }
        public string Self_Declaration_Not_Gone_School { get; set; }
        public string Previous_Attend_School { get; set; }
        public string Date_Of_Leaving_Last_Attended_School { get; set; }
        public string Handicap { get; set; }
        public string Candidate_Handicaped_YN { get; set; }
        public string Subject_List { get; set; }
        public string Subject1 { get; set; }
        public string Subject2 { get; set; }
        public string Subject3 { get; set; }
        public string Subject4 { get; set; }
        public string Subject5 { get; set; }
        public string Minority_Religion { get; set; }
        public string Exam_Form_Disable { get; set; }
        public string Type_Of_User { get; set; }
        public string Nsqf_Subject { get; set; }
        public string Hall_Ticket { get; set; }
        public string Photo { get; set; }
        public string Password { get; set; }
        public string Signature { get; set; }
        public string Payment_Status { get; set; }
        public string Ec_Status { get; set; }
        public string ip { get; set; }
        public Nullable<System.DateTime> DateNow { get; set; }
        public string Year_Id { get; set; }

        public HttpPostedFileBase Upload_Photo { get; set; }
        public HttpPostedFileBase Upload_Sign { get; set; }
        public HttpPostedFileBase AgeCertificate { get; set; }
        public List<string> SUB { get; set; }
        public List<string> SUBNSQF { get; set; }

        public List<Tbl_Subject> SubjectListA { get; set; }
        public List<Tbl_Subject> SubjectListB { get; set; }
        public List<Tbl_Subject> SubjectListC { get; set; }
        public List<Tbl_Subject> SubjectListD { get; set; }
        public List<Tbl_Subject> SubjectListD_NSQF { get; set; }

        public string Class_Of_Last_Attend_8 { get; set; }
        public string Class_Of_Last_Attend_5 { get; set; }
        public string Seat_No { get; set; }
    }
}