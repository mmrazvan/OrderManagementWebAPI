using AutoMapper;

using Microsoft.EntityFrameworkCore;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.Helpers;

namespace OrderManagementWebAPI.Repos.OrderLabelsRepository
{
    public class OrderLabelsRepo : IOrderLabelsRepo
    {
        private readonly OrderManagementContext _context;
        public OrderLabelsRepo(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task AddOrderLabels(int orderNumber)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (order == null)
            {
                return;
            }
            var orderLabels = DataHelpers.CreateLabels(order);
            if (orderLabels != null)
            {
                await _context.OrderLabels.AddRangeAsync(orderLabels);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteOrderLabelsAsync(int orderNumber)
        {
            var order = await _context.Orders.Where(o => o.OrderNumber == orderNumber).FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }
            var orderLabels = await GetOrderLabelsAsync(orderNumber);
            _context.OrderLabels.RemoveRange(orderLabels);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderLabels>> GetOrderLabelsAsync(int orderNumber)
        {
            return await _context.OrderLabels.Where(ol => ol.OrderNumber == orderNumber).ToListAsync();
        }
    }
}
