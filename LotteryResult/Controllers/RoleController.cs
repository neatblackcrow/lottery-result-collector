using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LotteryResult.Models;

namespace LotteryResult.Controllers
{
    public class RoleController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: Role
        public ActionResult Index()
        {
            return View(_dbContext.role.ToArray());
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            role r = _dbContext.role.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
            }
            return View(r);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(role r)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.role.Add(r);
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

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            role r = _dbContext.role.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
            }
            return View(r);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, role r)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    role changingRole = _dbContext.role.Find(id);
                    changingRole.name = r.name;
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

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            role r = _dbContext.role.Find(id);

            if (r == null)
            {
                return RedirectToAction("Index");
            }
            return View(r);
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, role r)
        {
            try
            {
                role removingRole = _dbContext.role.Find(id);
                _dbContext.role.Remove(removingRole);
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
