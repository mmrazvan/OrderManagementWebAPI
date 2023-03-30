using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Services.OrderTracesService
{
    public interface IOrderTracesService
    {
        public Task<IEnumerable<OrderTrace>> GetOrderTracesAsync(int orderNumber);
    }
}
