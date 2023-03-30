using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;

namespace OrderManagementWebAPI.Repos.OrdersRepository
{
    public interface IOrdersRepo
    {
        public Task<IEnumerable<Orders>> GetOrdersAsync();
        public Task<Orders> GetOrderByIdAsync(int orderNumber);
        public Task AddOrderAsync(Orders order);
        public Task<bool> DeleteOrderAsync(int orderNumber);
        public Task<CreateUpdateOrders> UpdateOrderAsync(int id, CreateUpdateOrders order);
        public Task<CreateUpdateOrders> UpdatePartiallyOrderAsync(int id, CreateUpdateOrders order);
    }
}
