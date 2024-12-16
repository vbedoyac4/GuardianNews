namespace GuardianNewsApp.Application.Interfaces
{
    using GuardianNewsApp.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGuardianApiService
    {
        Task<List<News>> FetchNewsFromGuardianApiAsync();
        Task<List<News>> FetchNewsBySearchInput(string searchInput);
    }
}
