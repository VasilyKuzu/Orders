using OrderExercise.Domain;
using OrderExercise.Repository;
using OrderExercise.Services;

class Program
{
    public static void Main()
    {

        MemoryOrderRepository repository = new();

        OrderService service = new OrderService(repository);

        var itemsData = new List<(Product, int)>
        {
            (new Product("Вишня", "cherry23", 40), 3),
            (new Product("Виноград", "grape23", 60), 2)
        };

        service.CreateOrder(itemsData);

        var ordersForReading = service.GetOrders();

        foreach(Order o in ordersForReading)
        {
            Console.WriteLine($"Заказ {o.Id}, Итого к оплате: {o.TotalAmount}");

            foreach (var item in o.Items)
            {
                Console.WriteLine($"Название товара: {item.Product.Name}\nАртикул: {item.Product.Article}\nЦена: {item.Product.Price}\nКол-во: {item.Quantity}");
            }
        }
    }
}