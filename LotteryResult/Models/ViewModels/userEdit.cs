using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models.ViewModels
{
    public class userEdit
    {
        public int id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_username")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_username_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_username")]
        public string username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_old_password")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_old_password")]
        [DataType(DataType.Password)]
        public string old_password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_new_password")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_new_password")]
        [DataType(DataType.Password)]
        public string hashed_password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_confirm_password")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_confirm_password")]
        [DataType(DataType.Password)]
        [Compare("hashed_password", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_confirm_password_compare")]
        public string confirmed_password { get; set; }
    }
}