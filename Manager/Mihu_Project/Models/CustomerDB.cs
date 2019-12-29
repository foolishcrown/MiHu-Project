using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mihu_Project.Models
{



    public class CustomerDB
    {
        private readonly string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public DataTable GetAllCus()
        {
            DataTable dtCus = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string sql = "Select fullname, tel, address, email, rank From customer";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dtCus);
            }

            return dtCus;
        }


    }
}