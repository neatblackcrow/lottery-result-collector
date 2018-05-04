using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models.AnonymousModels
{
    public class userChange
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_username")]
        public string username { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_old_password")]
        [DataType(DataType.Password)]
        public string old_password { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_password")]
        [DataType(DataType.Password)]
        public string hashed_password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_firstname")]
        public string firstname { get; set; }

        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_lastname")]
        public string lastname { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_role")]
        public byte role_id { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_timestamp")]
        public DateTime create_timestamp { get; set; }
    }
}