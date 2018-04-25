namespace LotteryResult.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("result")]
    public partial class result
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int round_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int reward_type_id { get; set; }

        public short reward_order { get; set; }

        public short result_order { get; set; }

        public int result_a { get; set; }

        public DateTime result_a_timestamp { get; set; }

        public int result_a_userid { get; set; }

        public int result_b { get; set; }

        public DateTime result_b_timestamp { get; set; }

        public int result_b_userid { get; set; }

        public int result_final { get; set; }

        public DateTime result_final_timestamp { get; set; }

        public int result_final_userid { get; set; }

        public virtual user user { get; set; }

        public virtual user user1 { get; set; }

        public virtual user user2 { get; set; }

        public virtual reward_type reward_type { get; set; }

        public virtual round round { get; set; }
    }
}
