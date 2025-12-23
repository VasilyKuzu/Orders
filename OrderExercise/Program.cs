using OrderExercise;

class Program
{
    public static void Main()
    {

        Product cherry = new Product("Вишня", "cherry23", 40);
        Product grape = new Product("Виноград", "grape23", 60);

        OrderService service = new OrderService();

        var order = service.CreateOrder();

        service.AddItem(order, cherry, 1);
        service.AddItem(order, grape, 2);

        service.RemoveItem(order, cherry);
        service.UpdateQuantity(order, grape, 45);

        service.Print(order);

    }
}