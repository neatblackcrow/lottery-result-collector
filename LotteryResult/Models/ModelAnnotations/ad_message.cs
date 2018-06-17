using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models
{
    [MetadataType(typeof(IAdMessageMetadata))]
    public partial class ad_message : IAdMessageMetadata
    {
    }

    public interface IAdMessageMetadata
    {
        int id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_ad_msg")]
        [StringLength(250, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_ad_msg_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_ad_msg")]
        string advertise_msg { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_by")]
        int create_by { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_timestamp")]
        System.DateTime create_timestamp { get; set; }
    }
}