using MiHuStore.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace MiHuStore.Models.Cart
{
    public class CartItem
    {


        public CartItem()
        {

        }
        private List<Item> cart;

        public List<Item> Cart
        {
            get { return cart; }
            set { cart = value; }
        }

      
        public void AddToCart(ProductDTO p,int quantity)
        {
            bool tmp = false;
            if (cart != null)
            {
                for (int i = 0; i < cart.Count; i++)
                {

                    if (p.ID.Equals(cart[i].Product.ID))
                    {
                        tmp = true;
                        cart[i].Quantity += quantity;
                    }
                }
                if (tmp == false) // san pham add vao la san pham moi
                {
                    Item i = new Item(p, quantity);
                    
                    cart.Add(i);


                }

            }
            else
            {

                cart = new List<Item>();
                
                Item i = new Item(p, quantity);

                cart.Add(i);
            }
        }



        public void RemoveItem(string productID)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (productID.Equals(cart[i].Product.ID))
                {
                    cart.Remove(cart[i]);
                }

            }
            if (cart.Count == 0)
            {
                cart = null;
            }

        }


        public bool UpdateQuantityProduct(string productID, int quantity)
        {
            bool result = true;

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ID.Equals(productID))
                {
                    ProductDAO dao = new ProductDAO();

                    int quantityInDB = dao.GetProductQuantity(productID); // lay quantity cua product trong DB ??? chua lay 
                    if (quantity > quantityInDB)
                    {
                        result = false;
                    }
                    else
                    {
                        cart[i].Quantity = quantity;
                    }
                    break;
                }
            }
            return result;
        }



       



    }
}