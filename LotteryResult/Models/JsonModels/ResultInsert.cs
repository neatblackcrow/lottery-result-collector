using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LotteryResult.Models.JsonModels
{
    public class ResultInsert
    {
        public int round { get; set; }
        public int reward_type { get; set;}
        public int resultOrder { get; set; }
        public string result { get; set; }
    }
}