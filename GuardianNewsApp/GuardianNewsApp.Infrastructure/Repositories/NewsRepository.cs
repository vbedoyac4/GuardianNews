using GuardianNewsApp.Domain.Entities;
using GuardianNewsApp.Domain.Interfaces;
using GuardianNewsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GuardianNewsApp.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> GetNewsByIdAsync(string id)
        {
            return await _context.News.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task AddNewsAsync(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNewsAsync(News article)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingArticle = await _context.News.FindAsync(article.Id);

                if (existingArticle != null)
                {
                    existingArticle.Category = article.Category;
                    existingArticle.Date = article.Date;
                    existingArticle.Link = article.Link;
                    existingArticle.Title = article.Title;

                    _context.News.Update(existingArticle);
                }
                else
                {
                    await AddNewsAsync(article);
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteNewsAsync(string id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
            }
        }
    }
}
