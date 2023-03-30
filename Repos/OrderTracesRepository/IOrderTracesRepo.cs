using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;

namespace OrderManagementWebAPI.Repos.OrderTracesRepository
{
    public interface IOrderTracesRepo
    {
        public Task<IEnumerable<OrderTrace>> GetOrderTracesAsync(int orderNumber);
        public Task<OrderTrace> GetOrderTracesByIdBoxNumberAsync(string idBoxNumber);
        public Task AddOrderTracesAsync(int ordernumber);
        public Task<bool> DeleteOrderTracesAsync(int orderNumber);
        public Task<CreateUpdateOrderTraces> UpdatePartiallyOrderTracesAsync(string idBoxNumber, CreateUpdateOrderTraces orderTrace);
    }
}
