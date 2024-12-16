using GuardianNewsApp.Domain.Entities;

namespace GuardianNewsApp.Domain.Interfaces
{
    public interface ISearchParamRepository
    {
        Task<IEnumerable<SearchParam>> GetAllSearchParamsAsync();
        Task<SearchParam> GetSearchParamByIdAsync(int id);
        Task AddSearchParamAsync(SearchParam searchParam);
        Task UpdateSearchParamAsync(SearchParam searchParam);
        Task DeleteSearchParamAsync(int id);
    }
}
