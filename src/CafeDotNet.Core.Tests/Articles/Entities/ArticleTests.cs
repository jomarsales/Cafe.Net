using CafeDotNet.Core.Articles.Entities;
using CafeDotNet.Core.Articles.ValueObjects;
using CafeDotNet.Core.Validation;
using FluentAssertions;

namespace CafeDotNet.Core.Tests.Articles.Entities;

[Trait("Entity", "Article")]
public class ArticleTests
{
    [Fact(DisplayName = "Make sure that creating article with valid data initializes correctly")]
    public void Make_sure_that_creating_article_with_valid_data_initializes_correctly()
    {
        // Arrange
        var title = "Meu Primeiro Artigo";
        var content = "<p>Conteúdo de teste</p>";
        var author = "Jomar Sales";

        // Act
        var article = new Article(title, "Subtítulo", content, "Resumo", "programação, .net", author);

        // Assert
        article.Title.Should().Be(title);
        article.Subtitle.Should().Be("Subtítulo");
        article.HtmlContent.Should().Be(content);
        article.Summary.Should().Be("Resumo");
        article.Keywords.Should().Be("programação, .net");
        article.Author.Should().Be(author);
        article.Status.Should().Be(ArticleStatus.Draft);
        article.ViewCount.Should().Be(0);
        article.Slug.Should().Be("meu-primeiro-artigo");
        article.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = "Make sure that creating article without title throws exception")]
    public void Make_sure_that_creating_article_without_title_throws_exception()
    {
        // Arrange
        var content = "<p>Conteúdo</p>";

        // Act
        var article = new Article("", "Sub", content, "Resumo", "chave");

        // Assert
        article.ValidationResult.Errors.Should().NotBeEmpty();
        article.ValidationResult.Errors.Should().Contain(x => x.Message == "Título não pode ser vazio.");
    }

    [Fact(DisplayName = "Make sure that creating article without content throws exception")]
    public void Make_sure_that_creating_article_without_content_throws_exception()
    {
        // Arrange
        var title = "Artigo sem conteúdo";

        // Act
        var article = new Article(title, "Sub", "", "Resumo", "chave");

        // Assert
        article.ValidationResult.Errors.Should().NotBeEmpty();
        article.ValidationResult.Errors.Should().Contain(x => x.Message == "Conteúdo não pode ser vazio.");
    }

    [Fact(DisplayName = "Make sure that incrementing view count increases counter")]
    public void Make_sure_that_incrementing_view_count_increases_counter()
    {
        // Arrange
        var article = new Article("Título", null, "<p>html</p>", null, null);

        // Act
        article.IncrementViewCount();
        article.IncrementViewCount();

        // Assert
        article.ViewCount.Should().Be(2);
    }

    [Fact(DisplayName = "Make sure that publishing article sets status and date")]
    public void Make_sure_that_publishing_article_sets_status_and_date()
    {
        // Arrange
        var article = new Article("Título", null, "<p>html</p>", null, null);

        // Act
        article.Publish();

        // Assert
        article.Status.Should().Be(ArticleStatus.Published);
        article.PublishedAt.Should().NotBeNull();
        article.UpdatedAt.Should().NotBeNull();
    }

    [Fact(DisplayName = "Make sure that setting editing status updates fields")]
    public void Make_sure_that_setting_editing_status_updates_fields()
    {
        // Arrange
        var article = new Article("Título", null, "<p>html</p>", null, null);

        // Act
        article.SetEditing();

        // Assert
        article.Status.Should().Be(ArticleStatus.Editing);
        article.UpdatedAt.Should().NotBeNull();
    }

    [Fact(DisplayName = "Make sure that archiving article updates status and date")]
    public void Make_sure_that_archiving_article_updates_status_and_date()
    {
        // Arrange
        var article = new Article("Título", null, "<p>html</p>", null, null);

        // Act
        article.Archive();

        // Assert
        article.Status.Should().Be(ArticleStatus.Archived);
        article.UpdatedAt.Should().NotBeNull();
    }

    [Fact(DisplayName = "Make sure that updating content updates fields and date")]
    public void Make_sure_that_updating_content_updates_fields_and_date()
    {
        // Arrange
        var article = new Article("Título", "Sub", "<p>Conteúdo</p>", "Resumo", "tag");

        // Act
        article.UpdateContent("<p>novo</p>", "Resumo novo", "Novo subtítulo", "nova, tag");

        // Assert
        article.HtmlContent.Should().Be("<p>novo</p>");
        article.Summary.Should().Be("Resumo novo");
        article.Subtitle.Should().Be("Novo subtítulo");
        article.Keywords.Should().Be("nova, tag");
        article.UpdatedAt.Should().NotBeNull();
    }

    [Fact(DisplayName = "Make sure that updating with empty content throws exception")]
    public void Make_sure_that_updating_with_empty_content_throws_exception()
    {
        // Arrange
        var article = new Article("Título", null, "<p>html</p>", null, null);

        // Act
        article.UpdateContent("");

        // Assert
        article.ValidationResult.Errors.Should().NotBeEmpty();
        article.ValidationResult.Errors.Should().Contain(x => x.Message == "Conteúdo não pode ser vazio.");
    }

    [Fact(DisplayName = "Make sure that updating title changes slug and sets date")]
    public void Make_sure_that_updating_title_changes_slug_and_sets_date()
    {
        // Arrange
        var article = new Article("Título Antigo", null, "<p>html</p>", null, null);

        // Act
        article.UpdateTitle("Novo Título Legal");

        // Assert
        article.Title.Should().Be("Novo Título Legal");
        article.Slug.Should().Be("novo-titulo-legal");
        article.UpdatedAt.Should().NotBeNull();
    }

    [Fact(DisplayName = "Make sure that updating title with empty value throws exception")]
    public void Make_sure_that_updating_title_with_empty_value_throws_exception()
    {
        // Arrange
        var article = new Article("Título", null, "<p>html</p>", null, null);

        // Act
        article.UpdateTitle("");

        // Assert
        article.ValidationResult.Errors.Should().NotBeEmpty();
        article.ValidationResult.Errors.Should().Contain(x => x.Message == "Título não pode ser vazio.");
    }
}