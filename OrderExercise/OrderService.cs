using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderExercise
{
    public class OrderService
    {
        public Order CreateOrder()
        {
            return new Order();
        }

        public void AddItem(Order order, Product product, int quantity)
        {
            order.AddItem(product, quantity);
        }

        public void RemoveItem(Order order, Product product)
        {
            order.RemoveItem(product);
        }

        public void UpdateQuantity(Order order, Product product, int quantity)
        {
            order.UpdateQuantity(product, quantity);
        }

        public void Print(Order order)
        {
            order.PrintOrder();
        }
    }
}
