using LotteryResult.Models;
using LotteryResult.Models.JsonModels;
using LotteryResult.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
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
        public ActionResult Index(int? year, int round = 1, string reward_type = "all")
        {
            var resultsQuery = from rs in _dbContext.result
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

            // Filter
            if (year == null)
            {
                year = Int32.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("en-US")));
            }
            resultsQuery = resultsQuery.Where(r => r.round_date.Year == year);

            resultsQuery = resultsQuery.Where(r => r.round == round);

            if (reward_type != "all")
            {
                resultsQuery = resultsQuery.Where(r => r.reward_name == reward_type);
            }

            // ViewBags
            var roundQuery = from r in _dbContext.round
                             select r.date;
            List<SelectListItem> yearSelectItemList = new List<SelectListItem>();
            foreach (DateTime eachDate in roundQuery.ToList())
            {
                yearSelectItemList.Add(new SelectListItem() { Text = eachDate.ToString("yyyy", new CultureInfo("th-TH")), Value = eachDate.Year.ToString() });
            }
            ViewBag.year = yearSelectItemList.Distinct(new SelectListItemComparer());

            var rewardTypeQuery = from rt in _dbContext.reward_type
                                  where rt.is_active == true
                                  select rt.name;
            List<SelectListItem> rewardTypeSelectItemList = new List<SelectListItem>();
            rewardTypeSelectItemList.Add(new SelectListItem() { Text = "ทั้งหมด", Value = "all" });
            foreach (String eachRewardType in rewardTypeQuery.ToList())
            {
                rewardTypeSelectItemList.Add(new SelectListItem() { Text = eachRewardType, Value = eachRewardType });
            }
            ViewBag.reward_type = rewardTypeSelectItemList;

            List<SelectListItem> roundSelectItemList = new List<SelectListItem>();
            for (int i=1; i<=24; i++)
            {
                roundSelectItemList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.round = roundSelectItemList;

            return View(resultsQuery.ToList());
        }

        // GET: Result/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Result/Create
        [HttpGet]
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
            else if (role == "ผู้ดูแลระบบ")
            {
                if (rcs.currentRound == null)
                {
                    try
                    {
                        round latestRoundQuery = (from round in _dbContext.round
                                                  where round.is_active == true
                                                  select round).SingleOrDefault();

                        ViewBag.status = "ok";
                        return View("DataAdminRoundSelect", latestRoundQuery);
                    }
                    catch (InvalidOperationException ex)
                    {
                        ViewBag.status = "error";
                        return View("DataAdminRoundSelect");
                    }
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

        // GET: Result/WaitForRewardType
        [HttpGet]
        public JsonResult WaitForRewardType()
        {

            ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];
            if (rcs.currentRound != null && rcs.currentReward != null)
            {
                return Json(new { status = "ok", message = "ready"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "ok", message = "not-ready" }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Result/GetRoundAndRewardType
        [HttpGet]
        public JsonResult GetRoundAndRewardType()
        {
            ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];
            if (rcs.currentRound != null && rcs.currentReward != null)
            {
                return Json(new
                {
                    status = "ok",
                    message = "round-and-reward_type",
                    payload = new
                    {
                        round = new
                        {
                            rcs.currentRound.id,
                            date = rcs.currentRound.date.ToString("dd-MM-yyyy"),
                            round = rcs.currentRound.round1
                        },
                        rewardType = new
                        {
                            rcs.currentReward.id,
                            rcs.currentReward.name,
                            rcs.currentReward.instance,
                            rcs.currentReward.format,
                            rcs.currentReward.reward_amount
                        }
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {
                    status = "error",
                    message = "round-and-reward_type-not-selected"
                }, JsonRequestBehavior.AllowGet);
            }
            
        }

        // Post: Result/InsertResult
        [HttpPost]
        public JsonResult InsertResult(ResultInsert request)
        {
            ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];
            
            // Ajax request must be conform with the application variables.
            if (request.round == rcs.currentRound.id &&
                request.reward_type == rcs.currentReward.id &&
                request.resultOrder == rcs.currentResultOrder &&
                request.result != null &&
                request.result.Length == rcs.currentReward.format.Length)
            {
                try
                {
                    var resultQuery = from rs in _dbContext.result
                                      where rs.round_id == rcs.currentRound.id &&
                                            rs.reward_type_id == rcs.currentReward.id &&
                                            rs.reward_order == rcs.currentRewardOrder &&
                                            rs.result_order == rcs.currentResultOrder
                                      select rs;

                    result result = resultQuery.SingleOrDefault();
                    if (result != null)
                    {
                        // Result already exists, Check for duplicate user before adding result_data
                        int userId = ((user)HttpContext.Session["user"]).id;
                        if (result.result_data.Where(rd => rd.create_by == userId).Count() == 0)
                        {
                            result.result_data.Add(new result_data
                            {
                                result = request.result,
                                create_timestamp = DateTime.Now,
                                create_by = userId,
                                is_confirmed_result = false
                            });

                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            return Json(new
                            {
                                status = "error",
                                message = "duplicate-request-data"
                            });
                        }
                    }
                    else
                    {
                        // Create a new result, then add a result_data
                        result = new result
                        {
                            round_id = rcs.currentRound.id,
                            reward_type_id = rcs.currentReward.id,
                            reward_order = rcs.currentRewardOrder,
                            result_order = rcs.currentResultOrder
                        };

                        result = _dbContext.result.Add(result);

                        result.result_data.Add(new result_data
                        {
                            result = request.result,
                            create_timestamp = DateTime.Now,
                            create_by = ((user)HttpContext.Session["user"]).id,
                            is_confirmed_result = false
                        });

                        _dbContext.SaveChanges();
                    }

                    return Json(new
                    {
                        status = "ok",
                        message = "result-inserted-successfully"
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        status = "error",
                        message = ex.Message
                    });
                }
            }
            else
            {
                return Json(new {
                    status = "error",
                    message = "invalid-request-data"
                });
            }
        }

        // GET: Result/AllowNextInsert
        [HttpGet]
        public JsonResult AllowNextInsert(int round, int reward_type, int resultOrder)
        {
            /*
             * Allow a next input when a is_confirmed_result = true in requested result_data is present.
             * Which means, the result was verified by admin.
             * 
             * The request doesn't need to be conform with current application variables.
             * This allow the data entry update their UI after the result was validated.
            */

            var resultQuery = from rs in _dbContext.result
                              join rd in _dbContext.result_data on rs.id equals rd.result_id
                              where rs.round_id == round &&
                                    rs.reward_type_id == reward_type &&
                                    rs.result_order == resultOrder &&
                                    rd.is_confirmed_result == true
                              select rs;

            if (resultQuery.SingleOrDefault() != null)
            {
                return Json(new
                {
                    status = "ok",
                    message = "allow-next-input"
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new {
                    status = "ok",
                    message = "wait"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Result/ValidateResult
        [HttpGet]
        public JsonResult ValidateResult(int round, int reward_type, int resultOrder)
        {
            ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];

            // Ajax request must conform with application variables.
            if (round == rcs.currentRound.id &&
                reward_type == rcs.currentReward.id &&
                resultOrder == rcs.currentResultOrder)
            {
                var resultQuery = from rs in _dbContext.result
                                  where rs.round_id == rcs.currentRound.id &&
                                        rs.reward_type_id == rcs.currentReward.id &&
                                        rs.reward_order == rcs.currentRewardOrder &&
                                        rs.result_order == rcs.currentResultOrder
                                  select rs;

                result result = resultQuery.SingleOrDefault();
                if (result != null && result.result_data.Where(rd => rd.is_confirmed_result == false).Count() == 2)
                {
                    List<result_data> resultData = result.result_data.ToList();
                    string message;
                    if (resultData[0].result == resultData[1].result)
                    {
                        try
                        {
                            result.result_data.Add(new result_data
                            {
                                result = resultData[0].result,
                                create_timestamp = DateTime.Now,
                                create_by = ((user)HttpContext.Session["user"]).id,
                                is_confirmed_result = true
                            });

                            _dbContext.SaveChanges();
                            updateApplicationState();

                            message = "result-is-valid";
                        }
                        catch (Exception ex)
                        {
                            return Json(new
                            {
                                status = "error",
                                message = ex.Message
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        message = "correction-required";
                    }

                    return Json(new
                    {
                        status = "ok",
                        message,
                        payload = new
                        {
                            result_data_a = new
                            {
                                resultData[0].result,
                                resultData[0].user.username
                            },
                            result_data_b = new
                            {
                                resultData[1].result,
                                resultData[1].user.username
                            }
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (result != null && result.result_data.Where(rd => rd.is_confirmed_result == false).Count() == 1)
                {
                    result_data rs = result.result_data.Single();
                    return Json(new {
                        status = "ok",
                        message = "one-user-input",
                        payload = new
                        {
                            rs.result,
                            rs.user.username
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        status = "ok",
                        message = "wait-for-input"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new {
                    status = "error",
                    message = "invalid-request-data"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Result/CorrectResult
        [HttpPost]
        public JsonResult CorrectResult(ResultInsert request)
        {
            ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];

            // Ajax request must be conform with the application variables.
            if (request.round == rcs.currentRound.id &&
                request.reward_type == rcs.currentReward.id &&
                request.resultOrder == rcs.currentResultOrder &&
                request.result != null &&
                request.result.Length == rcs.currentReward.format.Length)
            {
                var resultQuery = from rs in _dbContext.result
                                  where rs.round_id == rcs.currentRound.id &&
                                        rs.reward_type_id == rcs.currentReward.id &&
                                        rs.reward_order == rcs.currentRewardOrder &&
                                        rs.result_order == rcs.currentResultOrder
                                  select rs;

                result result = resultQuery.SingleOrDefault();
                if (result != null && result.result_data.Where(rd => rd.is_confirmed_result == false).Count() == 2)
                {
                    try
                    {
                        result.result_data.Add(new result_data
                        {
                            result = request.result,
                            create_timestamp = DateTime.Now,
                            create_by = ((user)HttpContext.Session["user"]).id,
                            is_confirmed_result = true
                        });

                        _dbContext.SaveChanges();
                        updateApplicationState();

                        return Json(new
                        {
                            status = "ok",
                            message = "result-corrected"
                        });
                    }
                    catch (Exception ex)
                    {
                        return Json(new
                        {
                            status = "error",
                            message = ex.Message
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        status = "error",
                        message = "invalid-request-data"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    status = "error",
                    message = "invalid-request-data"
                });
            }
        }

        private void updateApplicationState()
        {
            ResultCollectionStats rcs = (ResultCollectionStats)HttpContext.Application["result_collection_stats"];
            if (rcs.currentResultOrder < rcs.currentReward.instance)
            {
                rcs.currentResultOrder += 1;
            }
            else
            {
                rcs.currentResultOrder = 1;
                rcs.currentRewardOrder += 1;
                rcs.currentReward = null;
            }
            HttpContext.Application.Set("result_collection_stats", rcs);
        }

        [HttpGet]
        public void CreateResultTxt()
        {

            var selectResult = from rs in _dbContext.result
                               join r in _dbContext.round on rs.round_id equals r.id
                               join rt in _dbContext.reward_type on rs.reward_type_id equals rt.id
                               join rd in _dbContext.result_data on rs.id equals rd.result_id
                               where r.is_active == true && rd.is_confirmed_result == true
                               select new { rt.reward_code, rs.result_order, rd.result };

            StringWriter oStringWriter = new StringWriter();
            foreach (var result in selectResult.ToList())
            {
                oStringWriter.WriteLine(String.Format("{0,1}{1,3}{2}", result.reward_code, result.result_order, result.result.PadRight(6, ' ')));
            }

            Response.ContentType = "text/plain";
            Response.AddHeader("content-disposition", "attachment;filename=result.txt");
            Response.Clear();

            using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
            {
                writer.Write(oStringWriter.ToString());
            }
            Response.End();
        }

        [HttpGet]
        public void CreateResultExcel()
        {

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
