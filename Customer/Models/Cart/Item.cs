using MiHuStore.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiHuStore.Models.Cart
{
    public class Item
    {
        public Item()
        {
                
        }

        public Item(ProductDTO product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }

        private ProductDTO product;

        public ProductDTO Product
        {
            get { return product; }
            set { product = value; }
        }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }


    }
}