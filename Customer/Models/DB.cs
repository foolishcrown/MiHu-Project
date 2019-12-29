using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MiHuStore.Models
{
    public class DB
    {
      public static SqlConnection GetConnection()
        {
            SqlConnection cnn = new SqlConnection("Data Source=localhost;Initial Catalog=mihu;User ID=sa;Password=1");
            return cnn;
        }



    }
}