using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LotteryResult.Models
{
    [MetadataType(typeof(IRoundMetadata))]
    public partial class round : IRoundMetadata
    {
    }

    public interface IRoundMetadata
    {
        int id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_date")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_date")]
        [DataType(DataType.Date)]
        System.DateTime date { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_round")]
        [Range(1, 52, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_round_range")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_round")]
        int round1 { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_by")]
        int create_by { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_timestamp")]
        System.DateTime create_timestamp { get; set; }
    }
}