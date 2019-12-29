using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mihu_Project.Models;

namespace Mihu_Project.Controllers
{
    public class AdminController : Controller
    {
        ProductDB ProductTool = new ProductDB();
        StaffDB StaffTool = new StaffDB();
        #region Product
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Show_Product()
        {

            DataTable dtProduct = ProductTool.GetAllProduct();
            TempData["SHOWPAGE"] = "SHOWPAGE";
            return View(dtProduct);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Show_Deleted_Product()
        {

            DataTable dtProduct = ProductTool.GetAllDeletedProduct();
            TempData["SHOWPAGE"] = "DELETEDPAGE";
            return View("Show_Product", dtProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Insert_Product()
        {
            DataTable dtCate = new CateDB().GetAllCate();
            ViewBag.CateList = dtCate;
            if (TempData["ProductModel"] != null)
            {
                return View(TempData["ProductModel"]);
            }
            else
                return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Insert_Product(Product p)
        {
            if (ModelState.IsValid)
            {

                if (!ProductTool.InsertProduct(p))
                {
                    TempData["ERROR"] = "Insert Fail, Error at Model";
                    TempData["ProductModel"] = p;
                    return RedirectToAction("Insert_Product");
                }

                @TempData["InsertSuccess"] = "Insert Success " + p.ProductName;
                return RedirectToAction("Insert_Product");
            }
            else
            {
                DataTable dtCate = new CateDB().GetAllCate();
                ViewBag.CateList = dtCate;
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete_Product()
        {
            try
            {
                string productID = Request["PRODUCTID"];
                if (!ProductTool.DeleteProduct(productID))
                {
                    TempData["ERROR"] = "Delete Fail, Error at Model";
                }
                else
                {
                    TempData["DELETESUCCESS"] = "Delete Success!";
                }
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = "Delete Fail, Error at Delete_Product Action";
                Console.WriteLine(ex.ToString());
            }

            return RedirectToAction("Show_Product");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ReAdd_Product()
        {
            try
            {
                string productID = Request["PRODUCTID"];
                if (!ProductTool.ReAddProduct(productID))
                {
                    TempData["ERROR"] = "ReAdd Fail, Error at Model";
                }
                else
                {
                    TempData["DELETESUCCESS"] = "ReAdd Success!";
                }
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = "ReAdd Fail, Error at ReAdd_Product Action";
                Console.WriteLine(ex.ToString());
            }

            return RedirectToAction("Show_Deleted_Product");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Detail_Product()
        {
            string ProductID = Request["PRODUCTID"];
            string prePage = Request["prepage"];
            if (ProductID != null)
            {
                DataTable dtCate = new CateDB().GetAllCate();
                ViewBag.CateList = dtCate;
                Product p = ProductTool.FindProduct(ProductID);
                if (p != null)
                {
                    if (prePage.Equals("del"))
                    {
                        TempData["ACTION"] = "Show_Deleted_Product";
                    }
                    else
                    {
                        TempData["ACTION"] = "Show_Product";
                    }
                    return View(p);
                }
                else
                {
                    return RedirectToAction("Show_Product");
                }


            }
            else return RedirectToAction("Show_Product");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Detail_Product(Product p)
        {
            string link = "../Admin/Detail_Product?PRODUCTID=" + Request["PRODUCTID"] + "&prepage=" + Request["prepage"];
            try
            {

                if (!ProductTool.UpdateProduct(p))
                {
                    TempData["ERROR"] = "Update Fail, Error at Model";
                    //return RedirectToAction("Detail_Product", new Product { ProductID = p.ProductID });
                    return Redirect(link);
                }
                else
                {
                    TempData["UpdateSuccess"] = "Update Success!";
                }
            }

            catch (Exception ex)
            {
                TempData["ERROR"] = "Update Fail, Error at Detail_Product Action";
                Console.WriteLine(ex.ToString());
                //return RedirectToAction("Detail_Product", new Product { ProductID = p.ProductID });
                return Redirect(link);
            }

            //return RedirectToAction("Detail_Product", new Product { ProductID = p.ProductID });
            return Redirect(link);
        }
        #endregion

        [Authorize(Roles = "Admin")]
        public ActionResult Show_Staff()
        {

            DataTable dtProduct = StaffTool.GetAllStaff();
            return View(dtProduct);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete_Staff()
        {
            //try
            //{
            string staffUsername = Request["USERNAME"];
            if (!StaffTool.DeleteStaff(staffUsername))
            {
                TempData["ERROR"] = "Delete Fail, Error at Model";
            }
            else
            {
                TempData["DELETESUCCESS"] = "Delete Success!";
            }
            //}
            //catch (Exception ex)
            //{
            //    TempData["ERROR"] = "Delete Fail, Error at Delete_Staff Action";
            //    Console.WriteLine(ex.ToString());
            //}

            return RedirectToAction("Show_Staff");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Insert_Staff()
        {
            DataTable dtCate = new CateDB().GetAllCate();
            ViewBag.CateList = dtCate;
            if (TempData["StaffModel"] != null)
            {
                return View(TempData["StaffModel"]);
            }
            else
                return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Insert_Staff(Staff s)
        {
            if (ModelState.IsValid)
            {
                TempData["StaffModel"] = s;
                try
                {
                    if (!StaffTool.InsertStaff(s))
                    {
                        TempData["ERROR"] = "Insert Fail, Error at Model";

                        return RedirectToAction("Insert_Staff");
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("duplicate"))
                    {
                        TempData["DUPLICATE"] = "Existed Username, please try again";

                    }
                    else
                    {
                        TempData["ERROR"] = "Insert Fail, SQL ERROR";
                    }
                    return RedirectToAction("Insert_Staff");
                }
                catch (Exception)
                {
                    TempData["ERROR"] = "Insert Fail, Unexpected ERROR";
                    return RedirectToAction("Insert_Staff");
                }


                return RedirectToAction("Show_Staff");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Detail_Staff()
        {
            string UserName = Request["USERNAME"];
            if (UserName != null)
            {

                Staff p = StaffTool.FindStaff(UserName);
                if (p != null)
                {
                    return View(p);
                }
                else
                {
                    return RedirectToAction("Show_Staff");
                }


            }
            else return RedirectToAction("Show_Staff");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Detail_Staff(Staff p)
        {
            string link = "../Admin/Detail_Staff?USERNAME=" + Request["USERNAME"];
            try
            {
                if (!StaffTool.UpdateStaff(p))
                {
                    TempData["ERROR"] = "Update Fail, Error at Model";
                    //return RedirectToAction("Detail_Product", new Product { ProductID = p.ProductID });
                    return Redirect(link);
                }
                else
                {
                    TempData["UpdateSuccess"] = "Update Success!";
                }
            }

            catch (Exception ex)
            {
                TempData["ERROR"] = "Update Fail, Error at Detail_Product Action";
                Console.WriteLine(ex.ToString());
                //return RedirectToAction("Detail_Product", new Product { ProductID = p.ProductID });
                return Redirect(link);
            }

            //return RedirectToAction("Detail_Product", new Product { ProductID = p.ProductID });
            return Redirect(link);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_BillWaiting()
        {
            BillDB tool = new BillDB();
            DataTable dtBill = tool.GetBillOnState(0);
            return View(dtBill);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Admin_BillInProcess()
        {
            BillDB tool = new BillDB();
            DataTable dtBill = tool.GetBillOnState(1);
            return View(dtBill);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_BillSuccess()
        {
            BillDB tool = new BillDB();
            DataTable dtBill = tool.GetBillOnState(2);
            return View(dtBill);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_BillDetail(int BillID)
        {
            BillDB billDB = new BillDB();
            Bill bill = billDB.GetBillDetail(BillID);
            return View(bill);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_Customer()
        {
            CustomerDB tool = new CustomerDB();
            DataTable dtCus = tool.GetAllCus();
            return View(dtCus);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_Category()
        {
            CateDB tool = new CateDB();
            DataTable dtCate = tool.GetAllCate();
            return View(dtCate);
        }
    }
}