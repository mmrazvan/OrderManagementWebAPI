using Microsoft.AspNetCore.Mvc;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.Helpers;
using OrderManagementWebAPI.Repos;

namespace OrderManagementWebAPI.Services
{
    public class LabelsService : ILabelsService
    {
        private readonly ILabelsRepo _repo;
        public LabelsService(ILabelsRepo repo)
        {
            _repo = repo;
        }

        public async Task AddLabelAsync(Labels label) 
        {
            ValidationFunctions.ExceptionWhenSizeNotInRange(label.Width);
            ValidationFunctions.ExceptionWhenSizeNotInRange(label.Heigth);
            await _repo.AddLabelAsync(label);
        }

        public async Task<bool> DeleteLabelAsync(int id) 
        {
            return await _repo.DeleteLabelAsync(id);
        }
        public async Task<Labels> GetLabelByIdAsync(int id)
        {
            return await _repo.GetLabelByIdAsync(id);
        }

        public async Task<Labels> GetLabelByNameAsync(string name) 
        {
            return await _repo.GetLabelByNameAsync(name);
        }

        public async Task<IEnumerable<Labels>> GetLabelsAsync() 
        {
            return await _repo.GetLabelsAsync();
        }
    }
}
