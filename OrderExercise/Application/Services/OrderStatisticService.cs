using OrderExercise.Application.DTOs;
using OrderExercise.Domain;
using OrderExercise.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderExercise.Application.Services
{
    class OrderStatisticService
    {
        private readonly IOrderRepository _repository;
        public OrderStatisticService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Order> GetTopOrders(int quantity)
        {
            IEnumerable<Order> allOrders = _repository.GetOrders();

            var result = allOrders.OrderByDescending(o => o.TotalAmount).Take(quantity);
            return result;
        }

        public IEnumerable<TopSoldItem> GetTopSoldItems(int quantity)
        {
            IEnumerable<Order> allOrders = _repository.GetOrders();

            IEnumerable<TopSoldItem> result = allOrders.SelectMany(o => o.Items)
                                             .GroupBy(i => i.Product.Article)
                                             .Select(g => new TopSoldItem(
                                                 g.Key,
                                                 g.First().Product.Name,
                                                 g.Sum(i => i.Quantity),
                                                 g.Sum(i => i.TotalPrice)
                                             )).OrderByDescending(item => item.TotalQuantity)
                                             .Take(quantity);

            return result;
        }

        public decimal GetAverageAmount()
        {
            IEnumerable<Order> allOrders = _repository.GetOrders();
            if (!allOrders.Any()) return 0m;
            return allOrders.Average(o => o.TotalAmount);
        }


    }
}
