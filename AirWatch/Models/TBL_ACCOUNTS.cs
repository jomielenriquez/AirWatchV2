//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirWatch.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_ACCOUNTS
    {
        public System.Guid ACCOUNTID { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string EMAIL { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public System.DateTime CREATEDDATE { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDDATE { get; set; }
        public string UPDATEDBY { get; set; }
    }
}
