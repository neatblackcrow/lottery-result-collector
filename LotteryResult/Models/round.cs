namespace LotteryResult.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("round")]
    public partial class round
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public round()
        {
            result = new HashSet<result>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_date")]
        public DateTime date { get; set; }

        [Column("round")]
        [Display(ResourceType = typeof(Resources), Name = "display_name_round")]
        public int round1 { get; set; }

        [StringLength(250)]
        [Display(ResourceType = typeof(Resources), Name = "display_name_ad_msg")]
        public string advertise_msg { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_by")]
        public int create_by { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "display_name_create_timestamp")]
        public DateTime create_timestamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<result> result { get; set; }

        public virtual user user { get; set; }
    }
}
