﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AirWatchDBEntities : DbContext
    {
        public AirWatchDBEntities()
            : base("name=AirWatchDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<TBL_ACCOUNTS> TBL_ACCOUNTS { get; set; }
        public DbSet<TBL_ACTIVATION> TBL_ACTIVATION { get; set; }
    }
}
