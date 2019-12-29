using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mihu_Project.Models
{
    public class BillDetail
    {
        public int BillID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public string ProductName { get; set; }

    }

    public class BillDetailDB
    {
        private readonly string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public DataTable GetListDetail(int billID)
        {
            DataTable dtDetail = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "Select productID, productName, quantity, unitPrice From orderDetail Where orderID = @orderID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@orderID", billID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtDetail);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return dtDetail;
        }

    }
}