using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.Repos.OrderLabelsRepository;

namespace OrderManagementWebAPI.Services.OrderLabelsService
{
    public class OrderLabelsService : IOrderLabelsService
    {
        private readonly IOrderLabelsRepo _orderLabelsRepo;
        public OrderLabelsService(IOrderLabelsRepo orderLabelsRepo)
        {
            _orderLabelsRepo = orderLabelsRepo;
        }

        public async Task AddOrderLabels(int orderNumber)
        {
            await _orderLabelsRepo.AddOrderLabels(orderNumber);
        }

        public async Task<bool> DeleteOrderLabelsAsync(int orderNumber)
        {
            return await _orderLabelsRepo.DeleteOrderLabelsAsync(orderNumber);
        }

        public async Task<IEnumerable<OrderLabels>> GetOrderLabelsAsync(int orderNumber)
        {
            return await _orderLabelsRepo.GetOrderLabelsAsync(orderNumber);
        }
    }
}
