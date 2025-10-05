namespace CafeDotNet.Core.Articles.ValueObjects
{
    public class ArticleListItemDto
    {
        public int Id { get; }
        public string Title { get; }
        public string? Subtitle { get; }
        public string Autor { get; }
        public DateTime PublishDate { get; }

        public ArticleListItemDto(int id, string title, string? subtitle)
        {
            Id = id;
            Title = title;
            Subtitle = subtitle;
            Autor = "Cafe.Net";
            PublishDate = DateTime.UtcNow;
        }
    }
}
