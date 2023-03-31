using AutoMapper;

using Microsoft.EntityFrameworkCore;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;
using OrderManagementWebAPI.Helpers;
using OrderManagementWebAPI.Model;

namespace OrderManagementWebAPI.Repos.OrdersRepository
{
    public class OrdersRepo : IOrdersRepo
    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;
        public OrdersRepo(OrderManagementContext context, IMapper mapper)
        {
            _context = context;            
            _mapper = mapper;
        }

        public async Task AddOrderAsync(Orders order)
        {
            order.OrderNumber = await GetOrderIdAsync();
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteOrderAsync(int orderNumber)
        {
            var order = await GetOrderByIdAsync(orderNumber);
            if (order == null)
            {
                return false;
            }
            if (order.OrderStatus == "Production")
            {
                throw new ModelValidationException(ErrorMessagesEnum.OrderInProduction);
            }
            var orderLabels = await _context.OrderLabels.Where(ol => ol.OrderNumber == orderNumber).ToListAsync();
            if (orderLabels.Any())
            {
                _context.OrderLabels.RemoveRange(orderLabels);
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Orders> GetOrderByIdAsync(int orderNumber) 
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Orders>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<CreateUpdateOrders> UpdateOrderAsync(int id, CreateUpdateOrders order)
        {
            if (!await ExistsOrderAsync(id))
            {
                return null;
            }
            var orderUpdated = _mapper.Map<Orders>(order);
            orderUpdated.OrderNumber = id;
            _context.Orders.Update(orderUpdated);
            await _context.SaveChangesAsync();
            return order;
        }

        private async Task<int> GetOrderIdAsync()
        {
            var orders = await GetOrdersAsync();
            return orders.Any() ? orders.Max(x => x.OrderNumber) + 1 : 1;
        }

        private async Task<bool> ExistsOrderAsync(int id)
        {
            return await _context.Orders.CountAsync(l => l.OrderNumber == id) > 0;
        }

        public async Task<CreateUpdateOrders> UpdatePartiallyOrderAsync(int id, CreateUpdateOrders order)
        {
            var orderFromDb = await GetOrderByIdAsync(id);
            if (orderFromDb == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(order.Client) && order.Client != orderFromDb.Client)
            {
                orderFromDb.Client = order.Client;
            }
            if (order.Completed > 0 && orderFromDb.Completed != order.Completed)
            {
                orderFromDb.Completed = order.Completed;
            }
            if (order.Quantity > 0 && orderFromDb.Quantity != order.Quantity)
            {
                orderFromDb.Quantity = order.Quantity;
            }
            if (order.PagesOnEnvelope > 0 && order.PagesOnEnvelope <=6 && orderFromDb.PagesOnEnvelope != order.PagesOnEnvelope)
            {
                orderFromDb.PagesOnEnvelope = order.PagesOnEnvelope;
            }
            if (order.DateInProduction != null && orderFromDb.DateInProduction != order.DateInProduction)
            {
                orderFromDb.DateInProduction = order.DateInProduction;
            }
            if (order.DateFinished != null && orderFromDb.DateFinished != order.DateFinished)
            {
                orderFromDb.DateFinished = order.DateFinished;
            }
            if (!string.IsNullOrEmpty(order.DocumentFormat) && orderFromDb.DocumentFormat != order.DocumentFormat)
            {
                orderFromDb.DocumentFormat = order.DocumentFormat;
            }
            if (!string.IsNullOrEmpty(order.OrderStatus) && orderFromDb.OrderStatus != order.OrderStatus)
            {
                orderFromDb.OrderStatus = order.OrderStatus;
            }
            if (!string.IsNullOrEmpty(order.EnvelopeType) && orderFromDb.EnvelopeType != order.EnvelopeType)
            {
                orderFromDb.EnvelopeType = order.EnvelopeType;
            }
            if (!string.IsNullOrEmpty(order.DocumentName) && orderFromDb.DocumentName != order.DocumentName)
            {
                orderFromDb.DocumentName = order.DocumentName;
            }
            if (!string.IsNullOrEmpty(order.LabelType) && orderFromDb.LabelType != order.LabelType)
            {
                orderFromDb.LabelType = order.LabelType;
            }

            _context.Orders.Update(orderFromDb);
            _context.SaveChanges();
            return order;
        }
    }
}
