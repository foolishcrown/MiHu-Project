using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MiHuStore.Models;

namespace MiHuStore.Models.Order
{
    public class OrderDetailDAO
    {

        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;

        public OrderDetailDAO()
        {

        }
        public List<OrderDetailDTO> GetBillDetail(int orderID)
        {
            List<OrderDetailDTO> result = new List<OrderDetailDTO>();
            string sql = "select productName, unitPrice, quantity from orderDetail where orderID=@ID";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@ID", orderID);
            try
            {
                if (cnn.State == System.Data.ConnectionState.Closed)
                {
                    cnn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result.Add(new OrderDetailDTO() { ProductName=dr.GetString(0),UnitPrice=(float)dr.GetDouble(1),Quantity=dr.GetInt32(2) });
                    }
                }
            }catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public bool CreateOrderDetail(OrderDetailDTO dto)
        {
            bool result = false;
            string sql = "insert into orderDetail(orderID, productID, quantity, unitPrice,productName) values(@OrderID, @ProductID, @Quan, @Price,@ProductName)";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@OrderID", dto.OrderID);
            cmd.Parameters.AddWithValue("@ProductID", dto.ProductID);
            cmd.Parameters.AddWithValue("@Quan", dto.Quantity);
            cmd.Parameters.AddWithValue("@Price", dto.UnitPrice);
            cmd.Parameters.AddWithValue("@ProductName", dto.ProductName);
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
    }
}