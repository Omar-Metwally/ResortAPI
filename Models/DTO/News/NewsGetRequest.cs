using WebApplication5.Models.DominModels;

namespace WebApplication5.Models.DTO.News;

public record NewsGetRequest(
    Guid Id,
    string Title,
    DateTime Date,
    ICollection<NewsSection> NewsSections);