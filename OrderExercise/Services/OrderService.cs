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

        public void CreateOrder(List<(Product product, int quantity)> itemsData)
        {
            Order order = new();

            foreach (var (product, quantity) in itemsData)
            {
                order.AddItem(product, quantity);
            }

            _repository.AddOrder(order);
        }

        public List<Order> GetOrders()
        {
           return _repository.GetOrders();
        }
    }
}
