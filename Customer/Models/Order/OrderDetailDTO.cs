using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiHuStore.Models.Order
{
    public class OrderDetailDTO
    {
        public OrderDetailDTO()
        {

        }

        public OrderDetailDTO(int orderID, string productID, int quantity, float unitPrice,string productName)
        {
            OrderID = orderID;
            ProductID = productID;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ProductName = productName;
        }

        public int OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }

        public string ProductName { get; set; }


    }
}