using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiHuStore.Models.Order
{
    public class BillDTO
    {
        public BillDTO()
        {

        }

        public BillDTO(DateTime createDate, string customerID, int state, float totalPrice, float discountValue)
        {
            CreateDate = createDate;
            CustomerID = customerID;
            State = state;
            TotalPrice = totalPrice;
            DiscountValue = discountValue;
        }

        public int OrderID { get; set; }
        public DateTime CreateDate { get; set; }
        public string CustomerID { get; set; }
        public string StaffID { get; set; }
        public int State { get; set; }
        public string Description { get; set; }
        public float TotalPrice { get; set; }
        public float DiscountValue { get; set; }


    }
}