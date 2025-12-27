using OrderExercise.Controllers;
using OrderExercise.Domain;
using OrderExercise.Repository;
using OrderExercise.Services;

class Program
{
    public static void Main()
    {
        OrderConsoleController program_start = new OrderConsoleController();
        program_start.Start();
        /*
        MemoryOrderRepository repository = new();

        OrderService service = new OrderService(repository);

        Product cherry = new Product("Вишня", "cherry23", 40);
        Product grape = new Product("Виноград", "grape23", 60);

        OrderItem orderItem1 = new OrderItem(cherry, 3);
        OrderItem orderItem2 = new OrderItem(grape, 2);

        List<OrderItem> itemsData = new List<OrderItem>
        {
            orderItem1, orderItem2
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
        */
    }
}