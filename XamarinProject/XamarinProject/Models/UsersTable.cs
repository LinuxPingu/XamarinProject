using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class UsersTable:RealmObject
    {

        public string UserID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UsersTable()
        {
        }

        public UsersTable(string userID, string email, string username, string password)
        {
            UserID = userID;
            Email = email;
            Username = username;
            Password = password;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
