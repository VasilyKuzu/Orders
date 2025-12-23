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
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                throw new Exception("Кол-во должно быть больше 0");
            }

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

        public void RemoveItem(Product product)
        {
            OrderItem? existingItem = Items.FirstOrDefault(p => p.Product.Article == product.Article);

            if (existingItem == null)
            {
                throw new Exception("Товар не найден");
            }

            Items.Remove(existingItem);
        }

        public void UpdateQuantity(Product product, int value)
        {
            OrderItem? existingItem = Items.FirstOrDefault(p => p.Product.Article == product.Article);


            if (existingItem == null)
            {
                throw new Exception("Товар не найден");
            }

            existingItem.Quantity += value;

            if (existingItem.Quantity <= 0)
            {
                Items.Remove(existingItem);
            }

        }

        public void PrintOrder()
        {
            var sortedItems = Items.OrderByDescending(p => p.Product.Name);

            foreach(OrderItem o in sortedItems)
            {
                Console.WriteLine($"Название товара: {o.Product.Name}, артикул: {o.Product.Article}, количество: {o.Quantity}, цена: {o.Product.Price}, сумма: {o.TotalPrice}");
            }
            Console.WriteLine($"Итого сумма заказа: {TotalAmount}");
        }
    }
}
