using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace MiHuStore.Models.Product
{
    public class ProductDAO
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;

        public ProductDAO()
        {

        }

        

        public List<ProductDTO> SearchRandom(int cate)
        {
            List<ProductDTO> result = new List<ProductDTO>();
            try
            {
                string sql="";
                cnn = DB.GetConnection();
                if (cate == 0)
                {
                    sql = "Select TOP 8 productID, productName, unitPrice, description, image From product where stock > 0  and isDeleted=0 Order By NEWID()";
                    cmd = new SqlCommand(sql, cnn);

                }
                else
                {
                    sql = "Select TOP 8 productID, productName, unitPrice, description, image From product where productCategoryID=@Cate and stock > 0 and isDeleted=0 Order By NEWID()";
                    cmd = new SqlCommand(sql, cnn);
                    cmd.Parameters.AddWithValue("@Cate", cate);
                }
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
               
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    result.Add(new ProductDTO(dr.GetString(0), dr.GetString(1), (float)dr.GetDouble(2), dr.GetString(3), dr.GetString(4)));
                }
            }
            finally
            {
                cnn.Close();
            }
            return result;

        }




        public List<ProductDTO> SearchProduct(string name)
        {
            List<ProductDTO> result = new List<ProductDTO>();
            try
            {
                string sql = "Select productID, productName, unitPrice, description, image from product where productName LIKE @Name and stock > 0 and isDeleted=0";
                cnn = DB.GetConnection();
                cnn.Open();
                cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    result.Add(new ProductDTO(dr.GetString(0), dr.GetString(1), (float)dr.GetDouble(2), dr.GetString(3), dr.GetString(4)));
                }
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public int GetProductQuantity(string id)
        {
            int result = 0;
            try
            {

                string sql = "Select stock from  product where productID=@ID";
                cnn = DB.GetConnection();
                cnn.Open();
                cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@ID", id);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    result = dr.GetInt32(0);
                }
            }
            finally
            {
                cnn.Close();
            }
            return result;

        }

        public bool UpdateQuantitty(string id, int quantity)
        {
            bool result = false;
            try
            {
                string sql = "Update product set stock=stock-@Quan where productID=@ID";
                cnn = DB.GetConnection();
                cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Quan", quantity);
                cmd.Parameters.AddWithValue("@ID", id);
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    result = cmd.ExecuteNonQuery()>0;
                }
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public ProductDTO FindProduct(string id)
        {
            ProductDTO dto = null;
            try
            {
                string sql = "Select productID, productName, image, stock, description, unitPrice from product where productID=@ID";
                cnn = DB.GetConnection();
                cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("ID", id);
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dto = new ProductDTO(dr.GetString(0),dr.GetString(1), (float)dr.GetDouble(5), dr.GetString(4), dr.GetString(2), dr.GetInt32(3));

                    }
                }


            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return dto;
        }


    }
}