//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Open_Schooling.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Site_Status
    {
        public int ID { get; set; }
        public Nullable<int> Active_Status { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> Late_Date { get; set; }
        public string Type { get; set; }
        public string RegisterEorD { get; set; }
    }
}
