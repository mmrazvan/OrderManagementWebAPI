using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;
using OrderManagementWebAPI.Repos.OrdersRepository;

namespace OrderManagementWebAPI.Services.OrdersService
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepo _ordersRepo;
        public OrdersService(IOrdersRepo ordersRepo)
        {
            _ordersRepo = ordersRepo;
        }

        public async Task AddOrderAsync(Orders order)
        {
            await _ordersRepo.AddOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int orderNumber)
        {
            return await _ordersRepo.DeleteOrderAsync(orderNumber);
        }

        public async Task<Orders> GetOrderByIdAsync(int orderNumber)
        {
            return await _ordersRepo.GetOrderByIdAsync(orderNumber);
        }

        public async Task<IEnumerable<Orders>> GetOrdersAsync()
        {
            return await _ordersRepo.GetOrdersAsync();
        }

        public async Task<CreateUpdateOrders> UpdateOrderAsync(int id, CreateUpdateOrders order)
        {
            return await _ordersRepo.UpdateOrderAsync(id, order);
        }

        public async Task<CreateUpdateOrders> UpdatePartiallyOrderAsync(int id, CreateUpdateOrders order)
        {
            return await _ordersRepo.UpdatePartiallyOrderAsync(id, order);
        }
    }
}
