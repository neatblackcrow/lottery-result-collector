using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models.ViewModels
{
    public class resultViewModel
    {
        [Key]
        public int result_id { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_date")]
        public System.DateTime round_date { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_round")]
        public int round { get; set; }

        [Required]
        [StringLength(100)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_name")]
        public string reward_name { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_order")]
        public int reward_order { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_result_order")]
        public int result_order { get; set; }

        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_result_name")]
        public string confirmed_result { get; set; }

    }
}