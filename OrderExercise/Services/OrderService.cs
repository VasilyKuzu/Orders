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

    }
}
