using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiHuStore.Models.Product
{
    public class ProductDTO
    {
        public ProductDTO()
        {

        }

        
        public ProductDTO(string id, string nameProduct, float price, string description,string image)
        {
            this.id = id;
            this.nameProduct = nameProduct;
            this.price = price;
            this.description = description;
            this.image = image;
        }
        public ProductDTO(string id,string nameProduct, float price, string description, string image, int stock)
        {
            this.id = id;
            this.stock = stock;
            this.nameProduct = nameProduct;
            this.price = price;
            this.description = description;
            this.image = image;
        }



        private DateTime dateOfCreate;
                
        public DateTime DateOfCreate
        {
            get { return dateOfCreate; }
            set { dateOfCreate = value; }
        }

        private bool isDelete;

        public bool IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        private int productCategoryID;

        public int PropductCategoryID
        {
            get { return productCategoryID; }
            set { productCategoryID = value; }
        }



        private int stock;

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }



        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string nameProduct;

        public string NameProduct
        {
            get { return nameProduct; }
            set { nameProduct = value; }
        }

        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        



    }
}