using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mihu_Project.Models
{
    public class Product
    {
        public string ProductID { get; set; }

        public int CateID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public float UnitPrice { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime createdDate { get; set; }
    }
    public class ProductDB
    {

        //private readonly string connString = "Server=tcp:kaydb.database.windows.net,1433;Initial Catalog=mihu;Persist Security Info=False;User ID=kaytrieu;Password=Kaykay123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public DataTable GetAllProduct()
        {
            DataTable dtProduct = new DataTable();
            using (SqlConnection conn = DB.GetConnection())
            {
                conn.Open();
                string sql = "select * from product where isDeleted = 0";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dtProduct);
            }
            return dtProduct;
        }

        public Product FindProduct(string ProductID)
        {
            Product p = null;
            using (SqlConnection conn = DB.GetConnection())
            {
                string sql = "select * from product where ProductID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", ProductID);
                try
                {
                    SqlDataReader dr;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            p = new Product();
                            p.ProductID = ProductID;
                            p.CateID = dr.GetInt32(1);
                            p.createdDate = dr.GetSqlDateTime(2).Value;
                            p.UnitPrice = (float)dr.GetDouble(3);
                            p.ProductName = dr.GetString(4);
                            p.IsDeleted = dr.GetBoolean(5);
                            p.Image = dr.GetString(6);
                            p.Stock = dr.GetInt32(7);
                            p.Description = dr.GetString(8);

                        }
                    }
                }
                //catch(Exception ex)
                //{
                //    throw ;
                //}
                finally
                {
                    conn.Close();
                }
            }
            return p;
        }

        public DataTable GetAllDeletedProduct()
        {
            DataTable dtProduct = new DataTable();
            using (SqlConnection conn = DB.GetConnection())
            {
                conn.Open();
                string sql = "select * from product where isDeleted = 1";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dtProduct);
            }
            return dtProduct;
        }

        private string GetProductIDByCate(int cateID)
        {
            SqlConnection conn = DB.GetConnection();
            string sql = "SELECT TOP 1 ProductID FROM Product WHERE productCategoryID = @cateID ORDER BY productID desc";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cateID", cateID);
            try
            {
                SqlDataReader dr;
                string productID = "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                }
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string currentID = dr.GetString(0);
                    if (currentID != null)
                    {
                        string[] tmp = currentID.Split('_');
                        int temp = int.Parse(tmp[1]);
                        temp++;
                        productID = tmp[0] + "_" + temp;
                    }
                }
                if (productID.Equals(""))
                {
                    sql = "select description from Cate where cateID = @ID";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", cateID);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();

                    }
                    dr.Close();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        productID = dr.GetString(0) + "_1";
                    }
                }
                return productID;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        internal bool UpdateProduct(Product p)
        {
            SqlConnection conn = DB.GetConnection();
            string sql = "Update product set unitPrice = @Price, " +
            "productName = @Name, image = @Image, stock = @Stock, " +
            "description = @Description where productID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", p.ProductID);
            cmd.Parameters.AddWithValue("@Price", p.UnitPrice);
            cmd.Parameters.AddWithValue("@Name", p.ProductName);
            cmd.Parameters.AddWithValue("@Image", p.Image);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);
            cmd.Parameters.AddWithValue("@Description", p.Description);
            

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                bool result = cmd.ExecuteNonQuery() > 0;
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public bool DeleteProduct(string productID)
        {
            SqlConnection conn = DB.GetConnection();
            string sql = "update product set isDeleted = 1 where productID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", productID);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                bool result = cmd.ExecuteNonQuery() > 0;
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool ReAddProduct(string productID)
        {
            SqlConnection conn = DB.GetConnection();
            string sql = "update product set isDeleted = 0 where productID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", productID);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                bool result = cmd.ExecuteNonQuery() > 0;
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool InsertProduct(Product p)
        {
            string productID = GetProductIDByCate(p.CateID);
            SqlConnection conn = DB.GetConnection();
            string sql = "INSERT [dbo].[product] ([productID], [productCategoryID]," +
                " [unitPrice], [productName], [image], [stock], [description]) " +
                "VALUES (@ID, @CateID, @Price, @Name, @Pic, @Stock, @Des)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", productID);
            cmd.Parameters.AddWithValue("@CateID", p.CateID);
            cmd.Parameters.AddWithValue("@Price", p.UnitPrice);
            cmd.Parameters.AddWithValue("@Name", p.ProductName);
            cmd.Parameters.AddWithValue("@Pic", p.Image);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);
            cmd.Parameters.AddWithValue("@Des", p.Description);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                bool result = cmd.ExecuteNonQuery() > 0;
                return result;
            }

            finally
            {
                conn.Close();
            }

        }



    }

    public class CateDB
    {
        private readonly string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public DataTable GetAllCate()
        {
            DataTable dtCate = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string sql = "select * from Cate";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dtCate);
            }
            return dtCate;
        }
    }
}