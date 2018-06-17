using LotteryResult.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LotteryResult.Controllers
{
    [Authorize]
    public class AdMessageController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: AdMessage
        public ActionResult Index()
        {
            return View(_dbContext.round.ToArray());
        }

        // GET: AdMessage/Details/5
        public ActionResult Details(int id)
        {
            round r = _dbContext.round.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
            }
            return View(r);
        }

        // GET: AdMessage/Edit/5
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

        // POST: AdMessage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, round r)
        {
            r.ad_message.create_timestamp = DateTime.Now;
            r.ad_message.create_by = ((user)HttpContext.Session["user"]).id;

            if (ModelState.IsValid)
            {
                try
                {
                    round changingRound = _dbContext.round.Find(id);
                    if (changingRound.ad_message == null)
                    {
                        changingRound.ad_message = r.ad_message;
                    }
                    else
                    {
                        changingRound.ad_message.create_by = r.ad_message.create_by;
                        changingRound.ad_message.create_timestamp = r.ad_message.create_timestamp;
                        changingRound.ad_message.advertise_msg = r.ad_message.advertise_msg;
                    }

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

        // GET: AdMessage/Delete/5
        public ActionResult Delete(int id)
        {
            round r = _dbContext.round.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
            }
            return View(r);
        }

        // POST: AdMessage/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, round r)
        {
            try
            {
                round removingRound = _dbContext.round.Find(id);
                _dbContext.ad_message.Remove(removingRound.ad_message);

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
