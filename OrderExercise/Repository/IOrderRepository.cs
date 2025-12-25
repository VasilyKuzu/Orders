using OrderExercise.Domain;

namespace OrderExercise.Repository
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        List<Order> GetOrders();
    }
}
