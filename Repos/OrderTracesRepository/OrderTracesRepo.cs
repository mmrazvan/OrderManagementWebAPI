using Microsoft.EntityFrameworkCore;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;
using OrderManagementWebAPI.Helpers;

namespace OrderManagementWebAPI.Repos.OrderTracesRepository
{
    public class OrderTracesRepo : IOrderTracesRepo
    {
        private readonly OrderManagementContext _context;

        public OrderTracesRepo(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task AddOrderTracesAsync(int ordernumber)
        {
            var orderLabels = await _context.OrderLabels.Where(ol => ol.OrderNumber == ordernumber).ToListAsync();
            var orderTraces = DataHelpers.CreateTraces(orderLabels);
            await _context.OrderTrace.AddRangeAsync(orderTraces);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteOrderTracesAsync(int orderNumber)
        {
            var order = await _context.Orders.Where(o => o.OrderNumber == orderNumber).ToListAsync();
            if (order == null)
            {
                return false;
            }
            var orderTraces = await _context.OrderTrace.Where(ot => ot.OrderNumber == orderNumber).ToListAsync();
            if (orderTraces == null || !orderTraces.Any()) return false;
            _context.OrderTrace.RemoveRange(orderTraces);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderTrace>> GetOrderTracesAsync(int orderNumber)
        {
            return await _context.OrderTrace.Where(ot => ot.OrderNumber == orderNumber).ToListAsync();
        }

        public async Task<OrderTrace> GetOrderTracesByIdBoxNumberAsync(string idBoxNumber)
        {
            return await _context.OrderTrace.FirstOrDefaultAsync(ot => ot.IdBoxNumber == idBoxNumber);
        }

        public async Task<CreateUpdateOrderTraces> UpdatePartiallyOrderTracesAsync(string idBoxNumber, CreateUpdateOrderTraces orderTrace)
        {
            var orderTraceFromDb = await GetOrderTracesByIdBoxNumberAsync(idBoxNumber);
            if (orderTraceFromDb == null)
            {
                return null;
            }

            if (orderTraceFromDb.MachineId == null)
            {
                orderTraceFromDb.MachineId = orderTrace.MachineId;
                orderTraceFromDb.DateOut = DateTime.Now;
            }
            else
                return null;         

            _context.OrderTrace.Update(orderTraceFromDb);
            await _context.SaveChangesAsync();
            return orderTrace;
        }
    }
}
