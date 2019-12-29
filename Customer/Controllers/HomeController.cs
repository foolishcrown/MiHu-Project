using MiHuStore.Models.Product;
using MiHuStore.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiHuStore.Models.Customer;
using MiHuStore.Models.Order;
using System.Text.RegularExpressions;
using MiHuStore.Models.Account;
using System.Web.Security;

namespace MuHiStore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {



            return View("ViewLogin");


        }
        public ViewResult Register()
        {
            return View("ViewCreateCustomer");
        }

        public ActionResult Login()
        {
            string username = Request["txtUsername"];
            string pass = Request["txtPass"];
            AccountDAO dao = new AccountDAO();
            CustomerDAO daoCus = new CustomerDAO();
            string cusUsername = dao.Login(username, pass);
            if (cusUsername != null)
            {
                FormsAuthentication.SetAuthCookie(cusUsername, false);//
                CustomerDTO cus = daoCus.GetCusInfo(cusUsername);
                cus.Username = cusUsername;
                Session["CUSTOMER"] = cus;
                return RedirectToAction("SearchRandom");
            }
            else
            {
                ViewBag.error = "Username or password maybe incorrect.";
            }
            return View("ViewLogin");

        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public ViewResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return View("ViewLogin");
        }

        [Authorize(Roles = "Customer")]
        public ActionResult SearchRandom()
        {
            if (Session["CUSTOMER"] != null)
            {
                int cate = 0;
                int cate2 = -1;
                if (Request["Cate"] != null)
                {

                    cate = int.Parse(Request["Cate"]);

                }
                ProductDAO dao = new ProductDAO();
                List<ProductDTO> listProduct = dao.SearchRandom(cate);
                if (cate == 0)
                {
                    TempData["ISHOMEPAGE"] = "Best Seller";
                }
                else
                {
                    TempData["ISHOMEPAGE"] = "Products";
                }
                if (Request["Cate2"] != null)
                {
                    cate2 = int.Parse(Request["Cate2"]);
                    List<ProductDTO> tmp = dao.SearchRandom(cate2);
                    if (tmp != null)
                    {
                        for (int i = 0; i < tmp.Count; i++)
                        {
                            listProduct.Add(tmp[i]);

                        }
                    }
                }

                return View("Shopping", listProduct);
            }
            return View();

        }

        [Authorize(Roles = "Customer")]
        public ActionResult ViewCart()
        {
            // get quantity in database of product in cart
            CartItem c = (CartItem)Session["CART"];

            if (c != null)
            {

                List<int> listQuantity = new List<int>();
                ProductDAO dao = new ProductDAO();
                for (int i = 0; i < c.Cart.Count; i++)
                {
                    listQuantity.Add(dao.GetProductQuantity(c.Cart[i].Product.ID));
                }

                Session["listQuantity"] = listQuantity;

            }
            // check discount
            if (Session["CUSTOMER"] != null)
            {
                CustomerDTO dto = (CustomerDTO)Session["CUSTOMER"];
                CustomerDAO dao = new CustomerDAO();
                string rank = dao.GetRank(dto.Username);
                if (!rank.Equals(""))
                {
                    float discount = dao.GetDiscount(rank);
                    Session["Discount"] = discount;
                }

            }



            return View("ViewCart");

        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult Buy()
        {


            string id = Request["txtID"];
            string name = Request["txtName"];
            float price = float.Parse(Request["txtPrice"]);
            string description = Request["txtDescription"];
            int quantity = int.Parse(Request["txtQuantity"]);
            string image = Request["txtImage"];
            ProductDTO p = new ProductDTO(id, name, price, description, image);


            CartItem c = (CartItem)Session["CART"];
            if (c == null)
            {
                c = new CartItem();
                c.AddToCart(p, quantity);
                Session.Add("CART", c);
            }
            else
            {
                c.AddToCart(p, quantity);


            }
            //
            if (TempData["PreviousPage"] != null)
            {
                if ((int)TempData["PreviousPage"] == 1)
                {
                    return RedirectToAction("SearchRandom");
                }
                else if ((int)TempData["PreviousPage"] == 2)
                {
                    return RedirectToAction("ViewCart");
                }
            }

            return RedirectToAction("SearchProduct");


        }

        [Authorize(Roles = "Customer")]
        public ActionResult ShowCusInfo()
        {
            if (Session["LISTBILL"] != null) // refresh List bill if customer buy some thing
            {
                Session.Remove("LISTBILL");
            }

            BillDAO dao = new BillDAO();
            CustomerDTO cus = (CustomerDTO)Session["CUSTOMER"];
            string cusId = cus.Username;
            List<BillDTO> listBill = dao.GetAllBillOfCus(cusId);
            CustomerDAO daoCus = new CustomerDAO();
            string rank = daoCus.GetRank(cusId);
            if (!rank.Equals(""))
            {
                cus.Rank = rank;
            }

            Session["LISTBILL"] = listBill;


            return View("ViewCusInfo");
        }





        [Authorize(Roles = "Customer")]
        [HttpGet]
        public ActionResult SearchProduct()
        {



            string searchValue = (string)Session["txtSearchValue"]; // kiem tra da search trước đó chưa
            if (searchValue == null) // nếu chưa thì lấy giá trị search từ field search
            {
                searchValue = Request["txtSearchValue"];

                Session.Add("txtSearchValue", searchValue);

            }
            else // nếu có rồi có nghĩa  đang ở trang khác gọi lại trang search với giá trị search cũ sau khi search xog hoặc gọi lại trang search để search
            {
                searchValue = Request["txtSearchValue"]; // kiểm tra xem có phải gọi lại trang search không lấy giá trị search mới của field
                if (searchValue != null) // nếu search value khác null nghĩ là dag ở trang search và search lại
                {
                    Session.Remove("txtSearchValue"); // xóa giá trị cũ 
                    Session.Add("txtSearchValue", searchValue);// thêm giá trị mới (có thể giá trị cũ và mới giống nhau)
                }
            }

            ProductDAO dao = new ProductDAO();
            List<ProductDTO> result = dao.SearchProduct((string)Session["txtSearchValue"]);


            return View("SearchView", result);


        }

        [Authorize(Roles = "Customer")]
        public ActionResult ViewProduct()
        {
            string id = Request["ProductID"];
            ProductDAO dao = new ProductDAO();
            ProductDTO dto = dao.FindProduct(id);
            ViewBag.ProductDetail = dto;
            // 1 is home page, 2 is viewcart page, 3 is view search
            if (Request["PreviousPage"] != null)
            {
                TempData["PreviousPage"] = int.Parse(Request["PreviousPage"]);
            }


            return View("ViewProduct");
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public ActionResult RemoveItem()
        {
            CartItem c = (CartItem)Session["CART"];
            c.RemoveItem(Request["txtID"]);
            if (c != null)
            {
                if (c.Cart == null)
                {
                    Session.Remove("CART");
                }
            }
            return RedirectToAction("ViewCart");
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult UpdateCustomer()
        {
            CustomerDTO cus = (CustomerDTO)Session["CUSTOMER"];
            string username = cus.Username;
            string fullname = Request["txtFullname"];
            string tel = Request["txtPhone"];
            string address = Request["txtAddress"];
            string email = Request["txtEmail"];
            CustomerDAO dao = new CustomerDAO();
            CustomerDTO tmp = new CustomerDTO(username, fullname, email, address, tel);
            bool result = dao.UpdateCustomer(tmp);
            if (result)
            {
                cus.CustomerName = fullname;
                cus.Email = email;
                cus.Address = address;
                cus.Phone = tel;
                TempData["UpdateSuccess"] = "Update infomation successful.";
                return RedirectToAction("ShowCusInfo");
            }

            return View();



        }


        [Authorize(Roles = "Customer")]
        [HttpGet]
        public ActionResult UpdateQuantity()
        {
            int quantity = int.Parse(Request["quantity"]);
            string idProduct = Request["txtID"];
            CartItem c = (CartItem)Session["CART"];

            ProductDAO dao = new ProductDAO();
            c.UpdateQuantityProduct(idProduct, quantity);

            return RedirectToAction("ViewCart");

        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult CheckOut()
        {



            CartItem c = (CartItem)Session["CART"];
            bool errorQuantity = false;
            ProductDAO dao = new ProductDAO();

            string[,] listErrorQuantity = new string[c.Cart.Count, 1]; // cap phat vung nho phai chi ro song col and row





            // check quantity at client side with quantity at database
            for (int i = 0; i < c.Cart.Count; i++)
            {

                if (Request["txtQuantityAt_" + i] != null)
                {
                    if (Regex.IsMatch(Request["txtQuantityAt_" + i], "^\\d+$") == false)
                    {

                        listErrorQuantity.SetValue("Positive integer", i, 0);
                        errorQuantity = true;
                    }
                    else
                    {
                        int quantityAtClient = int.Parse(Request["txtQuantityAt_" + i]);
                        int quantityInDatabase = dao.GetProductQuantity(c.Cart[i].Product.ID);
                        if (quantityAtClient > quantityInDatabase)
                        {
                            listErrorQuantity.SetValue("Max : " + quantityInDatabase, i, 0);
                            errorQuantity = true;
                        }
                        else
                        {
                            c.Cart[i].Quantity = int.Parse(Request["txtQuantityAt_" + i]);
                        }
                    }



                }


            }

            if (errorQuantity)
            {
                ViewBag.ErrorQuantity = listErrorQuantity;
                return View("ViewCart");
            }




            return View("ViewConfirm");

        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult Confirm()
        {
            bool errorQuantity = false;
            ProductDAO dao = new ProductDAO();
            CartItem c = (CartItem)Session["CART"];
            string[,] listErrorQuantity = new string[c.Cart.Count, 1];
            for (int i = 0; i < c.Cart.Count; i++)
            {
                int quantityAtCart = c.Cart[i].Quantity;
                int quantityInDatabase = dao.GetProductQuantity(c.Cart[i].Product.ID);
                if (quantityAtCart > quantityInDatabase)
                {
                    listErrorQuantity.SetValue("Max : " + quantityInDatabase, i, 0);
                    errorQuantity = true;
                }
            }
            if (errorQuantity == false)
            {
                for (int i = 0; i < c.Cart.Count; i++)
                {
                    dao.UpdateQuantitty(c.Cart[i].Product.ID, c.Cart[i].Quantity);
                }


                float totalPrice = float.Parse(Request["TotalPrice"]);
                CustomerDAO daoCus = new CustomerDAO();
                CustomerDTO cus = (CustomerDTO)Session["CUSTOMER"];
                bool result = daoCus.CheckOut(totalPrice, cus.Username);

                //check point and maybe update rank

                string newRank = "";

                float point = daoCus.GetPointOfCus(cus.Username);
                string currentRank = cus.Rank;
                if (currentRank == null)
                {
                    currentRank = "";
                }

                if (point >= 500 && point < 2000 && currentRank.Equals("bronze") == false)
                {
                    newRank = "bronze";
                    daoCus.UpdateRank(cus.Username, newRank);
                    ViewBag.Congrats = "Congratulations!!! Your rank is Bronze now. Thank you for choosing our service.";

                }
                if (point >= 2000 && point < 5000 && currentRank.Equals("silver") == false)
                {
                    newRank = "silver";
                    daoCus.UpdateRank(cus.Username, newRank);
                    ViewBag.Congrats = "Congratulations!!! Your rank is Silver now. Thank you for choosing our service.";

                }
                if (point >= 5000 && point < 15000 && currentRank.Equals("gold") == false)
                {
                    newRank = "gold";
                    daoCus.UpdateRank(cus.Username, newRank);
                    ViewBag.Congrats = "Congratulations!!! Your rank is Gold now. Thank you for choosing our service.";

                }
                if (point >= 15000 && currentRank.Equals("platinum") == false)
                {
                    newRank = "platinum";
                    daoCus.UpdateRank(cus.Username, newRank);
                    ViewBag.Congrats = "Congratulations!!! Your rank is Platinum now. Thank you for choosing our service.";
                }

                //when checkout done
                if (result)
                {
                    // create order
                    DateTime createDate = DateTime.Now;
                    string cusID = cus.Username;
                    int state = 0;// start order

                    float discount = 0;
                    if (Session["Discount"] != null) // neu moi mua lan dau thi ko co discount
                    {
                        discount = (float)Session["Discount"];
                    }

                    BillDTO order = new BillDTO(createDate, cusID, state, totalPrice, discount);
                    BillDAO daoOrder = new BillDAO();
                    bool resultOrder = daoOrder.CreateOrder(order);// create order
                    if (resultOrder)
                    {

                        OrderDetailDAO daoOrderDetail = new OrderDetailDAO();
                        int orderID = daoOrder.GetOrderID(); // get orderID has created
                        for (int i = 0; i < c.Cart.Count; i++)
                        {
                            OrderDetailDTO orderDetail = new OrderDetailDTO(orderID, c.Cart[i].Product.ID,
                                c.Cart[i].Quantity, c.Cart[i].Product.Price, c.Cart[i].Product.NameProduct);
                            daoOrderDetail.CreateOrderDetail(orderDetail);

                        }

                        Session.Remove("CART");

                        Session.Remove("txtSearchValue");
                    }

                }
            }
            else
            {
                ViewBag.ErrorQuantity = listErrorQuantity;
                return View("ViewCart");
            }



            return View("ViewOrdercomplete");
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public ActionResult ShowDetailBill()
        {
            if (Regex.IsMatch(Request["OrderID"], "^\\d+$"))
            {

                int id = int.Parse(Request["OrderID"]);
                id -= 491999;
                OrderDetailDAO dao = new OrderDetailDAO();
                List<OrderDetailDTO> listOrderDetail = dao.GetBillDetail(id);
                BillDAO daoBill = new BillDAO();
                float discount = daoBill.GetDiscount(id);
                string state = daoBill.GetState(id);
                ViewBag.Discount = discount;
                ViewBag.State = state;

                ViewBag.ListOrderDetail = listOrderDetail;
            }
            return View("ViewDetailBill");
        }



        [HttpPost]
        public ActionResult CreateCustomer()
        {
            AccountDAO accDao = new AccountDAO();
            CustomerDAO cusDao = new CustomerDAO();
            string username = Request["txtUsername"];
            if (accDao.CheckDuplicate(username))
            {

                ViewBag.Error = "Username is exsited.";
                return View("ViewCreateCustomer");

            }
            else
            {
                string fullname = Request["txtFullname"];
                string tel = Request["txtPhone"];
                string address = Request["txtAddress"];
                string email = Request["txtEmail"];
                string password = Request["txtPassword"];
                CustomerDTO newCus = new CustomerDTO(username, fullname, email, address, 0, tel);

                AccountDTO newAcc = new AccountDTO(username, password, 2);
                if (accDao.CreateAccount(newAcc))
                {
                    if (cusDao.CreateCustomer(newCus))
                    {
                        ViewBag.CreatSuccess = "1"; //Register Successfull.
                        return View("ViewCreateCustomer");
                    }
                }



            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }



    }
}