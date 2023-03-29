using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Services
{
    public interface ILabelsService
    {
        public Task<IEnumerable<Labels>> GetLabelsAsync();
        public Task<Labels> GetLabelByIdAsync(int id);
        public Task<Labels> GetLabelByNameAsync(string name);

        public Task AddLabelAsync(Labels label);

        public Task<bool> DeleteLabelAsync(int id);
    }
}
