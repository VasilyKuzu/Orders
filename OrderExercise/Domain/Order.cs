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
        private readonly List<OrderItem> _items = new();
        public IReadOnlyList<OrderItem> Items => _items;
        public DateTime CreatedAt { get; private set; }
        public decimal TotalAmount => Items.Sum(p => p.TotalPrice);

        public Order()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            _items = new List<OrderItem>();
        }

        [JsonConstructor]
        public Order(Guid id, DateTime createdAt, List<OrderItem> items)
        {
            Id = id;
            CreatedAt = createdAt;
            _items = items ?? new List<OrderItem>();
        }

        public void AddItem(Product product, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
            {
                throw new Exception("Кол-во должно быть больше 0");
            }

            if (unitPrice < 0)
            {
                throw new Exception("Цена не может быть отрицательной");
            }

            var existingItem = Items.FirstOrDefault(p => p.Product.Article == product.Article);
            
            if (existingItem != null)
            {
                var newQuantity = existingItem.Quantity;
                newQuantity += quantity;

                existingItem.UpdateQuantity(newQuantity);
            }
            else
            {
                OrderItem orderItem = new OrderItem(this, product, quantity, unitPrice);
                _items.Add(orderItem);
            }
        }

        public void AddItem(OrderItem item)
        {
            if (item == null)
            {
                throw new Exception("Передано пустое значение");
            }

            var existingItem = Items.FirstOrDefault(p => p.Product.Article == item.Product.Article);

            if (existingItem != null)
            {
                var newQuantity = existingItem.Quantity;
                newQuantity += item.Quantity;

                existingItem.UpdateQuantity(newQuantity);
            }
            else
            {
                _items.Add(item);
            }
        }

        public void RemoveItem(Product product)
        {
            OrderItem? existingItem = Items.FirstOrDefault(p => p.Product.Article == product.Article);

            if (existingItem == null)
            {
                throw new Exception("Товар не найден");
            }

            _items.Remove(existingItem);
        }

        public void RemoveItem(string article)
        {
            OrderItem? existingItem = Items.FirstOrDefault(p => p.Product.Article == article);

            if (existingItem == null)
            {
                throw new Exception("Товар не найден");
            }

            _items.Remove(existingItem);
        }

        public void UpdateQuantity(string article, int value)
        {
            OrderItem? existingItem = Items.FirstOrDefault(p => p.Product.Article == article);

            if (existingItem == null)
            {
                throw new Exception("Товар не найден");
            }

            existingItem.UpdateQuantity(value);

            if (existingItem.Quantity <= 0)
            {
                _items.Remove(existingItem);
            }


        }

        public void UpdatePrice(string article, decimal value)
        {
            OrderItem? existingItem = Items.FirstOrDefault(p => p.Product.Article == article);

            if (existingItem == null)
            {
                throw new Exception("Товар не найден");
            }

            if (value < 0)
            {
                throw new Exception("Цена не может быть отрицательной");
            }

            existingItem.UpdatePrice(value);

        }

    }
}
