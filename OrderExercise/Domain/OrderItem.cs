namespace OrderExercise.Domain
{
    public class OrderItem
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => UnitPrice * Quantity;

        public OrderItem(Product product, int quantity, decimal unitPrice)
        {
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;

        }

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;

            if (quantity < 0)
            {
                throw new Exception("Кол-во товара в заказе не может быть меньше нуля");
            }
        }
        public void UpdatePrice(decimal price)
        {
            UnitPrice = price;

            if (price < 0)
            {
                throw new Exception("Цена товара в заказе не может быть меньше нуля");
            }
        }
    }
}
