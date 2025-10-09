using CafeDotNet.Core.Articles.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDotNet.Infra.Data.Common.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

            builder.HasKey(a => a.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Title).IsRequired().HasMaxLength(Article.TitleMaxLength);
            builder.Property(a => a.Slug).IsRequired();
            builder.Property(a => a.Subtitle).HasMaxLength(Article.SubtitleMaxLength);
            builder.Property(a => a.HtmlContent).IsRequired();
            builder.Property(a => a.Summary).HasMaxLength(Article.SummaryMaxLength);
            builder.Property(a => a.Keywords).HasMaxLength(Article.KeywordsMaxLength);
            builder.Property(a => a.Author).IsRequired().HasMaxLength(Article.AuthorMaxLength);
            builder.Property(a => a.Status).HasConversion<int>().IsRequired();
            builder.Property(a => a.PublishedAt);
            builder.Property(a => a.ViewCount).HasDefaultValue(0);

            //Base
            builder.Property(a => a.CreatedAt).IsRequired();
            builder.Property(a => a.UpdatedAt);
            builder.Property(a => a.IsActive);

            builder.HasIndex(a => a.Slug).IsUnique();
            builder.HasIndex(a => a.Status);
            builder.HasIndex(a => a.PublishedAt);
        }
    }
}
