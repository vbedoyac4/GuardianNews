using GuardianNewsApp.Application.Interfaces;
using GuardianNewsApp.Domain.Interfaces;

namespace GuardianNewsApp.Worker.Jobs
{
    public class NewsUpsertJob
    {
        private readonly INewsRepository _newsRepository;
        private readonly IGuardianApiService _guardianApiService; 

        public NewsUpsertJob(INewsRepository newsRepository, IGuardianApiService guardianApiService)
        {
            _newsRepository = newsRepository;
            _guardianApiService = guardianApiService;
        }

        public async Task UpsertNewsDataAsync()
        {
            var news = await _guardianApiService.FetchNewsFromGuardianApiAsync();

            if (news != null && news.Any())
            {
                foreach (var article in news)
                {
                    await _newsRepository.UpdateNewsAsync(article);
                }
            }
        }
    }
}
