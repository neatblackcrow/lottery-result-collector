using LotteryResult.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LotteryResult.Models.ViewModels;
using System.Security.Cryptography;
using System.Web.Security;
using System.Security.Principal;

namespace LotteryResult.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private LottoResultContext _dbContext = new LottoResultContext();

        // GET
        [AllowAnonymous]
        public ActionResult Redirector()
        {

            if (HttpContext.Request.IsAuthenticated && HttpContext.User is GenericPrincipal)
            {
                GenericPrincipal principal = (GenericPrincipal) HttpContext.User;
                FormsIdentity identity = (FormsIdentity) principal.Identity;

                if (identity.IsAuthenticated)
                {
                    // Only get a first role.
                    switch (principal.Claims.Where(c => c.Type == identity.RoleClaimType).First().Value)
                    {
                        case "กองออกรางวัล":
                            return RedirectToAction("Index", "Round");
                        case "ประชาสัมพันธ์":
                            return RedirectToAction("Index", "AdMessage");
                        case "ผู้บันทึกผลรางวัล":
                            return RedirectToAction("Index", "Result");
                        case "ผู้ตรวจสอบการบันทึกผลรางวัล":
                            return RedirectToAction("Index", "Result");
                    }
                    
                }
            }

            return RedirectToAction("LogIn");
        }

        // GET
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LogIn(userLogin user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = Utility.computeMd5Hash(MD5.Create(), user.hashed_password);

                try
                {
                    var foundUser = _dbContext.user.Where(u => u.username == user.username &&
                                                          u.hashed_password == hashedPassword &&
                                                          u.is_active == true).Single();

                    /*
                     * 
                     * Cookie only stores security information, username and role(s) and Session Id for 20 minutes
                     * 
                     * 
                    */
                    FormsAuthentication.SetAuthCookie(foundUser.username, false);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, foundUser.username, DateTime.Now, DateTime.Now.AddMinutes(20), false, foundUser.role.name);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    // Anything else...
                    HttpContext.Session.Add("user", foundUser);

                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Redirector");
                    }

                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", "รหัสผู้ใช้หรือรหัสผ่านผิด");
                    return View(user);
                }

            }
            else
            {
                ModelState.AddModelError("", "กรุณากรอกข้อมูลให้ถูกต้อง");
                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            // AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account");
        }

    }
}