using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class OrderListItemModel
    {
        public string OrderID { get; set; }
        public string Detail { get; set; }
        public string Status { get; set; }

        public OrderListItemModel()
        {
        }

        public OrderListItemModel(string orderID, string detail, string status)
        {
            OrderID = orderID;
            Detail = detail;
            Status = status;
        }
    }
}
