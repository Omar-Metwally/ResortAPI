using WebApplication5.Models.DominModels;

namespace WebApplication5.Models.DTO.News;

public class NewsUpsertRequest
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public virtual ICollection<NewsSectionCreateRequest> NewsSections { get; set; } = new List<NewsSectionCreateRequest>();
}

public class NewsSectionUpsertRequest
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public IFormFile Image { get; set; }
}