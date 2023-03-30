using AutoMapper;

using Microsoft.EntityFrameworkCore;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;

namespace OrderManagementWebAPI.Repos.LabelsRepository
{
    public class LabelsRepo : ILabelsRepo
    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;
        public LabelsRepo(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddLabelAsync(Labels label)
        {
            _context.Labels.Add(label);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteLabelAsync(int id)
        {
            var label = await GetLabelByIdAsync(id);
            if (label == null)
            {
                return false;
            }
            _context.Labels.Remove(label);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Labels> GetLabelByIdAsync(int id)
        {
            return await _context.Labels.FirstOrDefaultAsync(l => l.LabelId == id);
        }

        public async Task<Labels> GetLabelByNameAsync(string name)
        {
            return await _context.Labels.FirstOrDefaultAsync(l => l.LabelName == name);
        }


        public async Task<IEnumerable<Labels>> GetLabelsAsync()
        {
            return await _context.Labels.ToListAsync();
        }

        public async Task<CreateUpdateLabels> UpdateLabelsAsync(int id, CreateUpdateLabels label)
        {
            if (!await ExistsLabelAsync(id))
            {
                return null;
            }

            var labelUpdated = _mapper.Map<Labels>(label);
            _context.Labels.Update(labelUpdated);
            await _context.SaveChangesAsync();
            return label; 
        }

        private async Task<bool> ExistsLabelAsync(int id)
        {
            return await _context.Labels.CountAsync(l => l.LabelId == id) > 0;
        }
    }
}
