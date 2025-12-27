## Сценарий: Создание заказа

1. Пользователь вводит данные в консоли
2. ConsoleController собирает входные данные
3. ConsoleController вызывает OrderService.CreateOrder(...)
4. OrderService:
   - создаёт Order
   - добавляет OrderItem
   - проверяет бизнес-правила
5. OrderService передаёт Order в IOrderRepository
6. Repository сохраняет заказ

## Сценарий: Просмотр заказов

1. Пользователь выбирает пункт "Показать заказы"
2. ConsoleController вызывает OrderService.GetOrders()
3. Service получает данные из Repository
4. Controller выводит данные в консоль
