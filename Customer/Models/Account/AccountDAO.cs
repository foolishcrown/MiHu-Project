using MiHuStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MiHuStore.Models.Account
{
    public class AccountDAO
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;

        public AccountDAO()
        {

        }

        public string Login(string customerID, string password)
        {
            string username = null;
            try
            {
                string sql = "Select username from Account where username=@Username and password=@Pass and role=2";
                cnn = DB.GetConnection();

                cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@Username", customerID); // neu customerID hay password null thi se co error
                cmd.Parameters.AddWithValue("@Pass", password);
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {

                        username = dr.GetString(0);

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
            return username;
        }

        public bool CheckDuplicate(string username)
        {
            bool result = false;
            string sql = "Select username from Account where username=@Username";
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
                        result = true;
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
        public int CheckRole(string username, string password)
        {
            int role = -1;
            try
            {
                using (SqlConnection conn = DB.GetConnection())
                {
                    string sql = "Select Role From Account Where username = @username and password = @password";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            role = dr.GetInt32(0);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return role;
        }

        public int GetRole(string username)
        {
            int role = -1;
            try
            {
                using (SqlConnection conn = DB.GetConnection())
                {
                    string sql = "Select Role From Account Where username = @username";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            role = dr.GetInt32(0);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return role;
        }

        public bool CreateAccount(AccountDTO dto)
        {
            bool result = false;
            cnn = DB.GetConnection();
            string sql = "insert into Account(username,password,role) values(@Username,@Pass,@Role)";
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Username", dto.Username);
            cmd.Parameters.AddWithValue("@Pass", dto.Password);
            cmd.Parameters.AddWithValue("@Role", dto.Role);

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