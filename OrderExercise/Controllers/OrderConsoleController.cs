using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderExercise.Repository;
using OrderExercise.Services;

namespace OrderExercise.Controllers
{
    internal class OrderConsoleController
    {
        public void Start()
        {
            MemoryOrderRepository repository = new();

            OrderService service = new OrderService(repository);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите желаемое действие:");
                Console.WriteLine(
                    "0. Завершить программу\n" +
                    "1. Создать заказ\n" +
                    "2. Посмотреть список заказов\n" +
                    "3. Найти заказ по номеру\n" +
                    "4. Удалить заказ\n" +
                    "5. Добавить товар в заказ\n" +
                    "6. Изменить кол-во товаров в заказе");

                int choiсe = Convert.ToInt32(Console.ReadLine());

                switch (choiсe)
                {
                    case 0:
                        Console.Clear();
                        return;
                    case 1:
                        Console.Clear();
                        Guid orderId = service.CreateOrder();
                        Console.WriteLine($"Заказ #{orderId} создан");

                        Console.WriteLine("Нажмите любую кнопку...");
                        Console.ReadKey();
                        break;

                        /* это для будущей реализации добавления товара в заказ
                        Console.WriteLine("Введите название товара");
                        string product_name = Console.ReadLine();
                        Console.WriteLine("Введите артикул товара");
                        string product_article = Console.ReadLine();
                        Console.WriteLine("Введите цену товара");
                        decimal product_price = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Введите кол-во товара");
                        decimal product_quantity = Convert.ToInt32(Console.ReadLine());

                        service.AddProduct(product_name, product_article, product_price);
                        */
                }
            }
        }
    }
}
