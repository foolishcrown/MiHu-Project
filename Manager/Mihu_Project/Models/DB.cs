using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mihu_Project.Models
{
    public static class DB
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=.;Integrated Security=true;Initial Catalog=mihu");

        }
    }
}