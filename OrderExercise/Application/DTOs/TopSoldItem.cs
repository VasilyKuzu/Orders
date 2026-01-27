using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderExercise.Application.DTOs
{
    public class TopSoldItem
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }

        public TopSoldItem(string article, string name, int totalQuantity, decimal totalAmount)
        {
            Article = article;
            Name = name;
            TotalQuantity = totalQuantity;
            TotalAmount = totalAmount;
        }
    }

}
