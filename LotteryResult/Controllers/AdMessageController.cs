using LotteryResult.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LotteryResult.Controllers
{
    [Authorize (Roles = "ผู้ดูแลระบบ,ประชาสัมพันธ์")]
    public class AdMessageController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: AdMessage
        [HttpGet]
        public ActionResult Index(int? year)
        {
            IEnumerable<round> roundList;
            if (year == null)
            {
                year = Int32.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("en-US")));
            }

            roundList = (from r in _dbContext.round
                         where r.date.Year == year
                         select r).ToList();

            var roundQuery = (from r in _dbContext.round
                              select r.date);

            List<SelectListItem> yearSelectItemList = new List<SelectListItem>();

            foreach (DateTime eachDate in roundQuery.ToList())
            {
                yearSelectItemList.Add(new SelectListItem() { Text = eachDate.ToString("yyyy", new CultureInfo("th-TH")), Value = eachDate.Year.ToString() });
            }

            ViewBag.year = yearSelectItemList.Distinct(new SelectListItemComparer());

            return View(roundList);
        }

        // GET: AdMessage/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }
            round r = _dbContext.round.Find(id);

            if (r == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(r);
        }

        // GET: AdMessage/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }
            round r = _dbContext.round.Find(id);

            if (r == null)
            {
                throw new HttpException(404, "Not found");
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
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }
            round r = _dbContext.round.Find(id);

            if (r == null)
            {
                throw new HttpException(404, "Not found");
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
