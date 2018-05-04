namespace LotteryResult.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class reward_type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public reward_type()
        {
            result = new HashSet<result>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_name")]
        public string name { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_instance")]
        public int instance { get; set; }

        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_format")]
        public string format { get; set; }

        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_reward_amount")]
        public string reward_amount { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_timestamp")]
        public DateTime create_timestamp { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_by")]
        public int create_by { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<result> result { get; set; }

        public virtual user user { get; set; }
    }
}
