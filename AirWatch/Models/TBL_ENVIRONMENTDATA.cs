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
    
    public partial class TBL_ENVIRONMENTDATA
    {
        public System.Guid ENVIRONMENTDATEID { get; set; }
        public int EDID { get; set; }
        public int HUMIDITY { get; set; }
        public int AMMONIA { get; set; }
        public int SULFURDIOXICE { get; set; }
        public int TEMPERATURE { get; set; }
        public int CARBONMONOXIDE { get; set; }
        public int NITROGENOXIDE { get; set; }
        public bool ISDISPLAYED { get; set; }
        public System.DateTime CREATEDDATE { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDDATE { get; set; }
        public string UPDATEDBY { get; set; }
    }
}
