using GuardianNewsApp.Domain.Entities;
using GuardianNewsApp.Domain.Interfaces;
using GuardianNewsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GuardianNewsApp.Infrastructure.Repositories
{
    public class SearchParamRepository : ISearchParamRepository
    {
        private readonly ApplicationDbContext _context;

        public SearchParamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SearchParam>> GetAllSearchParamsAsync()
        {
            return await _context.SearchParams.ToListAsync();
        }

        public async Task<SearchParam> GetSearchParamByIdAsync(int id)
        {
            return await _context.SearchParams.FirstOrDefaultAsync(sp => sp.Id == id);
        }

        public async Task AddSearchParamAsync(SearchParam searchParam)
        {
            if (searchParam == null || string.IsNullOrWhiteSpace(searchParam.Value))
            {
                throw new ArgumentException("Search parameter value cannot be null or empty.");
            }

            var normalizedValue = searchParam.Value.ToLower();

            var existingParam = await _context.SearchParams
                .FirstOrDefaultAsync(p => p.Value.ToLower() == normalizedValue);

            if (existingParam != null)
            {
                throw new ArgumentException("A search parameter with the same value already exists.");
            }

            await _context.SearchParams.AddAsync(searchParam);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateSearchParamAsync(SearchParam searchParam)
        {
            _context.SearchParams.Update(searchParam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSearchParamAsync(int id)
        {
            var searchParam = await _context.SearchParams.FindAsync(id);
            if (searchParam != null)
            {
                _context.SearchParams.Remove(searchParam);
                await _context.SaveChangesAsync();
            }
        }
    }
}
