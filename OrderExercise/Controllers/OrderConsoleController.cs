using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderExercise.Domain;
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
                    "6. Изменить кол-во товаров в заказе\n" +
                    "7. Убрать товар из заказа");

                int choiсe = Convert.ToInt32(Console.ReadLine());

                switch (choiсe)
                {
                    case 0:
                        Console.Clear();
                        return;
                    case 1:
                        Console.Clear();
                        Guid orderId = service.CreateOrder();
                        Console.WriteLine($"Заказ # {orderId} создан");

                        Console.WriteLine("Нажмите любую кнопку...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();   
                        var orders = service.GetOrders();
                        foreach (Order o in orders)
                        {
                            Console.WriteLine($"Заказ {o.Id}, Итого к оплате: {o.TotalAmount}");

                            foreach (var item in o.Items)
                            {
                                Console.WriteLine($"Название товара: {item.Product.Name}\nАртикул: {item.Product.Article}\nЦена: {item.Product.Price}\nКол-во: {item.Quantity}");
                            }
                        }

                        Console.WriteLine("Нажмите любую кнопку...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Введите Id заказа: ");
                        Guid orderIdToGet = Guid.Parse(Console.ReadLine());
                        var foundOrderToGet = service.GetOrderById(orderIdToGet);
                        Console.WriteLine($"Заказ {foundOrderToGet.Id}, Итого к оплате: {foundOrderToGet.TotalAmount}");

                        foreach (var item in foundOrderToGet.Items)
                        {
                            Console.WriteLine($"Название товара: {item.Product.Name}\nАртикул: {item.Product.Article}\nЦена: {item.Product.Price}\nКол-во: {item.Quantity}");
                        }

                        Console.WriteLine("Нажмите любую кнопку...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Введите Id заказа: ");
                        Guid orderIdToDelete = Guid.Parse(Console.ReadLine());
                        var orderToDelete = service.GetOrderById(orderIdToDelete);
                        service.DeleteOrderById(orderIdToDelete);
                        Console.WriteLine($"Заказ {orderToDelete.Id} удален");

                        Console.WriteLine("Нажмите любую кнопку...");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Введите номер заказа для последующего добавления товаров в него: ");
                        Guid orderIdForAddingItems = Guid.Parse(Console.ReadLine());
                        Console.WriteLine("Введите название товара");
                        string productName = Console.ReadLine();
                        Console.WriteLine("Введите артикул товара");
                        string articleForAddingToOrder = Console.ReadLine();
                        Console.WriteLine("Введите цену товара");
                        decimal productPrice = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Введите кол-во товара");
                        int product_quantity = Convert.ToInt32(Console.ReadLine());

                        service.AddOrderItem(orderIdForAddingItems, productName, articleForAddingToOrder, productPrice, product_quantity);

                        Console.WriteLine($"Товар {productName} добавлен в заказ {orderIdForAddingItems}");
                        Console.WriteLine("Нажмите любую кнопку...");
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Введите номер заказа, в котором нужно изменить кол-во товара: ");
                        Guid orderIdForUpdatingQuantity = Guid.Parse(Console.ReadLine());
                        Console.WriteLine("Введите артикул товара, кол-во которого нужно поменять");
                        string articleForChangeQuantity = Console.ReadLine();
                        Console.WriteLine("Введите новое кол-во товара");
                        int updatedQuantity = Convert.ToInt32(Console.ReadLine());

                        service.UpdateOrderItem(orderIdForUpdatingQuantity, articleForChangeQuantity, updatedQuantity);

                        Console.WriteLine($"Кол-во товара стало {updatedQuantity}");
                        Console.WriteLine("Нажмите любую кнопку...");
                        Console.ReadKey();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Введите номер заказа, в котором нужно удалить товар: ");
                        Guid orderIdForRemovingItem = Guid.Parse(Console.ReadLine());
                        Console.WriteLine("Введите артикул товара, кол-во которого нужно поменять");
                        string articleForRemoving = Console.ReadLine();

                        service.DeleteItem(orderIdForRemovingItem, articleForRemoving);

                        Console.WriteLine($"Товар {articleForRemoving} удален из заказа {orderIdForRemovingItem}");
                        Console.WriteLine("Нажмите любую кнопку...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
