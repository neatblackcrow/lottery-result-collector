namespace LotteryResult.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LottoResultContext : DbContext
    {
        public LottoResultContext()
            : base("name=LottoResultContext")
        {
        }

        public virtual DbSet<result> result { get; set; }
        public virtual DbSet<reward_type> reward_type { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<round> round { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<reward_type>()
                .Property(e => e.format)
                .IsUnicode(false);

            modelBuilder.Entity<reward_type>()
                .HasMany(e => e.result)
                .WithRequired(e => e.reward_type)
                .HasForeignKey(e => e.reward_type_id);

            modelBuilder.Entity<reward_type>()
                .HasOptional(e => e.reward_type1)
                .WithRequired(e => e.reward_type2);

            modelBuilder.Entity<role>()
                .HasMany(e => e.user)
                .WithOptional(e => e.role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<round>()
                .HasMany(e => e.result)
                .WithRequired(e => e.round)
                .HasForeignKey(e => e.round_id);

            modelBuilder.Entity<user>()
                .Property(e => e.hashed_password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.result)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.result_a_userid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.result1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.result_b_userid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.result2)
                .WithRequired(e => e.user2)
                .HasForeignKey(e => e.result_final_userid)
                .WillCascadeOnDelete(false);
        }
    }
}
