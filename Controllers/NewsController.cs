using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Models.DominModels;
using WebApplication5.Models.DTO.News;
using WebApplication5.Services.Newss;

namespace WebApplication5.Controllers;

public class NewsController : ApiController
{
    private readonly INewsService _news;

    private readonly DataDBContext _context;

    public NewsController(DataDBContext context, INewsService news)
    {
        _context = context;

        _news = news;
    }

    [HttpPost("UploadImage")]
    public  IActionResult AddNews(IFormFile file)
    {
        return Created("Sdf",145);
    }

    [HttpPost]
    public async Task<IActionResult> AddNews(NewsCreateRequest request)
    {

        ErrorOr<News> requestToNewstResult = News.From(request);

        if (requestToNewstResult.IsError)
        {
            return Problem(requestToNewstResult.Errors);
        }

        var news = requestToNewstResult.Value;
        ErrorOr<Created> createNewsResult = await _news.AddNews(news);

        return createNewsResult.Match(
            created => CreatedAtGetNews(news),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetNewsById(Guid id)
    {
        ErrorOr<News> getNewsResult = await _news.GetNewsById(id);

        return getNewsResult.Match(
            news => Ok(MapNewsResponse(news)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetNews(int start, int end)
    {
        ErrorOr<IEnumerable<News>> getNewsResult = await _news.GetNews(start, end);

        return getNewsResult.Match(
            news => Ok(MapNewsResponse(news)),
            errors => Problem(errors));

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpsertNews(Guid id, NewsUpsertRequest request)
    {
        ErrorOr<News> requestToNewsResult = News.From(id, request);

        if (requestToNewsResult.IsError)
        {
            return Problem(requestToNewsResult.Errors);
        }

        var news = requestToNewsResult.Value;
        ErrorOr<Updated> upsertNewsResult = await _news.UpsertNews(news);


        return requestToNewsResult.Match(
            created => NoContent(),
            errors => Problem(errors));

    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteNews(Guid id)
    {
        ErrorOr<Deleted> deleteNewsResult = await _news.DeleteNews(id);

        return deleteNewsResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }
    private static NewsGetRequest MapNewsResponse(News news)
    {
        return new NewsGetRequest(
            news.Id,
            news.Title,
            news.Date,
            news.NewsSections);
    }
    private static IEnumerable<NewsGetRequest> MapNewsResponse(IEnumerable<News> News)
    {
        var NewsGetRequest = new List<NewsGetRequest>();
        foreach (var news in News)
        {
            NewsGetRequest.Add(new NewsGetRequest(
            news.Id,
            news.Title,
            news.Date,
            news.NewsSections));
        }
        return NewsGetRequest;
    }
    private CreatedAtActionResult CreatedAtGetNews(News news)
    {
        return CreatedAtAction(
            actionName: nameof(GetNews),
            routeValues: new { id = news.Id },
            value: MapNewsResponse(news));
    }

}
