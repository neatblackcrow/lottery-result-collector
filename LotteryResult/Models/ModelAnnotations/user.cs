using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LotteryResult.Models
{
    [MetadataType(typeof(IUserMetadata))]
    public partial class user : IUserMetadata
    {
    }

    public interface IUserMetadata
    {
        int id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_username")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_username_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_username")]
        string username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_password")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_password")]
        [DataType(DataType.Password)]
        string hashed_password { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_old_password")]
        [DataType(DataType.Password)]
        string old_password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_firstname")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_firstname_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_firstname")]
        string firstname { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_lastname")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_lastname_length")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_lastname")]
        string lastname { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_role")]
        byte role_id { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_timestamp")]
        System.DateTime create_timestamp { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "error_msg_isactive")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_is_active")]
        bool is_active { get; set; }

    }
}