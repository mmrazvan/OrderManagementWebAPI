using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Repos.OrderLabelsRepository
{
    public interface IOrderLabelsRepo
    {
        public Task<IEnumerable<OrderLabels>> GetOrderLabelsAsync(int orderNumber);
        public Task AddOrderLabels(int orderNumber);
    }
}
