using CafeDotNet.Core.Articles.ValueObjects;
using CafeDotNet.Core.Base.Entities;
using System.Text;

namespace CafeDotNet.Core.Articles.Entities
{
    public class Article : EntityBase
    {
        public const int TitleMaxLength = 300;
        public const int SubtitleMaxLength = 500;
        public const int SummaryMaxLength = 200;
        public const int KeywordsMaxLength = 300;
        public const int AuthorMaxLength = 100;

        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string? Subtitle { get; private set; }
        public string HtmlContent { get; private set; }
        public string? Summary { get; private set; }
        public string? Keywords { get; private set; }
        public string Author { get; private set; }
        public DateTime? PublishedAt { get; private set; }
        public int ViewCount { get; private set; }
        public ArticleStatus Status { get; private set; }

        protected Article() { }

        public Article(string title, string? subtitle, string htmlContent, string? summary, string? keywords, string? author = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Título não informado.", nameof(title));
           
            if (string.IsNullOrWhiteSpace(htmlContent))
                throw new ArgumentException("Conteúdo do artigo não informado.", nameof(htmlContent));

            Title = title;
            Subtitle = subtitle;
            HtmlContent = htmlContent;
            Summary = summary;
            Keywords = keywords;
            Author = author ?? "Café.Net";
            Status = ArticleStatus.Draft;
            ViewCount = 0;

            Slug = GenerateSlug(title);

            Activate();
        }

        public void IncrementViewCount()
        {
            ViewCount++;
        }

        public void Publish()
        {
            Status = ArticleStatus.Published;
            PublishedAt = DateTime.UtcNow;
            
            SetUpdated();
        }

        public void SetEditing()
        {
            Status = ArticleStatus.Editing;
          
            SetUpdated();
        }

        public void Archive()
        {
            Status = ArticleStatus.Archived;
            
            SetUpdated();
        }

        public void UpdateContent(string htmlContent, string? summary = null, string? subtitle = null, string? keywords = null)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
                throw new ArgumentException("Content cannot be empty.", nameof(htmlContent));

            HtmlContent = htmlContent;
            
            if (summary != null) Summary = summary;
            if (subtitle != null) Subtitle = subtitle;
            if (keywords != null) Keywords = keywords;

            SetUpdated();
        }

        public void UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            Title = title;

            Slug = GenerateSlug(title);

            SetUpdated();
        }

        private static string GenerateSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var normalized = text.Normalize(NormalizationForm.FormD);

            var chars = normalized
                .Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                .ToArray();

            var clean = new string(chars);

            clean = clean.ToLowerInvariant()
                         .Replace(" ", "-")
                         .Replace("--", "-");

            clean = System.Text.RegularExpressions.Regex.Replace(clean, @"[^a-z0-9\-]", string.Empty);

            return clean.Trim('-');
        }
    }
}
