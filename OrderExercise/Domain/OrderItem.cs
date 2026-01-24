namespace OrderExercise.Domain
{
    public class OrderItem
    {
        public Guid Id { get; private set; }

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }

        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => UnitPrice * Quantity;

        public OrderItem(Order order, Product product, int quantity, decimal unitPrice)
        {
            if (quantity < 0)
            {
                throw new Exception("Кол-во товара в заказе не может быть меньше нуля");
            }

            if (unitPrice < 0)
            {
                throw new Exception("Цена товара в заказе не может быть меньше нуля");
            }

            Id = Guid.NewGuid();

            ProductId = product.Id;
            Product = product;

            OrderId = order.Id;
            Order = order;

            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new Exception("Кол-во товара в заказе не может быть меньше нуля");
            }

            Quantity = quantity;
        }
        public void UpdatePrice(decimal price)
        {

            if (price < 0)
            {
                throw new Exception("Цена товара в заказе не может быть меньше нуля");
            }

            UnitPrice = price;

        }
    }
}
