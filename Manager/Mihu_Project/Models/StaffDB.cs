using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mihu_Project.Models
{
    public class Staff
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password must match")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }

        [Required]
        public string Tel { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }
    }
    public class StaffDB
    {
        public bool InsertAccount(Staff s)
        {
            SqlConnection conn = DB.GetConnection();
            string sql = " INSERT[dbo].[Account]([username], [password], [role]) VALUES(@Username, @Password, 1)";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Username", s.Username);
                cmd.Parameters.AddWithValue("@Password", s.Password);

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


        }

        public bool InsertStaff(Staff s)
        {
            if (InsertAccount(s))
            {
                SqlConnection conn = DB.GetConnection();
                string sql = "INSERT [dbo].[staff] ([username], [fullname], [tel], [address], [email]) VALUES (@Username, @Fullname, @Tel, @Address, @Email)";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Username", s.Username);
                cmd.Parameters.AddWithValue("@Fullname", s.Fullname);
                cmd.Parameters.AddWithValue("@Tel", s.Tel);
                cmd.Parameters.AddWithValue("@Address", s.Address);
                cmd.Parameters.AddWithValue("@Email", s.Email);

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
            else { return false; }

        }

        public DataTable GetAllStaff()
        {
            DataTable dtProduct = new DataTable();
            using (SqlConnection conn = DB.GetConnection())
            {
                conn.Open();
                string sql = "select * from staff";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dtProduct);
            }
            return dtProduct;
        }

        public Staff FindStaff(string StaffUsername)
        {
            Staff s = null;
            using (SqlConnection conn = DB.GetConnection())
            {
                string sql = "select * from staff where username = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", StaffUsername);
                try
                {
                    SqlDataReader dr;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            s = new Staff();
                            s.Username = StaffUsername;
                            s.Fullname = dr.GetString(1);
                            s.Tel = dr.GetString(2);
                            s.Address = dr.GetString(3);
                            s.Email = dr.GetString(4);

                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return s;
        }
        public bool deleteAccount(string staffUsername)
        {
            bool result = false;
            using (SqlConnection conn = DB.GetConnection())
            {
                string sql = "delete from account where username = @username";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", staffUsername);
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        result = cmd.ExecuteNonQuery() > 0;
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool DeleteStaff(string staffUsername)
        {
            bool result = false;

            using (SqlConnection conn = DB.GetConnection())
            {
                string sql = "delete from staff where username = @username";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", staffUsername);
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        result = cmd.ExecuteNonQuery() > 0;
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            if (result)
            {
                if (!deleteAccount(staffUsername))
                {
                    result = false;
                }
            }
            return result;
        }

        public bool UpdateStaff(Staff p)
        {
            
                SqlConnection conn = DB.GetConnection();
                string sql = "Update staff set fullname = @FullName, tel = @Tel, " +
                "address = @Address, email = @Email where username = @Username";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FullName", p.Fullname);
                cmd.Parameters.AddWithValue("@Tel", p.Tel);
                cmd.Parameters.AddWithValue("@Address", p.Address);
                cmd.Parameters.AddWithValue("@Email", p.Email);
                cmd.Parameters.AddWithValue("@Username", p.Username);

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
    }
}