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
        private readonly List<Order> _orders = new();
        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }
        public IReadOnlyList<Order> GetOrders()
        {
            return _orders;
        }

        public Order? FindOrder(Guid id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public void DeleteOrder(Guid id)
        {
            var orderToDelete = _orders.FirstOrDefault(o => o.Id == id);
            if (orderToDelete == null)
                return;
            _orders.Remove(orderToDelete);
        }
        public void AddOrderItem(Guid orderId, OrderItem orderItem)
        {
            var foundOrder = _orders.FirstOrDefault(o => o.Id == orderId);
            if (foundOrder == null)
                return;
            foundOrder.AddItem(orderItem);
        }
        public void UpdateOrderItem(Guid orderId, string article, int quantity)
        {
            var foundOrder = _orders.FirstOrDefault(o => o.Id == orderId);
            if (foundOrder == null)
                return;
            foundOrder.UpdateQuantity(article, quantity);
        }
        public void DeleteItem(Guid orderId, string article)
        {
            var foundOrder = _orders.FirstOrDefault(o => o.Id == orderId);
            if (foundOrder == null)
                return;
            foundOrder.RemoveItem(article);
        }
    }
}
