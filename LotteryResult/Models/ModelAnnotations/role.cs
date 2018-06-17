using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models
{
    [MetadataType(typeof(IRoleMetadata))]
    public partial class role : IRoleMetadata
    {
    }

    public interface IRoleMetadata
    {
        byte id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_role")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_role_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_role")]
        string name { get; set; }

    }
}