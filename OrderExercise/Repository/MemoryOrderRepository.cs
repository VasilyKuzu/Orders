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

        public Order FindOrder(Guid id)
        {
            return orders.FirstOrDefault(o => o.Id == id);
        }

        public void DeleteOrder(Guid id)
        {
            var orderToDelete = orders.FirstOrDefault(o => o.Id == id);
            orders.Remove(orderToDelete);
        }
        public void AddOrderItem(Guid orderId, OrderItem orderItem)
        {
            var foundOrder = orders.FirstOrDefault(o => o.Id == orderId);

            foundOrder.AddItem(orderItem);
        }
        public void UpdateOrderItem(Guid orderId, string article, int quantity)
        {
            var foundOrder = orders.FirstOrDefault(o => o.Id == orderId);
            foundOrder.UpdateQuantity(article, quantity);
        }
        public void DeleteItem(Guid orderId, string article)
        {
            var foundOrder = orders.FirstOrDefault(o => o.Id == orderId);
            foundOrder.RemoveItem(article);
        }
    }
}
