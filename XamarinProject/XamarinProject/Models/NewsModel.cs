using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class NewsModel
    {

        public string Header { get; set; }
        public string Image { get; set; }

        public string Description { get; set; }
        public string Footer { get; set; }

        public NewsModel()
        {
        }

        public NewsModel(string header, string image, string description, string footer)
        {
            Header = header;
            Image = image;
            Description = description;
            Footer = footer;
        }
    }
}
