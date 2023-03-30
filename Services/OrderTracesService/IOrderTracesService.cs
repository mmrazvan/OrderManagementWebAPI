﻿using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Services.OrderTracesService
{
    public interface IOrderTracesService
    {
        public Task<IEnumerable<OrderTrace>> GetOrderTracesAsync(int orderNumber);
        public Task AddOrderTracesAsync(int orderNumber);
        public Task<bool> DeleteOrderTracesAsync(int orderNumber);
    }
}
