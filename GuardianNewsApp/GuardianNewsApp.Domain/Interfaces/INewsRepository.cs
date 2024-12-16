using GuardianNewsApp.Domain.Entities;

namespace GuardianNewsApp.Domain.Interfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News> GetNewsByIdAsync(string id);
        Task AddNewsAsync(News news);
        Task UpdateNewsAsync(News news);
        Task DeleteNewsAsync(string id);
    }
}
