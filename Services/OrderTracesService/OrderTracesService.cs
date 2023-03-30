using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.Repos.OrderTracesRepository;

namespace OrderManagementWebAPI.Services.OrderTracesService
{
    public class OrderTracesService : IOrderTracesService
    {
        private readonly IOrderTracesRepo _orderTraceRepo;
        public OrderTracesService(IOrderTracesRepo orderTracesRepo)
        {
            _orderTraceRepo = orderTracesRepo;
        }

        public async Task AddOrderTracesAsync(int orderNumber)
        {
            await _orderTraceRepo.AddOrderTracesAsync(orderNumber);
        }

        public async Task<bool> DeleteOrderTracesAsync(int orderNumber)
        {
            return await _orderTraceRepo.DeleteOrderTracesAsync(orderNumber);
        }

        public async Task<IEnumerable<OrderTrace>> GetOrderTracesAsync(int orderNumber)
        {
            return await _orderTraceRepo.GetOrderTracesAsync(orderNumber);
        }
    }
}
