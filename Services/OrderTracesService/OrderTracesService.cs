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
        public async Task<IEnumerable<OrderTrace>> GetOrderTracesAsync(int orderNumber)
        {
            return await _orderTraceRepo.GetOrderTracesAsync(orderNumber);
        }
    }
}
