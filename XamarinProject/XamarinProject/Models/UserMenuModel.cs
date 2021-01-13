using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class UserMenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public UserMenuModel()
        {
        }

        public UserMenuModel(int id, string name, string icon)
        {
            Id = id;
            Name = name;
            Icon = icon;
        }
    }
}
