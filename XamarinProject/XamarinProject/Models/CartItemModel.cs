using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class CartItemModel
    {

      
        public string CartID { get; set; }
        public string ItemID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }

        public CartItemModel()
        {
        }

        public CartItemModel(string cartID, string itemID, string title, string image, int quantity, double subtotal)
        {
            CartID = cartID;
            ItemID = itemID;
            Title = title;
            Image = image;
            Quantity = quantity;
            Subtotal = subtotal;
        }
    }
}
