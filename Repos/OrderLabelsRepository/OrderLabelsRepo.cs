using AutoMapper;

using Microsoft.EntityFrameworkCore;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.Helpers;

namespace OrderManagementWebAPI.Repos.OrderLabelsRepository
{
    public class OrderLabelsRepo : IOrderLabelsRepo
    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;
        public OrderLabelsRepo(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddOrderLabels(int orderNumber)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            var orderLabels = DataHelpers.CreateLabels(order);
            if (orderLabels != null)
            {
                await _context.OrderLabels.AddRangeAsync(orderLabels);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderLabels>> GetOrderLabelsAsync(int orderNumber)
        {
            return await _context.OrderLabels.Where(ol => ol.OrderNumber == orderNumber).ToListAsync();
        }
    }
}
