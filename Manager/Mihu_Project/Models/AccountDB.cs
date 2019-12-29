using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Mihu_Project.Models
{
    
    public class Account
    {
       
        
        public string Username { get; set; }
        
        public string Password { get; set; }
       
        public int Role { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ChangPasswordModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }


    public class AccountDB
    {
        //private readonly string connString = "Server=tcp:kaydb.database.windows.net,1433;Initial Catalog=mihu;Persist Security Info=False;User ID=kaytrieu;Password=Kaykay123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public int GetRole(string username)
        {
            int role = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
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

        public bool ChangePass(string username, string password)
        {
            bool check = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "Update Account Set password = @Pass Where username = @User";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Pass", password);
                    cmd.Parameters.AddWithValue("@User", username);
                    check = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return check; 
        }

        public int CheckRole(string username, string password)
        {
            int role = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
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
    }
}