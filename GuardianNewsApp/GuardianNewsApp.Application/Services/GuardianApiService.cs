using Newtonsoft.Json;
using GuardianNewsApp.Domain.Interfaces;
using GuardianNewsApp.Domain.Entities;
using GuardianNewsApp.Application.Interfaces;

namespace GuardianNewsApp.Application.Services
{
    public class GuardianApiService : IGuardianApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ISearchParamRepository _searchParamRepository;

        public GuardianApiService(HttpClient httpClient, ISearchParamRepository searchParamRepository)
        {
            _httpClient = httpClient;
            _searchParamRepository = searchParamRepository;
        }

        public string BuildUrl(string? search)
        {
            var apiKey = "098fcb75-39f8-4ab0-a780-0d389afa1a38";
            var urlBase = "https://content.guardianapis.com/search";
            var builder = new UriBuilder(urlBase)
            {
                Query = $"q={search}&api-key={apiKey}"
            };
            return builder.ToString();
        }

        public async Task<List<News>> FetchNewsBySearchInput(string searchInput)
        {
            var url = BuildUrl(searchInput);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var newsResponse = JsonConvert.DeserializeObject<GuardianNewsApiResponse>(content);

            if (newsResponse?.Response?.Results == null || !newsResponse.Response.Results.Any())
            {
                return new List<News>();
            }

            var newsList = MapNews(newsResponse.Response.Results);
            return newsList;
        }

        public async Task<List<News>> FetchNewsFromGuardianApiAsync()
        {
            try
            {
                var searchParams = await _searchParamRepository.GetAllSearchParamsAsync();
                var paramDict = searchParams.ToDictionary(p => p.Id, p => p.Value);

                var newsList = new List<News>();

                foreach (var param in paramDict)
                {
                    var url = BuildUrl(param.Value);
                    var response = await _httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();

                    var newsResponse = JsonConvert.DeserializeObject<GuardianNewsApiResponse>(content);

                    if (newsResponse?.Response?.Results != null && newsResponse.Response.Results.Any())
                    {
                        newsList.AddRange(MapNews(newsResponse.Response.Results));
                    }
                }

                return newsList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<News> MapNews(List<NewsArticle> articles)
        {
            return articles.Select(article => new News
            {
                Id = article.Id,
                Date = article.WebPublicationDate,
                Category = article.SectionName,
                Title = article.WebTitle,
                Link = article.WebUrl
            }).ToList();
        }
    }
}
