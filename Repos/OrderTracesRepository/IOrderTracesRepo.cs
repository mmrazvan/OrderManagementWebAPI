using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Repos.OrderTracesRepository
{
    public interface IOrderTracesRepo
    {
        public Task<IEnumerable<OrderTrace>> GetOrderTracesAsync(int orderNumber);
        public Task AddOrderTracesAsync(int ordernumber);
    }
}
