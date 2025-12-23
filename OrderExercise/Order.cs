using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderExercise
{
    public class Order
    {
        public Guid Id { get; } = Guid.NewGuid();
        public List<OrderItem> Items = new();
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public decimal TotalAmount => Items.Sum(p => p.TotalPrice);

        public Order(List<OrderItem> items)
        {
            Items = items ?? new List<OrderItem>();
        }

        public void AddItem(Product product, int quantity)
        {

            var existingItem = Items.FirstOrDefault(p => p.Product.Article == product.Article);
            
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                OrderItem orderItem = new OrderItem(product, quantity);
                Items.Add(orderItem);
            }

        }

        public void PrintOrder()
        {
            foreach(OrderItem o in Items)
            {
                Console.WriteLine($"Название товара: {o.Product.Name}, артикул: {o.Product.Article}, количество: {o.Quantity}, цена: {o.Product.Price}, сумма: {o.TotalPrice}");
            }
            Console.WriteLine($"Итого сумма заказа: {TotalAmount}");
        }
    }
}
