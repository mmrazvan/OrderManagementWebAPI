using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;
using OrderManagementWebAPI.Helpers;
using OrderManagementWebAPI.Repos.LabelsRepository;

namespace OrderManagementWebAPI.Services.LabelsService
{
    public class LabelsService : ILabelsService
    {
        private readonly ILabelsRepo _labelsRepo;
        public LabelsService(ILabelsRepo labelsRepo)
        {
            _labelsRepo = labelsRepo;
        }

        public async Task AddLabelAsync(Labels label)
        {
            ValidationFunctions.ExceptionWhenSizeNotInRange(label.Width);
            ValidationFunctions.ExceptionWhenSizeNotInRange(label.Heigth);
            await _labelsRepo.AddLabelAsync(label);
        }

        public async Task<bool> DeleteLabelAsync(int id)
        {
            return await _labelsRepo.DeleteLabelAsync(id);
        }
        public async Task<Labels> GetLabelByIdAsync(int id)
        {
            return await _labelsRepo.GetLabelByIdAsync(id);
        }

        public async Task<Labels> GetLabelByNameAsync(string name)
        {
            return await _labelsRepo.GetLabelByNameAsync(name);
        }

        public async Task<IEnumerable<Labels>> GetLabelsAsync()
        {
            return await _labelsRepo.GetLabelsAsync();
        }

        public async Task<CreateUpdateLabels> UpdateLabelsAsync(int id, CreateUpdateLabels label)
        {
            ValidationFunctions.ExceptionWhenSizeNotInRange(label.Width);
            ValidationFunctions.ExceptionWhenSizeNotInRange(label.Heigth);
            return await _labelsRepo.UpdateLabelsAsync(id, label);
        }

        public async Task<CreateUpdateLabels> UpdatePartiallyLabelsAsync(int id, CreateUpdateLabels label)
        {
            //ValidationFunctions.ExceptionWhenSizeNotInRange(label.Width);
            //ValidationFunctions.ExceptionWhenSizeNotInRange(label.Heigth);
            return await _labelsRepo.UpdatePartiallyLabelsAsync(id, label);
        }
    }
}
