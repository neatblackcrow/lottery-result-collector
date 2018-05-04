using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models.AnonymousModels
{
    public class userLogin
    {
        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_username")]
        public string username { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_password")]
        [DataType(DataType.Password)]
        public string hashed_password { get; set; }
    }
}