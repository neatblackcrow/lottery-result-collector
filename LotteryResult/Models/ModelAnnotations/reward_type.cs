using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models
{
    [MetadataType(typeof(IRewardTypeMetadata))]
    public partial class reward_type : IRewardTypeMetadata
    {
    }

    public interface IRewardTypeMetadata
    {
        int id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_reward_name")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_reward_name_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_name")]
        string name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_reward_instance")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_reward_instance_range")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_instance")]
        int instance { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_reward_format")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_reward_format_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_format")]
        string format { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_reward_amount")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_reward_amount_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_amount")]
        string reward_amount { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_timestamp")]
        System.DateTime create_timestamp { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_by")]
        int create_by { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_isactive")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_is_active")]
        bool is_active { get; set; }
    }
}