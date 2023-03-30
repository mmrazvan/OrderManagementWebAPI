using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Services.OrderLabelsService
{
    public interface IOrderLabelsService
    {
        public Task<IEnumerable<OrderLabels>> GetOrderLabelsAsync(int orderNumber);
        public Task AddOrderLabels(int orderNumber);
        public Task<bool> DeleteOrderLabelsAsync(int orderNumber);
    }
}
