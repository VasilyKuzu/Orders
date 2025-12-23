using OrderExercise;

class Program
{
    public static void Main()
    {
        var products = new List<Product>
        {
            new Product("Яблоко", "apple23", 45),
            new Product("Банан", "banana23", 30),
            new Product("Картошка", "potato23", 15)
        };

        var productItems = new List<OrderItem>
        {
            new OrderItem (products[0], 3),
            new OrderItem (products[1], 5),
            new OrderItem (products[2], 12)
        };

        Order order1 = new Order(productItems);

        Product orange = new Product("Orange", "orange23", 20);

        order1.AddItem(orange, 3);

        order1.PrintOrder();
    }
}