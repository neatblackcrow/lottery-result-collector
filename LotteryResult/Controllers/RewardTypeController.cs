using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LotteryResult.Models;

namespace LotteryResult.Controllers
{
    [Authorize]
    public class RewardTypeController : Controller
    {
        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: RewardType
        [HttpGet]
        public ActionResult Index()
        {
            return View(_dbContext.reward_type.ToArray());
        }

        // GET: RewardType/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }
            reward_type r = _dbContext.reward_type.Find(id);

            if (r == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(r);

        }

        // GET: RewardType/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RewardType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(reward_type r)
        {
            r.create_timestamp = DateTime.Now;
            r.create_by = ((user)HttpContext.Session["user"]).id;

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.reward_type.Add(r);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(r);
                }
            }
            else
            {
                ModelState.AddModelError("", "กรุณากรอกข้อมูลให้ถูกต้อง");
                return View(r);
            }
        }

        // GET: RewardType/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }
            reward_type r = _dbContext.reward_type.Find(id);

            if (r == null)
            {
                throw new HttpException(404, "Not found");
            }
            else
            {

                return View(r);
            }
        }

        // POST: RewardType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, reward_type r)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    reward_type changingReward = _dbContext.reward_type.Find(id);

                    changingReward.name = r.name;
                    changingReward.instance = r.instance;
                    changingReward.format = r.format;
                    changingReward.reward_amount = r.reward_amount;
                    changingReward.is_active = r.is_active;

                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(r);
                }
            }
            else
            {
                ModelState.AddModelError("", "กรุณากรอกข้อมูลให้ถูกต้อง");
                return View(r);
            }
        }

        // GET: RewardType/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }
            reward_type r = _dbContext.reward_type.Find(id);

            if (r == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(r);
        }

        // POST: RewardType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, reward_type r)
        {
            try
            {
                reward_type removingRewardType = _dbContext.reward_type.Find(id);
                _dbContext.reward_type.Remove(removingRewardType);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException exception)
            {
                SqlException ex = (SqlException)exception.InnerException.InnerException;

                if (ex.Errors.Count > 0 && ex.Errors[0].Number == 547)
                {
                    var rewardType = _dbContext.reward_type.Find(id);
                    ModelState.AddModelError("", "ไม่สามารถลบประเภทรางวัลได้ เนื่องจากประเภทรางวัลได้ถูกใช้ในระบบเรียบร้อยแล้ว ให้ปิดใช้งานประเภทรางวัลแทน");
                    return View(rewardType);
                }
                else
                {
                    var rewardType = _dbContext.reward_type.Find(id);
                    ModelState.AddModelError("", ex.Message);
                    return View(rewardType);
                }
            }
            catch (Exception ex)
            {
                var rewardType = _dbContext.reward_type.Find(id);
                ModelState.AddModelError("", ex.Message);
                return View(rewardType);
            }
        }
    }
}
