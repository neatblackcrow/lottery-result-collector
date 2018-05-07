using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LotteryResult.Models;

namespace LotteryResult.Controllers
{
    public class RoundController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: Round
        //[Authorize (Roles = "กองออกรางวัล")]
        public ActionResult Index()
        {
            return View(_dbContext.round.ToArray());
        }

        // GET: Round/Details/5
        public ActionResult Details(int id)
        {
            round r = _dbContext.round.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
            }
            return View(r);
        }

        // GET: Round/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Round/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(round r)
        {
            r.create_timestamp = DateTime.Now;
            r.advertise_msg = null;
            r.create_by = ((user)HttpContext.Session["user"]).id;

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.round.Add(r);
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

        // GET: Round/Edit/5
        public ActionResult Edit(int id)
        {

            round r = _dbContext.round.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                return View(r);
            }
        }

        // POST: Round/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, round r)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    round changingRound = _dbContext.round.Find(id);

                    changingRound.date = r.date;
                    changingRound.round1 = r.round1;

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

        // GET: Round/Delete/5
        public ActionResult Delete(int id)
        {
            round r = _dbContext.round.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
            }
            return View(r);
        }

        // POST: Round/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, round r)
        {
            try
            {
                round removingRound = _dbContext.round.Find(id);
                _dbContext.round.Remove(removingRound);
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
