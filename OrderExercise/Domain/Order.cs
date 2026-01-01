using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderExercise.Domain
{
    public class Order
    {
        public Guid Id { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public decimal TotalAmount => Items.Sum(p => p.TotalPrice);

        public Order(List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Items = items ?? new List<OrderItem>();
        }
        public Order(OrderItem item)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Items = new List<OrderItem> { item };
        }
        public Order()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Items = new List<OrderItem>();
        }

        [JsonConstructor]
        public Order(Guid id, DateTime createdAt, List<OrderItem> items)
        {
            Id = id;
            CreatedAt = createdAt;
            Items = items ?? new List<OrderItem>();
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
