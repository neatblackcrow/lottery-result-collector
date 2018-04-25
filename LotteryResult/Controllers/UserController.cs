using LotteryResult.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LotteryResult.Controllers
{
    public class UserController : Controller
    {
        private LottoResultContext _db = new LottoResultContext();

        // GET: User
        public ActionResult Index()
        {
            return View(_db.user.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            user selectedUser = _db.user.Find(id);

            if (selectedUser == null)
            {
                return RedirectToAction("Index");
            }

            return View(selectedUser);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            List<SelectListItem> role_id = new List<SelectListItem>();
            List<role>.Enumerator roleIterator = _db.role.ToList().GetEnumerator();

            while (roleIterator.MoveNext()) {
                role r = roleIterator.Current;
                role_id.Add(new SelectListItem() { Text = r.name, Value = r.id.ToString() });
            }

            ViewBag.role_id = role_id;
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(user user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.hashed_password = computeMd5Hash(MD5.Create(), user.hashed_password);
                    _db.user.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Please enter data with correct formating.");
                return View(user);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            List<SelectListItem> role_id = new List<SelectListItem>();
            List<role>.Enumerator roleIterator = _db.role.ToList().GetEnumerator();

            while (roleIterator.MoveNext())
            {
                role r = roleIterator.Current;
                role_id.Add(new SelectListItem() { Text = r.name, Value = r.id.ToString() });
            }

            ViewBag.role_id = role_id;
            return View(_db.user.Find(id));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, user user)
        {
            try
            {
                user changedUser = _db.user.Find(id);
                changedUser.username = user.username;
                changedUser.hashed_password = computeMd5Hash(MD5.Create(), user.hashed_password);
                changedUser.role_id = user.role_id;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_db.user.Find(id));
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, user user)
        {
            try
            {
                user removedUser = _db.user.Find(id);
                _db.user.Remove(removedUser);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Custom methods go here...

        private string computeMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private bool verifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = computeMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
