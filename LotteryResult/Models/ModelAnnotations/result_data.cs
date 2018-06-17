using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models
{
    [MetadataType(typeof(IResultDataMetadata))]
    public partial class result_data : IResultDataMetadata
    {
    }

    public interface IResultDataMetadata
    {
        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_result_name")]
        string result { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_timestamp")]
        System.DateTime create_timestamp { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_by")]
        int create_by { get; set; }

        [Required]
        int result_id { get; set; }

        int id { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "display_name_result_confirmed")]
        bool is_confirmed_result { get; set; }

    }
}