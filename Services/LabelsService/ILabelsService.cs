using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;

namespace OrderManagementWebAPI.Services.LabelsService
{
    public interface ILabelsService
    {
        public Task<IEnumerable<Labels>> GetLabelsAsync();
        public Task<Labels> GetLabelByIdAsync(int id);
        public Task<Labels> GetLabelByNameAsync(string name);
        public Task AddLabelAsync(Labels label);
        public Task<bool> DeleteLabelAsync(int id);
        public Task<CreateUpdateLabels> UpdateLabelsAsync(int id, CreateUpdateLabels label);
        public Task<CreateUpdateLabels> UpdatePartiallyLabelsAsync(int id, CreateUpdateLabels label);
    }
}
