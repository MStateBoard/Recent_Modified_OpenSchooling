using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Open_Schooling.Models
{
    public class Division_List_Model
    {
        public int Id { get; set; }
        public string Div_Code { get; set; }

        public string ApplicationId { get; set; }

        public string First_Name { get; set; }
       
        public string Last_Name { get; set; }
       
        public string Mobile_No { get; set; }

        public string Standard { get; set; }

        public string Center { get; set; }
      
        public string Subject_List { get; set; }
       
        public string Center_Code { get; set; }
      
        public int countData { get; set; }
    }
}