using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models
{
    [MetadataType(typeof(IResultMetadata))]
    public partial class result : IResultMetadata
    {
    }

    public interface IResultMetadata
    {
        int id { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_round")]
        int round_id { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_name")]
        int reward_type_id { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_order")]
        int reward_order { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_result_order")]
        int result_order { get; set; }

    }
}