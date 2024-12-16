using GuardianNewsApp.Domain.Entities;
using GuardianNewsApp.Domain.Interfaces;

namespace GuardianNewsApp.Application.Services
{
    public class SearchParamService
    {
        private readonly ISearchParamRepository _searchParamRepository;

        public SearchParamService(ISearchParamRepository searchParamRepository)
        {
            _searchParamRepository = searchParamRepository;
        }

        public async Task<IEnumerable<SearchParam>> GetAllSearchParamsAsync()
        {
            return await _searchParamRepository.GetAllSearchParamsAsync();
        }

        public async Task<SearchParam> GetSearchParamByIdAsync(int id)
        {
            return await _searchParamRepository.GetSearchParamByIdAsync(id);
        }

        public async Task AddSearchParamAsync(SearchParam searchParam)
        {
             await _searchParamRepository.AddSearchParamAsync(searchParam);          
        }

        public async Task UpdateSearchParamAsync(SearchParam searchParam)
        {
            await _searchParamRepository.UpdateSearchParamAsync(searchParam);
        }

        public async Task DeleteSearchParamAsync(int id)
        {
            await _searchParamRepository.DeleteSearchParamAsync(id);
        }
    }
}
