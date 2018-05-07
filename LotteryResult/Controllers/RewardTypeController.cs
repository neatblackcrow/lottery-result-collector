﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LotteryResult.Models;

namespace LotteryResult.Controllers
{
    public class RewardTypeController : Controller
    {
        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: RewardType
        public ActionResult Index()
        {
            return View(_dbContext.reward_type.ToArray());
        }

        // GET: RewardType/Details/5
        public ActionResult Details(int id)
        {

            reward_type r = _dbContext.reward_type.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
            }
            return View(r);

        }

        // GET: RewardType/Create
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
        public ActionResult Edit(int id)
        {

            reward_type r = _dbContext.reward_type.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
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
        public ActionResult Delete(int id)
        {
            reward_type r = _dbContext.reward_type.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(r);
            }
        }
    }
}