using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiHuStore.Models.Customer
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {

        }

        public CustomerDTO(string username, string customerName, string email, string address, string rank, float point)
        {

            this.username = username;
            this.customerName = customerName;
            this.email = email;
            this.address = address;
            this.rank = rank;
            this.point = point;
        }
        public CustomerDTO(string username, string customerName, string email, string address,  float point,string phone)
        {

            this.username = username;
            this.customerName = customerName;
            this.email = email;
            this.address = address;
            this.phone = phone;
            this.point = point;
        }
        public CustomerDTO(string username, string cusName, string email, string address, string phone)
        {
            this.username = username;
            this.customerName = cusName;
            this.email = email;
            this.address = address;
            this.phone = phone;
        }


        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private float point;

        public float Point
        {
            get { return point; }
            set { point = value; }
        }



        private string rank;

        public string Rank
        {
            get { return rank; }
            set { rank = value; }
        }



    }
}
