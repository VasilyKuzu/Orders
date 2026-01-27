using OrderExercise.Repository;
using OrderExercise.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderExercise.Application.Generators
{
    class OrderGenerator
    {
        private readonly IOrderRepository _repository;
        public OrderGenerator(IOrderRepository repository)
        {
            _repository = repository;
        }

        private readonly List<Product> products = new List<Product>();

        private readonly Random random = new Random();

        //генерируем данные с ограничениями: цена товара от 50 до 1000 рублей, кол-во товаров в заказе от 1 до 5
        public void GenerateOrders(int quantityOrders)
        {
            GenerateProducts(50);

            for (int i = 1; i<= quantityOrders; i++)
            {
                GenerateOrder();
            }
        }
        private void GenerateOrder()
        {
            Order order = new Order();

            int orderItemQuantity = random.Next(1, 6);

            for (int i = 1; i <= orderItemQuantity; i++)
            {
                int randomIndex = random.Next(0, products.Count);
                int productQuantity = random.Next(1, 4);
                decimal unitPrice = random.Next(50, 1001);

                OrderItem orderItem = new OrderItem(order, products[randomIndex], productQuantity, unitPrice);
                order.AddItem(orderItem);
            }

            _repository.AddOrder(order);
        }

        private void GenerateProducts(int quantity)
        {
            for (int i = 1; i <= quantity; i++)
            {
                string name;
                string article;

                name = $"Product {i}";
                article = $"PRD-{i}";

                Product product = new Product(name, article);
                products.Add(product);
            }
        }

    }
}
