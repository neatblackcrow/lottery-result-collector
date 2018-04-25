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
        public string name { get; set; }

        public short instance { get; set; }

        [Required]
        [StringLength(50)]
        public string format { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<result> result { get; set; }

        public virtual reward_type reward_type1 { get; set; }

        public virtual reward_type reward_type2 { get; set; }
    }
}
