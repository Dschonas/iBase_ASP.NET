using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iBase_ASP_DOT_NET.Models;
using System.Web.Security;

namespace iBase_ASP_DOT_NET.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login (Loginmodel model, string returnUrl)
        {
            UserTable utlogin;

            if (ModelState.IsValid) {
                using (var db = new iBaseEntities()) {
                    var erg = from ut in db.UserTable
                              where ut.Username == model.UserName && ut.Password == model.Password
                              select ut;
                    utlogin = erg.FirstOrDefault();

                }

                if (utlogin != null) {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToLocal(returnUrl);  // Url  ?? "~/Home/Index"
                }
                else {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }

        public ActionResult Register (Loginmodel model, string returnUrl)
        {
            UserTable utlogin = null;
            var db = new iBaseEntities();
            if (ModelState.IsValid) {
                var erg = from ut in db.UserTable
                          where ut.Username == model.UserName && ut.Password == model.Password
                          select ut;
                utlogin = erg.FirstOrDefault();

                if (utlogin != null) {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToLocal(returnUrl);  // Url  ?? "~/Home/Index"
                }
                else {
                    utlogin = new UserTable();
                    utlogin.Username = model.UserName;
                    utlogin.Password = model.Password;

                    db.UserTable.Add(utlogin);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff ()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal (string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}