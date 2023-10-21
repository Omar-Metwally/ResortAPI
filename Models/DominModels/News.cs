
using ErrorOr;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebApplication5.Models.DTO.Bill;
using WebApplication5.Models.DTO.News;


namespace WebApplication5.Models.DominModels;

public class News
{

    private static readonly IWebHostEnvironment? _webHostEnvironment;

    public Guid Id { get; set; }

    public string Title { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<NewsSection> NewsSections { get; set; } = new List<NewsSection>();

    public News() {}

    private News(
    Guid Id,
    string Title,
    DateTime Date,
    ICollection<NewsSection> NewsSections)
    {
        this.Id = Id;
        this.Title = Title;
        this.Date = Date;
        this.NewsSections = NewsSections;
    }

    public static ErrorOr<News> Create(
    string Title,
    DateTime Date,
    ICollection<NewsSection> NewsSections,
    Guid? Id = null
    )
    {
        List<Error> errors = new();

        /*if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Bill.InvalidName);
        }

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Bill.InvalidDescription);
        }*/

        if (errors.Count > 0)
        {
            return errors;
        }

        return new News
            (
            Id ?? Guid.NewGuid(),
            Title,
            Date,
            NewsSections
            );
    }

    public static ErrorOr<News> From(NewsCreateRequest request)
    {
        return Create(
            request.Title,
            DateTime.Now,
            SaveImages(request.NewsSections),
            null);
    }

    public static ErrorOr<News> From(Guid id, NewsUpsertRequest request)
    {
        return Create(
            request.Title,
            DateTime.MinValue,
            SaveImages(request.NewsSections),
            id);
    }
    public static List<NewsSection> SaveImages(ICollection<NewsSectionCreateRequest> request)
    {
        var NewsSections = new List<NewsSection>();
        foreach (var NewsSection in request)
        {
            var ImageId = Guid.NewGuid();
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploads = Path.Combine(wwwRootPath, "Images", "News");
            var extension = Path.GetExtension(NewsSection.Image.FileName);
            var filename = $"{ImageId}{extension}";

            var filePath = Path.Combine(uploads, filename);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                NewsSection.Image.CopyTo(fileStream);
            }
            NewsSections.Add(new NewsSection { Description = NewsSection.Description, image = @"\Images\News\" + filename, Id = ImageId });
        }
        return NewsSections;

    }

}
public class NewsSection
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public string image { get; set; }

    public Guid NewsId { get; set; }
    [JsonIgnore]
    public virtual News News { get; set; } = null!;
}