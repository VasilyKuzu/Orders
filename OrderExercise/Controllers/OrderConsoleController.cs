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
            IOrderRepository repository = new MemoryOrderRepository();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите место для хранения данных:\n0. Завершить программу\n1. Json в файле\n2. Внутрення память");

                if (!TryCastToInt(Console.ReadLine(), out int choiсe))
                {
                    Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                    WaitForInput();
                    continue;
                }
                switch (choiсe)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Clear();
                        repository = new JsonOrderRepository();
                        Console.WriteLine("В качестве хранения выбран файл, запись в формате Json");
                        WaitForInput();
                        break;
                    case 2:
                        Console.Clear();
                        repository = new MemoryOrderRepository();
                        Console.WriteLine("В качестве хранения выбрана внутренняя память");
                        WaitForInput();
                        break;
                    default:
                        Console.WriteLine("Неизвестный пункт меню");
                        WaitForInput();
                        continue;
                }
                break;
            }

            OrderService service = new OrderService(repository);

            while (true)
            {
                PrintMenu();

                if (!TryCastToInt(Console.ReadLine(), out int choiсe))
                {
                    Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                    WaitForInput();
                    continue;
                }

                switch (choiсe)
                {
                    case 0:
                        Console.Clear();
                        return;
                    case 1:
                        Console.Clear();
                        Guid orderId = service.CreateOrder();
                        Console.WriteLine($"Заказ # {orderId} создан");

                        WaitForInput();
                        break;
                    case 2:
                        Console.Clear();   
                        var orders = service.GetOrders();
                        foreach (Order o in orders)
                        {
                            Console.WriteLine($"Заказ {o.Id}, Итого к оплате: {o.TotalAmount}");

                            foreach (var item in o.Items)
                            {
                                Console.WriteLine($"Название товара: {item.Product.Name}\nАртикул: {item.Product.Article}\nЦена: {item.UnitPrice}\nКол-во: {item.Quantity}");
                            }
                        }

                        WaitForInput();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Введите Id искомого заказа: ");
                        Guid orderIdToGet;

                        string? input = Console.ReadLine();

                        if (!TryCastToGuid(input, out orderIdToGet))
                        {
                            Console.WriteLine("Некорректный формат, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        var foundOrderToGet = service.GetOrderById(orderIdToGet);

                        if (foundOrderToGet == null)
                        {
                            Console.WriteLine("Заказ не найден, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine($"Заказ {foundOrderToGet.Id}, Итого к оплате: {foundOrderToGet.TotalAmount}");

                        foreach (var item in foundOrderToGet.Items)
                        {
                            Console.WriteLine($"Название товара: {item.Product.Name}\nАртикул: {item.Product.Article}\nЦена: {item.UnitPrice}\nКол-во: {item.Quantity}");
                        }

                        WaitForInput();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Введите Id удаляемого заказа: ");
                        if (!TryCastToGuid(Console.ReadLine(), out Guid orderIdToDelete))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        var orderToDelete = service.GetOrderById(orderIdToDelete);
                        if (orderToDelete == null)
                        {
                            Console.WriteLine("Заказ с таким Id не найден, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        service.DeleteOrderById(orderIdToDelete);

                        Console.WriteLine($"Заказ {orderToDelete.Id} удален");
                        WaitForInput();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Введите Id заказа для последующего добавления товаров в него: ");
                        if (!TryCastToGuid(Console.ReadLine(), out Guid orderIdForAddingItems))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine("Введите название товара");
                        if (!ValidateStringInput(Console.ReadLine(), out string productName))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine("Введите артикул товара");
                        if (!ValidateStringInput(Console.ReadLine(), out string articleForAddingToOrder))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine("Введите цену товара");
                        if (!TryCastToDecimal(Console.ReadLine(), out decimal productPrice))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine("Введите кол-во товара");
                        if (!TryCastToInt(Console.ReadLine(), out int productQuantity))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        service.AddOrderItem(orderIdForAddingItems, productName, articleForAddingToOrder, productPrice, productQuantity);

                        Console.WriteLine($"Товар {productName} добавлен в заказ {orderIdForAddingItems}");
                        WaitForInput();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Введите Id заказа, в котором нужно изменить кол-во товара: ");
                        if (!TryCastToGuid(Console.ReadLine(), out Guid orderIdForUpdatingQuantity))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine("Введите артикул товара, кол-во которого нужно поменять");
                        if (!ValidateStringInput(Console.ReadLine(), out string articleForChangeQuantity))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine("Введите новое кол-во товара");
                        if (!TryCastToInt(Console.ReadLine(), out int updatedQuantity))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        service.UpdateQuantityOrderItem(orderIdForUpdatingQuantity, articleForChangeQuantity, updatedQuantity);

                        Console.WriteLine($"Кол-во товара {articleForChangeQuantity} стало {updatedQuantity}");
                        WaitForInput();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Введите Id заказа, в котором нужно изменить цену товара: ");
                        if (!TryCastToGuid(Console.ReadLine(), out Guid orderIdForUpdatingPrice))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine("Введите артикул товара, кол-во которого нужно поменять");
                        if (!ValidateStringInput(Console.ReadLine(), out string articleForChangePrice))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine("Введите новую цену товара");
                        if (!TryCastToDecimal(Console.ReadLine(), out decimal updatedPrice))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        service.UpdatePriceOrderItem(orderIdForUpdatingPrice, articleForChangePrice, updatedPrice);

                        Console.WriteLine($"Цена товара {articleForChangePrice} стала {updatedPrice}");
                        WaitForInput();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("Введите номер заказа, в котором нужно удалить товар: ");
                        if (!TryCastToGuid(Console.ReadLine(), out Guid orderIdForRemovingItem))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        Console.WriteLine("Введите артикул товара, кол-во которого нужно поменять");
                        if (!ValidateStringInput(Console.ReadLine(), out string articleForRemoving))
                        {
                            Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
                            WaitForInput();
                            continue;
                        }

                        service.DeleteItem(orderIdForRemovingItem, articleForRemoving);

                        Console.WriteLine($"Товар {articleForRemoving} удален из заказа {orderIdForRemovingItem}");
                        WaitForInput();
                        break;
                }
            }
        }

        public void PrintMenu()
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
                "7. Изменить цену товара в заказе\n" +
                "8. Убрать товар из заказа");
        }

        public void WaitForInput()
        {
            Console.WriteLine("Нажмите любую кнопку...");
            Console.ReadKey();
        }

        public bool TryCastToGuid(string? value, out Guid result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = Guid.Empty;
                return false;
            }

            return Guid.TryParse(value, out result);
        }

        public bool TryCastToInt(string? value, out int result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = 0;
                return false;
            }

            return int.TryParse(value, out result);
        }

        public bool TryCastToDecimal(string? value, out decimal result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = 0;
                return false;
            }

            return decimal.TryParse(value, out result);
        }

        public bool ValidateStringInput(string? input, out string result)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                result = string.Empty;
                return false;
            }

            result = input;
            return true;
        }
    }
}
