using GuardianNewsApp.Domain.Entities;
using GuardianNewsApp.Domain.Interfaces;

namespace GuardianNewsApp.Application.Services
{
    public class NewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _newsRepository.GetAllNewsAsync();
        }

        public async Task<News> GetNewsByIdAsync(string id)
        {
            return await _newsRepository.GetNewsByIdAsync(id);
        }

        public async Task AddNewsAsync(News news)
        {
            await _newsRepository.AddNewsAsync(news);
        }

        public async Task UpdateNewsAsync(News news)
        {
            await _newsRepository.UpdateNewsAsync(news);
        }

        public async Task DeleteNewsAsync(string id)
        {
            await _newsRepository.DeleteNewsAsync(id);
        }
    }
}
