//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class result_data
    {
        public string result { get; set; }
        public System.DateTime create_timestamp { get; set; }
        public int create_by { get; set; }
        public int result_id { get; set; }
        public int id { get; set; }
        public bool is_confirmed_result { get; set; }
    
        public virtual result result1 { get; set; }
        public virtual user user { get; set; }
    }
}
