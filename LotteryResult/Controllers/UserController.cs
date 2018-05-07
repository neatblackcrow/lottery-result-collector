using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LotteryResult.Models;
using System.Security.Cryptography;
using System.Text;
using LotteryResult.Models.ViewModels;

namespace LotteryResult.Controllers
{
    public class UserController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: User
        public ActionResult Index()
        {
            return View(_dbContext.user.ToArray());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            user u = _dbContext.user.Find(id);

            if (u == null)
            {
                return RedirectToAction("Index");
            }
            return View(u);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.role_id = generateRoleListItem();

            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(user u)
        {
            u.create_timestamp = DateTime.Now;
            u.old_password = null;

            if (ModelState.IsValid)
            {
                try
                {
                    u.hashed_password = Utility.computeMd5Hash(MD5.Create(), u.hashed_password);

                    _dbContext.user.Add(u);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.role_id = generateRoleListItem();
                    return View(u);
                }
            }
            else
            {
                ModelState.AddModelError("", "กรุณากรอกข้อมูลให้ถูกต้อง");
                ViewBag.role_id = generateRoleListItem();
                return View(u);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {

            user u = _dbContext.user.Find(id);

            if (u == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.role_id = generateRoleListItem();

                return View(u);
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, user u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user changingUser = _dbContext.user.Find(id);
                    string oldPasswordHash = Utility.computeMd5Hash(MD5.Create(), u.old_password);

                    if (changingUser.hashed_password == oldPasswordHash)
                    {
                        changingUser.username = u.username;
                        changingUser.hashed_password = Utility.computeMd5Hash(MD5.Create(), u.hashed_password);
                        changingUser.old_password = oldPasswordHash;
                        changingUser.firstname = u.firstname;
                        changingUser.lastname = u.lastname;
                        changingUser.role_id = u.role_id;
                    }
                    else
                    {
                        ModelState.AddModelError("", "กรุณากรอกข้อมูลให้ถูกต้อง");
                        ViewBag.role_id = generateRoleListItem();
                        return View(u);
                    }

                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.role_id = generateRoleListItem();
                    return View(u);
                }
            }
            else
            {
                ModelState.AddModelError("", "กรุณากรอกข้อมูลให้ถูกต้อง");
                ViewBag.role_id = generateRoleListItem();
                return View(u);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            user u = _dbContext.user.Find(id);

            if (u == null)
            {
                return RedirectToAction("Index");
            }
            return View(u);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, user u)
        {
            try
            {
                user removingUser = _dbContext.user.Find(id);
                _dbContext.user.Remove(removingUser);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(u);
            }
        }

        private List<SelectListItem> generateRoleListItem()
        {
            List<SelectListItem> roleListItem = new List<SelectListItem>();
            List<role>.Enumerator roleIterator = _dbContext.role.ToList().GetEnumerator();
            while (roleIterator.MoveNext())
            {
                role r = roleIterator.Current;
                roleListItem.Add(new SelectListItem() { Text = r.name, Value = r.id.ToString() });
            }

            return roleListItem;
        }
    }
}
