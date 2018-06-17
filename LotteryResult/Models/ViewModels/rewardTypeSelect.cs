using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static LotteryResult.MvcApplication.ResultCollectionStats;

namespace LotteryResult.Models.ViewModels
{
    public class rewardTypeSelect
    {
        public List<RewardStatus> currentRewardList { get; set; }

        public int? selectedRewardId { get; set; }

        public rewardTypeSelect()
        {
            selectedRewardId = null;
        }
    }
}