using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models.ViewModels
{
    public class userLogin
    {
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_username")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_username_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_username")]
        public string username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_password")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_password")]
        [DataType(DataType.Password)]
        public string hashed_password { get; set; }
    }
}