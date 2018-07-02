using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LotteryResult.Models;
using System.Security.Cryptography;
using System.Text;
using LotteryResult.Models.ViewModels;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace LotteryResult.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET: User
        [HttpGet]
        [Authorize(Roles = "ผู้ดูแลระบบ")]
        public ActionResult Index()
        {
            return View(_dbContext.user.ToArray());
        }

        // GET: User/Details/5
        [HttpGet]
        [Authorize(Roles = "ผู้ดูแลระบบ")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }

            user u = _dbContext.user.Find(id);

            if (u == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(u);
        }

        // GET: User/Create
        [HttpGet]
        [Authorize(Roles = "ผู้ดูแลระบบ")]
        public ActionResult Create()
        {
            ViewBag.role_id = generateRoleListItem();

            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ผู้ดูแลระบบ")]
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
        [HttpGet]
        [Authorize(Roles = "ผู้ดูแลระบบ")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }

            user u = _dbContext.user.Find(id);

            if (u == null)
            {
                throw new HttpException(404, "Not found");
            }
            else
            {
                // Do not show hashed password.
                u.hashed_password = "";

                ViewBag.role_id = generateRoleListItem(u.role_id);
                return View(u);
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ผู้ดูแลระบบ")]
        public ActionResult Edit(int id, user u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user changingUser = _dbContext.user.Find(id);

                    changingUser.username = u.username;
                    
                    changingUser.old_password = changingUser.hashed_password;
                    changingUser.hashed_password = Utility.computeMd5Hash(MD5.Create(), u.hashed_password);

                    changingUser.firstname = u.firstname;
                    changingUser.lastname = u.lastname;
                    changingUser.role_id = u.role_id;
                    changingUser.is_active = u.is_active;

                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.role_id = generateRoleListItem(u.role_id);
                    return View(u);
                }
            }
            else
            {
                ModelState.AddModelError("", "กรุณากรอกข้อมูลให้ถูกต้อง");
                ViewBag.role_id = generateRoleListItem(u.role_id);
                return View(u);
            }
        }

        // GET: User/Edit
        [HttpGet]
        public ActionResult UserEdit()
        {

            user u = _dbContext.user.Find(((user)Session["user"]).id);

            if (u == null)
            {
                return RedirectToAction("Redirector", "Account");
            }
            else
            {
                return View(new userEdit {
                    id = u.id,
                    username = u.username
                });
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(int id, userEdit u)
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
                    }
                    else
                    {
                        ModelState.AddModelError("", "กรุณากรอกข้อมูลให้ถูกต้อง");
                        return View(u);
                    }

                    _dbContext.SaveChanges();
                    return RedirectToAction("Redirector", "Account");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(u);
                }
            }
            else
            {
                ModelState.AddModelError("", "กรุณากรอกข้อมูลให้ถูกต้อง");
                return View(u);
            }
        }

        // GET: User/Delete/5
        [HttpGet]
        [Authorize(Roles = "ผู้ดูแลระบบ")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Not found");
            }

            user u = _dbContext.user.Find(id);

            if (u == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(u);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ผู้ดูแลระบบ")]
        public ActionResult Delete(int id, user u)
        {
            try
            {
                user removingUser = _dbContext.user.Find(id);
                _dbContext.user.Remove(removingUser);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException exception)
            {
                SqlException ex = (SqlException)exception.InnerException.InnerException;

                if (ex.Errors.Count > 0 && ex.Errors[0].Number == 547)
                {
                    var user = _dbContext.user.Find(id);
                    _dbContext.Entry(user).State = System.Data.Entity.EntityState.Unchanged;
                    ModelState.AddModelError("", "ไม่สามารถลบผู้ใช้ได้ เนื่องจากผู้ใช้ได้สร้างข้อมูลในระบบเรียบร้อยแล้ว ให้ปิดใช้งานผู้ใช้แทน");
                    return View(user);
                }
                else
                {
                    var user = _dbContext.user.Find(id);
                    ModelState.AddModelError("", ex.Message);
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                var user = _dbContext.user.Find(id);
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }

        private List<SelectListItem> generateRoleListItem(int? selectedId = 1)
        {
            List<SelectListItem> roleListItem = new List<SelectListItem>();
            List<role>.Enumerator roleIterator = _dbContext.role.ToList().GetEnumerator();
            while (roleIterator.MoveNext())
            {
                role r = roleIterator.Current;
                if (selectedId != null && r.id == selectedId)
                {
                    roleListItem.Add(new SelectListItem() { Text = r.name, Value = r.id.ToString(), Selected = true });
                }
                else
                {
                    roleListItem.Add(new SelectListItem() { Text = r.name, Value = r.id.ToString() });
                }
            }

            return roleListItem;
        }
    }
}
