using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MiHuStore.Models;

namespace MiHuStore.Models.Order
{
    public class BillDAO
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;

        public BillDAO()
        {

        }

        public bool CreateOrder(BillDTO dto)
        {
            bool result = false;
            string sql = "Insert into bill(customerID, totalPrice, discountValue, createdDate, state) values(@CusID,@TotalPrice,@Discount,@CreateDate,@State)";

            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@CusID", dto.CustomerID);
            cmd.Parameters.AddWithValue("@TotalPrice", dto.TotalPrice);
            cmd.Parameters.AddWithValue("@Discount", dto.DiscountValue);
            cmd.Parameters.AddWithValue("@CreateDate", dto.CreateDate);
            cmd.Parameters.AddWithValue("@State", dto.State);
            try
            {
                if (cnn.State == System.Data.ConnectionState.Closed)
                {
                    cnn.Open();
                    result = cmd.ExecuteNonQuery() > 0;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public int GetOrderID()
        {
            int id = -1;
            string sql = "Select MAX(orderID) from bill";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            try
            {
                if (cnn.State == System.Data.ConnectionState.Closed)
                {
                    cnn.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            id = dr.GetInt32(0);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return id;


        }

        public float GetDiscount(int orderID)
        {
            float result = 0;
            string sql = "Select discountValue from bill where orderID=@OrderID";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@OrderID", orderID);
            try
            {
                if (cnn.State == System.Data.ConnectionState.Closed)
                {
                    cnn.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        result = (float)dr.GetDouble(0);
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
        public string GetState(int orderID)
        {
            string result = "";
            string sql = "Select state from bill where orderID=@OrderID";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@OrderID", orderID);
            try
            {
                if (cnn.State == System.Data.ConnectionState.Closed)
                {
                    cnn.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int temp = dr.GetInt32(0);
                        switch (temp)
                        {
                            case 0:
                                result = "Waiting";
                                break;
                            case 1:
                                result = "In Process";
                                break;
                            case 2:
                                result = "Success";
                                break;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public List<BillDTO> GetAllBillOfCus(string cusID)
        {
            List<BillDTO> ListBill = new List<BillDTO>();
            string sql = "Select orderID, createdDate, totalPrice from bill where customerID=@CusID Order by createdDate ASC";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@CusID", cusID);
            try
            {
                if (cnn.State == System.Data.ConnectionState.Closed)
                {
                    cnn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListBill.Add(new BillDTO { OrderID = dr.GetInt32(0), CreateDate = dr.GetDateTime(1), TotalPrice = (float)dr.GetDouble(2) });
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return ListBill;
        }


    }
}