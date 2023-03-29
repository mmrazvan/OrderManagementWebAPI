using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using OrderManagementWebAPI.DTOs;

namespace OrderManagementWebAPI.Repos
{
    public class LabelsRepo : ILabelsRepo
    {
        private readonly OrderManagementContext _context;
        public LabelsRepo(OrderManagementContext context)
        {
            _context = context;
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

    }
}
