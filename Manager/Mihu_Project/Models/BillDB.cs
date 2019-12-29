using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mihu_Project.Models
{
    public class Bill
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public string StaffID { get; set; }
        public int State { get; set; }
        public float DiscountValue { get; set; }


    }

    public class BillDB
    {
        private readonly string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public DataTable GetBillOnState(int state)
        {
            DataTable dtBill = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "Select orderID, customerID, createdDate, totalPrice, completeDate From bill Where state = @state";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@state", state);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtBill);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return dtBill;
        }

        public bool ChangeBillState(int state , int billID, string staffID)
        {
            bool check = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "Update bill Set state = @state, staffID = @staff Where orderID = @ID";
                    if (state == 2)
                    {
                        sql = "Update bill Set state = @state, staffID = @staff, completeDate = @date Where orderID = @ID";
                    }
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", billID);
                    cmd.Parameters.AddWithValue("@staff", staffID);
                    cmd.Parameters.AddWithValue("@state", state);
                    if (state == 2)
                    {
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    }
                    check = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return check;
        }

        public Bill GetBillDetail(int id)
        {
            Bill newBill = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "Select orderID, customerID, staffID, state, createdDate, totalPrice, discountValue From bill Where orderID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int billID = dr.GetInt32(0);
                            string customerID = dr.GetString(1);
                            string staffID;
                            try
                            {
                                staffID = dr.GetString(2);
                            }
                            catch (Exception)
                            {
                                staffID = "Waiting";
                            }
                            int state = dr.GetInt32(3);
                            DateTime createdDate = dr.GetDateTime(4);
                            float totalPrice = (float)dr.GetDouble(5);
                            float discountValue = (float)dr.GetDouble(6);
                            newBill = new Bill()
                            {
                                OrderID = billID,
                                CreatedDate = createdDate,
                                CustomerID = customerID,
                                DiscountValue = discountValue,
                                StaffID = staffID,
                                State = state,
                                TotalPrice = totalPrice
                            };

                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


            return newBill;
        }


    }
}