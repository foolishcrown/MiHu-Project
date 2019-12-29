using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MiHuStore.Models;
using System.Data;
using System.Diagnostics;

namespace MiHuStore.Models.Customer
{

    public class CustomerDAO
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;

        public CustomerDAO()
        {

        }



        public CustomerDTO GetCusInfo(string username)
        {
            CustomerDTO dto = null;
            string sql = "Select tel,fullname, address, email, rank from customer where username=@Username";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Username", username);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dto = new CustomerDTO();
                        dto.Phone = dr.GetString(0);
                        dto.CustomerName = dr.GetString(1);
                        dto.Address = dr.GetString(2);
                        dto.Email = dr.GetString(3);

                        if (dr.IsDBNull(4) == false) // check null or not before get data 
                        {

                            dto.Rank = dr.GetString(4);
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
            return dto;



        }

 



        public string GetRank(string idCus)
        {
            string result = "";
            try
            {
                string sql = "Select rank from customer where username=@username and rank IS NOT NULL";
                cnn = DB.GetConnection();
                cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@username", idCus);
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        result = dr.GetString(0);

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


        public float GetDiscount(string rank)
        {
            float result = 0;
            try
            {
                string sql = "Select discountValue from discountDetail where rank=@rank";
                cnn = DB.GetConnection();
                cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@rank", rank);
                if (cnn.State == ConnectionState.Closed)
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

        public bool UpdateRank(string username, string newRank)
        {
            bool result = false;
            string sql = "Update customer set rank=@Rank where username=@Username";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Rank", newRank);
            cmd.Parameters.AddWithValue("@Username", username);
            try
            {
                if (cnn.State == ConnectionState.Closed)
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


        public float GetPointOfCus(string username)
        {
            float result = 0;
            string sql = "Select point from customer where username=@Username";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Username", username);
            try
            {
                if (cnn.State == ConnectionState.Closed)
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


        public bool CheckOut(float totalPrice, string username)
        {
            bool result = false;
            try
            {
                string sql = "Update customer set point=point+@Total where username=@Username";
                cnn = DB.GetConnection();
                cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Total", totalPrice);
                cmd.Parameters.AddWithValue("@Username", username);
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    result = cmd.ExecuteNonQuery() > 0;
                }



            }
            finally
            {
                cnn.Close();
            }
            return result;
        }



        public bool CreateCustomer(CustomerDTO dto)
        {
            bool result = false;
            try
            {
                string sql = "insert into customer(username, fullname,email, address, point,tel) " +
                    "values (@Username,@Name,@Email, @Address,@Point,@Tel)";
                cnn = DB.GetConnection();
                cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Username", dto.Username);
                cmd.Parameters.AddWithValue("@Name", dto.CustomerName);
                cmd.Parameters.AddWithValue("@Email", dto.Email);
                cmd.Parameters.AddWithValue("@Address", dto.Address);
                cmd.Parameters.AddWithValue("@Tel", dto.Phone);

                cmd.Parameters.AddWithValue("@Point", dto.Point);
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    result = cmd.ExecuteNonQuery() > 0;
                }
               

            }
            finally
            {
                cnn.Close();
            }
            return result;
        }



        public bool UpdateCustomer(CustomerDTO dto)
        {
            bool result = false;
            string sql = "Update customer set fullname=@Fullname, tel=@Tel, address=@Address, email=@Email where username=@Username";
            cnn = DB.GetConnection();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Fullname", dto.CustomerName);
            cmd.Parameters.AddWithValue("@Tel", dto.Phone);
            cmd.Parameters.AddWithValue("@Address", dto.Address);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@Username", dto.Username);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            }
            return result;
        }




    }
}