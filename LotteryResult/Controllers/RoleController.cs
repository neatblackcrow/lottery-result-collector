using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LotteryResult.Models;

namespace LotteryResult.Controllers
{
    [Authorize(Roles = "ผู้ดูแลระบบ")]
    public class RoleController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: Role
        [HttpGet]
        public ActionResult Index()
        {
            return View(_dbContext.role.ToArray());
        }

        // GET: Role/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }

            role r = _dbContext.role.Find(id);

            if (r == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(r);
        }

        // GET: Role/Create
        [HttpGet]
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
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }

            role r = _dbContext.role.Find(id);

            if (r == null)
            {
                throw new HttpException(404, "Not found");
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }

            role r = _dbContext.role.Find(id);

            if (r == null)
            {
                throw new HttpException(404, "Not found");
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
            catch (DbUpdateException exception)
            {
                SqlException ex = (SqlException) exception.InnerException.InnerException;

                if (ex.Errors.Count > 0 && ex.Errors[0].Number == 547)
                {
                    var removingRole = _dbContext.role.Find(id);
                    _dbContext.Entry(removingRole).State = System.Data.Entity.EntityState.Unchanged;

                    StringBuilder finalMessage = new StringBuilder("ไม่สามารถลบบทบาทได้ เนื่องจากบทบาทถูกใช้โดยผู้ใช้ ดังต่อไปนี้: ");
                    List<user> effectedUser = removingRole.user.ToList();
                    for (int i = 0; i < effectedUser.Count; i++)
                    {
                        if (i == effectedUser.Count - 1)
                        {
                            finalMessage.Append(effectedUser[i].username);
                        }
                        else
                        {
                            finalMessage.Append(effectedUser[i].username + " ,");
                        }
                    }
                    ModelState.AddModelError("", finalMessage.ToString());
                    return View(removingRole);
                }
                else
                {
                    ModelState.AddModelError("", ex.Message);
                    var removingRole = _dbContext.role.Find(id);
                    return View(removingRole);
                }

            }
            catch (Exception ex)
            {
                var removingRole = _dbContext.role.Find(id);
                ModelState.AddModelError("", ex.Message);
                return View(removingRole);
            }
        }
    }
}
