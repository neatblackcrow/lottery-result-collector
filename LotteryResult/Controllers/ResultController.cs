using LotteryResult.Models;
using LotteryResult.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static LotteryResult.MvcApplication;
using static LotteryResult.MvcApplication.ResultCollectionStats;

namespace LotteryResult.Controllers
{
    [Authorize]
    public class ResultController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: Result
        [HttpGet]
        public ActionResult Index()
        {
            var results = from rs in _dbContext.result
                          join rt in _dbContext.reward_type on rs.reward_type_id equals rt.id
                          join ro in _dbContext.round on rs.round_id equals ro.id
                          join rd in _dbContext.result_data on rs.id equals rd.result_id
                          where rd.is_confirmed_result == true
                          select new resultViewModel {
                              result_id = rs.id,
                              round_date = ro.date,
                              round = ro.round1,
                              reward_name = rt.name,
                              reward_order = rs.reward_order,
                              result_order = rs.result_order,
                              confirmed_result = rd.result
                          };

            return View(results.ToList());
        }

        // GET: Result/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Result/Create
        public ActionResult Create()
        {
            GenericPrincipal principal = (GenericPrincipal)HttpContext.User;
            FormsIdentity identity = (FormsIdentity)principal.Identity;
            string role = principal.Claims.Where(c => c.Type == identity.RoleClaimType).First().Value;

            ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];

            if (role == "ผู้บันทึกผลรางวัล")
            {
                return View("DataEntryInsert");
            }
            else if (role == "ผู้ตรวจสอบการบันทึกผลรางวัล")
            {
                if (rcs.currentRound == null)
                {
                    var latestRoundQuery = from round in _dbContext.round
                                           orderby round.create_timestamp descending
                                           select round;
                                           
                    return View("DataAdminRoundSelect", latestRoundQuery.First());
                }
                else if (rcs.currentReward == null)
                {
                    if (rcs.currentRewardList == null)
                    {
                        rcs.currentRewardList = new List<RewardStatus>();
                        List<reward_type> allRewards = (from reward in _dbContext.reward_type
                                                        where reward.is_active == true
                                                        select reward).ToList();
                        foreach (reward_type r in allRewards)
                        {
                            rcs.currentRewardList.Add(new RewardStatus
                            {
                                isUsed = false,
                                reward = r
                            });
                        }
                        HttpContext.Application.Set("result_collection_stats", rcs);
                    }
                    return View("DataAdminRewardTypeSelect", new rewardTypeSelect {
                        currentRewardList = rcs.currentRewardList
                    });
                }
                else
                {
                    return View("DataAdminCrossCheck");
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Result/RoundConfirmation
        [HttpPost]
        public ActionResult RoundConfirmation(round r)
        {
            if (ModelState.IsValid)
            {
                ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];
                rcs.currentRound = r;
                HttpContext.Application.Set("result_collection_stats", rcs);

                return RedirectToAction("Create");
            }
            else
            {
                return View("DataAdminRoundSelect", r);
            }
           
        }

        // POST: Result/RewardSelect
        [HttpPost]
        public ActionResult RewardSelect(rewardTypeSelect r)
        {
            if (r.selectedRewardId != null)
            {
                ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];

                RewardStatus rewardStatus = rcs.currentRewardList.Find(rs => rs.reward.id == r.selectedRewardId);
                rewardStatus.isUsed = true;

                rcs.currentReward = rewardStatus.reward;

                HttpContext.Application.Set("result_collection_stats", rcs);

                return RedirectToAction("Create");
            }
            else
            {
                ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];

                return View("DataAdminRewardTypeSelect", new rewardTypeSelect
                {
                    currentRewardList = rcs.currentRewardList
                });
            }
            
        }

        // POST: Result/WaitForRewardType
        [HttpPost]
        public JsonResult WaitForRewardType()
        {

            ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];
            if (rcs.currentRound != null && rcs.currentReward != null)
            {
                return Json(new { status = "ready"});
            }
            else
            {
                return Json(new { status = "not-ready" });
            }

        }

        // POST: Result/GetRoundAndRewardType
        [HttpPost]
        public JsonResult GetRoundAndRewardType()
        {
            ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];

            return Json(new {
                round = new {
                    date = rcs.currentRound.date.ToString("dd-MM-yyyy"),
                    round = rcs.currentRound.round1
                },
                rewardType = new {
                    rcs.currentReward.name,
                    rcs.currentReward.instance,
                    rcs.currentReward.format,
                    rcs.currentReward.reward_amount
                }
            });

        }

        // GET: Result/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Result/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Result/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Result/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
