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
                order.AddItem(OrderItem.Product, OrderItem.Quantity);
            }

            _repository.AddOrder(order);
        }

        public Guid CreateOrder()
        {
            Order order = new();

            _repository.AddOrder(order);
            return order.Id;
        }

        public List<Order> GetOrders()
        {
            return _repository.GetOrders();
        }
        public Order GetOrderById(Guid id)
        {
            return _repository.FindOrder(id);
        }
        public void DeleteOrderById(Guid id)
        {
            _repository.DeleteOrder(id);
        }

        public void AddOrderItem(Guid orderId, string name, string article, decimal price, int quantity)
        {
            Product product = new Product(name, article, price);
            OrderItem orderItem = new OrderItem(product, quantity);

            _repository.AddOrderItem(orderId, orderItem);
        }

        public void UpdateOrderItem(Guid orderId, string article, int quantity)
        {
            _repository.UpdateOrderItem(orderId, article, quantity);
        }

        public void DeleteItem(Guid orderId, string article)
        {
            _repository.DeleteItem(orderId, article);
        }

    }
}
