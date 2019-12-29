using Mihu_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mihu_Project.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPage(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // do your stuff like: save to database and redirect to required page.
                    string username = model.Username;
                    string password = model.Password;

                    AccountDB accountDB = new AccountDB();
                    int role = accountDB.CheckRole(username, password);
                    if (role == 0)
                    {
                        FormsAuthentication.SetAuthCookie(username, false);
                        return RedirectToAction("Show_Product", "Admin");

                    }
                    else if (role == 1)
                    {
                        FormsAuthentication.SetAuthCookie(username, false);
                        return RedirectToAction("Staff_Product", "Staff");

                    }
                    else if (role == 2)
                    {
                        ModelState.AddModelError("", "Your Role is not Supported");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong username or password");
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return RedirectToAction("Error", "Error");
                }
               

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginPage", "Account");
        }


        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                AccountDB accountDB = new AccountDB();
                int role = accountDB.CheckRole(username, password);
                if (role == 0)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("Show_Product", "Admin");

                }
                else if (role == 1)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("Staff_Product", "Staff");

                }
                else if (role == 2)
                {
                    ModelState.AddModelError("", "Your Role is not Supported");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username or password");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Error", "Error");
            }
            

            return View();

        }
    }
}