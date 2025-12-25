using OrderExercise.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderExercise.Repository
{
    public class MemoryOrderRepository : IOrderRepository
    {
        public List<Order> orders = new();
        public void AddOrder(Order order)
        {
            orders.Add(order);
        }
        public List<Order> GetOrders()
        {
            return orders;
        }
    }
}
