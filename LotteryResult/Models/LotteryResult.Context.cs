﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LotteryResult.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LottoResultContext : DbContext
    {
        public LottoResultContext()
            : base("name=LottoResultContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<result> result { get; set; }
        public virtual DbSet<reward_type> reward_type { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<round> round { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<result_data> result_data { get; set; }
        public virtual DbSet<ad_message> ad_message { get; set; }
    }
}
