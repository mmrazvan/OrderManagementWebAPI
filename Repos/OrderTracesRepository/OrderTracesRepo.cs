using Microsoft.EntityFrameworkCore;

using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Repos.OrderTracesRepository
{
    public class OrderTracesRepo : IOrderTracesRepo
    {
        private readonly OrderManagementContext _context;

        public OrderTracesRepo(OrderManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderTrace>> GetOrderTracesAsync(int orderNumber)
        {
            return await _context.OrderTrace.Where(ot => ot.OrderNumber == orderNumber).ToListAsync();
        }
    }
}
