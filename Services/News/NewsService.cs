using WebApplication5.Data;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models.DominModels;
using ServiceErrors.News;

namespace WebApplication5.Services.Newss;

public class NewsService : INewsService
{
    private readonly DataDBContext _context;

    public NewsService(DataDBContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> AddNews(News news)
    {
                await _context.News.AddAsync(news);
                await _context.SaveChangesAsync();
                return Result.Created;
    }

    public async Task<ErrorOr<Deleted>> DeleteNews(Guid id)
    {

        var news = await _context.News.Where(x => x.Id == id).SingleOrDefaultAsync();
        if (news != null)
        {
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }
        return Errors.News.NotFound;

    }

    public async Task<ErrorOr<News>> GetNewsById(Guid id)
    {
        var news = await _context.News.Where(x => x.Id == id).SingleOrDefaultAsync();
        if (news != null) return news;

        return Errors.News.NotFound;
    }

    public async Task<ErrorOr<IEnumerable<News>>> GetNews(int start,int end)
    {
        var news = await _context.News.Skip(start).Take(end).OrderByDescending(x => x.Date).ToArrayAsync();
        return news;
    }
    public async Task<ErrorOr<Updated>> UpsertNews(News news)
    {

        if (await _context.News.AnyAsync(x => x.Id == news.Id))
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }
        return Errors.News.NotFound;
    }
}
