using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderExercise.Domain
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
        public Order(OrderItem item)
        {
            Items.Add(item);
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

        public void AddItem(OrderItem item)
        {
            if (item == null)
            {
                throw new Exception("Передано пустое значение");
            }

            var existingItem = Items.FirstOrDefault(p => p == item);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
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

        public void RemoveItem(string article)
        {
            OrderItem? existingItem = Items.FirstOrDefault(p => p.Product.Article == article);

            if (existingItem == null)
            {
                throw new Exception("Товар не найден");
            }

            Items.Remove(existingItem);
        }

        public void UpdateQuantity(string article, int value)
        {
            OrderItem? existingItem = Items.FirstOrDefault(p => p.Product.Article == article);

            if (existingItem == null)
            {
                throw new Exception("Товар не найден");
            }

            existingItem.Quantity = value;

            if (existingItem.Quantity <= 0)
            {
                Items.Remove(existingItem);
            }
        }
    }
}
