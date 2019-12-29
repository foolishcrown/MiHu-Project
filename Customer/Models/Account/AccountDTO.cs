using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiHuStore.Models.Account
{
    public class AccountDTO
    {
        public AccountDTO()
        {

        }

        public AccountDTO(string username, string password, int role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }
    }
}