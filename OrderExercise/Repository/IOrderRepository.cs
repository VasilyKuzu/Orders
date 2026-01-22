using OrderExercise.Domain;

namespace OrderExercise.Repository
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        IReadOnlyList<Order> GetOrders();
        Order? FindOrder(Guid orderId);
        void DeleteOrder(Guid orderId);
        void AddOrderItem(Guid orderId, OrderItem orderItem);
        void DeleteItem(Guid orderId, string article);
        void UpdatePriceOrderItem(Guid orderId, string article, decimal unitPrice);
        void UpdateQuantityOrderItem(Guid orderId, string article, int quantity);
    }
}
