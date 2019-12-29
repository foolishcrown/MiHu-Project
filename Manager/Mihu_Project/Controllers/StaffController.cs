using Mihu_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mihu_Project.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        // GET: Staff
        [Authorize(Roles = "Staff")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Staff_Product()
        {
            DataTable dtProduct;
            try
            {
                ProductDB tool = new ProductDB();
                dtProduct = tool.GetAllProduct();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Error", "Error");

            }
            return View(dtProduct);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Staff_Category()
        {
            DataTable dtCate;
            try
            {
                CateDB tool = new CateDB();
                dtCate = tool.GetAllCate();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Error", "Error");
            }

            return View(dtCate);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Staff_Customer()
        {
            DataTable dtCus;
            try
            {
                CustomerDB tool = new CustomerDB();
                dtCus = tool.GetAllCus();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Error", "Error");
            }

            return View(dtCus);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Staff_BillWaiting()
        {
            DataTable dtBill;
            try
            {
                BillDB tool = new BillDB();
                 dtBill = tool.GetBillOnState(0);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Error", "Error");
            }
            
            return View(dtBill);
        }


        [Authorize(Roles = "Staff")]
        public ActionResult BillWaiting(int BillID)
        {
            BillDB billDB = new BillDB();
            string staffID = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            bool check = billDB.ChangeBillState(1, BillID, staffID);
            if (check)
            {
                TempData["alertMessage"] = "Success";
            }
            else
            {
                TempData["alertMessage"] = "Fail";
            }

            return RedirectToAction("Staff_BillWaiting", "Staff");
        }

        [Authorize(Roles = "Staff")]
        public ActionResult BillInProcess(int BillID)
        {
            BillDB billDB = new BillDB();
            string staffID = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            bool check = billDB.ChangeBillState(2, BillID, staffID);
            if (check)
            {
                TempData["alertMessage"] = "Success";
            }
            else
            {
                TempData["alertMessage"] = "Fail";
            }

            return RedirectToAction("Staff_BillInProcess", "Staff");
        }

        [Authorize(Roles = "Staff")]
        public ActionResult BillCancel(int BillID)
        {
            BillDB billDB = new BillDB();
            string staffID = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            bool check = billDB.ChangeBillState(3, BillID, staffID);
            if (check)
            {
                TempData["alertMessage"] = "Success";
            }
            else
            {
                TempData["alertMessage"] = "Fail";
            }

            return RedirectToAction("Staff_BillInProcess", "Staff");
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Staff_BillInProcess()
        {
            BillDB tool = new BillDB();
            DataTable dtBill = tool.GetBillOnState(1);
            return View(dtBill);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Staff_BillSuccess()
        {
            BillDB tool = new BillDB();
            DataTable dtBill = tool.GetBillOnState(2);
            return View(dtBill);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Staff_BillFail()
        {
            BillDB tool = new BillDB();
            DataTable dtBill = tool.GetBillOnState(3);
            return View(dtBill);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Staff_BillDetail(int BillID)
        {
            BillDB billDB = new BillDB();
            Bill bill = billDB.GetBillDetail(BillID);
            return View(bill);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Staff_ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public ActionResult Staff_ChangePassword(ChangPasswordModel model, string returnURL)
        {
            if (ModelState.IsValid)
            {
                string username = model.Username;
                string password = model.Password;
                string confirm = model.ConfirmPassword;
                if (confirm.Equals(password))
                {
                    AccountDB accountDB = new AccountDB();
                    bool check = accountDB.ChangePass(username, password);
                    if (check)
                    {
                        TempData["alertMessage"] = "Success";
                        return RedirectToAction("Staff_Product", "Staff");
                    }
                    else
                    {
                        TempData["alertMessage"] = "Fail";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Password must match Confirm");
                }

            }

            return View();
        }


    }
}