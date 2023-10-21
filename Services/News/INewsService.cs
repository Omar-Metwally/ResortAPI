using ErrorOr;
using WebApplication5.Models.DominModels;

namespace WebApplication5.Services.Newss;

public interface INewsService
{
    Task<ErrorOr<Created>> AddNews(News news);
    Task<ErrorOr<News>> GetNewsById(Guid id);
    Task<ErrorOr<IEnumerable<News>>> GetNews();
    Task<ErrorOr<Updated>> UpsertNews(News expense);
    Task<ErrorOr<Deleted>> DeleteNews(Guid id);

}
