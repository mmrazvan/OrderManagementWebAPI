using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Repos
{
    public interface ILabelsRepo
    {
        public Task<IEnumerable<Labels>> GetLabelsAsync();

        public Task AddLabelAsync(Labels label);

        public Task<Labels> GetLabelByIdAsync(int id);
        public Task<Labels> GetLabelByNameAsync(string name);

        public Task<bool> DeleteLabelAsync(int id);

    }
}
