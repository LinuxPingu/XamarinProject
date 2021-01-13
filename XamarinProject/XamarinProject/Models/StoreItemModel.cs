using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class StoreItemModel
    {

        public string ItemID { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public StoreItemModel()
        {
        }

        public StoreItemModel(string itemID, string title, string image, string description, double price)
        {
            ItemID = itemID;
            Title = title;
            Image = image;
            Description = description;
            Price = price;
        }
    }
}
