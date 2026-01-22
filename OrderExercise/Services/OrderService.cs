using OrderExercise.Domain;
using OrderExercise.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderExercise.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public void CreateOrder(List<OrderItem> itemsData)
        {
            Order order = new();

            foreach (var OrderItem in itemsData)
            {
                order.AddItem(OrderItem.Product, OrderItem.Quantity, OrderItem.UnitPrice);
            }

            _repository.AddOrder(order);
        }

        public Guid CreateOrder()
        {
            Order order = new();

            _repository.AddOrder(order);
            return order.Id;
        }

        public IReadOnlyList<Order> GetOrders()
        {
            return _repository.GetOrders();
        }
        public Order? GetOrderById(Guid id)
        {
            return _repository.FindOrder(id);
        }
        public void DeleteOrderById(Guid id)
        {
            _repository.DeleteOrder(id);
        }

        public void AddOrderItem(Guid orderId, string name, string article, decimal price, int quantity)
        {
            Product product = new Product(name, article);
            OrderItem orderItem = new OrderItem(product, quantity, price);

            _repository.AddOrderItem(orderId, orderItem);
        }

        public void UpdateQuantityOrderItem(Guid orderId, string article, int quantity)
        {
            _repository.UpdateQuantityOrderItem(orderId, article, quantity);
        }

        public void UpdatePriceOrderItem(Guid orderId, string article, decimal unitPrice)
        {
            _repository.UpdatePriceOrderItem(orderId, article, unitPrice);
        }

        public void DeleteItem(Guid orderId, string article)
        {
            _repository.DeleteItem(orderId, article);
        }

    }
}
