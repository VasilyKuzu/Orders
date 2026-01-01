using OrderExercise.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderExercise.Repository
{
    class JsonOrderRepository : IOrderRepository
    {
        private string _path = Path.Combine(AppContext.BaseDirectory, "Orders.txt");

        public void WriteAll(List<Order> orders)
        {
            string json = JsonSerializer.Serialize(orders);
            File.WriteAllText(_path, json);
        }
        public List<Order> ReadAll()
        {
            if (!Path.Exists(_path))
            {
                return new List<Order>();
            }

            string json = File.ReadAllText(_path);

            if (json == null)
            {
                return new List<Order>();
            }

            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(json) ?? new();
            return orders;
        }
        public void AddOrder(Order order)
        {
            var orders = ReadAll();
            orders.Add(order);
            WriteAll(orders);
        }
        public IReadOnlyList<Order> GetOrders()
        {
            var orders = ReadAll();
            return orders;
        }
        public void DeleteOrder(Guid orderId)
        {
            var orders = ReadAll();
            var order = orders.FirstOrDefault(p => p.Id == orderId);
            if (order == null) return;
            orders.Remove(order);
            WriteAll(orders);
        }
        public Order? FindOrder(Guid orderId)
        {
            var orders = ReadAll();
            var order = orders.FirstOrDefault(p => p.Id == orderId);
            return order;
        }
        public void AddOrderItem(Guid orderId, OrderItem orderItem)
        {
            var orders = ReadAll();
            var order = orders.FirstOrDefault(p => p.Id == orderId);
            if (order == null) return;
            order.AddItem(orderItem);
            WriteAll(orders);
        }
        public void DeleteItem(Guid orderId, string article)
        {
            var orders = ReadAll();
            var order = orders.FirstOrDefault(p => p.Id == orderId);
            if (order == null) return;
            order.RemoveItem(article);
            WriteAll(orders);
        }
        public void UpdateOrderItem(Guid orderId, string article, int quantity)
        {
            var orders = ReadAll();
            var order = orders.FirstOrDefault(p => p.Id == orderId);
            if (order == null) return;
            order.UpdateQuantity(article, quantity);
            WriteAll(orders);
        }
    }
}
