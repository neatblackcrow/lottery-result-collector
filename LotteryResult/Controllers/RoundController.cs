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
    public class RoundController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: Round
        //[Authorize (Roles = "กองออกรางวัล")]
        [HttpGet]
        public ActionResult Index()
        {
            return View(_dbContext.round.ToArray());
        }

        // GET: Round/Details/5
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

        // GET: Round/Create
        [HttpGet]
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
            catch (DbUpdateException exception)
            {
                SqlException ex = (SqlException)exception.InnerException.InnerException;

                if (ex.Errors.Count > 0 && ex.Errors[0].Number == 547)
                {
                    var round = _dbContext.round.Find(id);
                    ModelState.AddModelError("", "ไม่สามารถลบงวดได้ เนื่องจากงวดได้ถูกใช้ในระบบเรียบร้อยแล้ว");
                    return View(round);
                }
                else
                {
                    var round = _dbContext.round.Find(id);
                    ModelState.AddModelError("", ex.Message);
                    return View(round);
                }
            }
            catch (Exception ex)
            {
                var round = _dbContext.round.Find(id);
                ModelState.AddModelError("", ex.Message);
                return View(round);
            }
        }
    }
}
