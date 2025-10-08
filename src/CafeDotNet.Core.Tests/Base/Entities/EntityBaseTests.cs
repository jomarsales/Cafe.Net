using CafeDotNet.Core.Base.Entities;
using FluentAssertions;

namespace CafeDotNet.Core.Tests.Base.Entities
{
    [Trait("Entity", "EntityBase")]
    public class EntityBaseTests
    {
        // Classe concreta de apoio (somente para testes)
        private class TestEntity : EntityBase
        {
            public void PublicActivate() => Activate();
            public void PublicDeactivate() => Deactivate();
            public void PublicSetUpdated() => SetUpdated();
        }

        [Fact(DisplayName = "Make sure that Creating entity should set CreatedAt automatically")]
        public void Make_sure_that_Creating_entity_should_set_CreatedAt_automatically()
        {
            // Act
            var entity = new TestEntity();

            // Assert
            entity.CreatedAt.Should().NotBe(default);
            entity.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            entity.IsActive.Should().BeFalse("default state should be inactive until explicitly activated");
            entity.UpdatedAt.Should().BeNull();
        }

        [Fact(DisplayName = "Make sure that Activate should set IsActive to true")]
        public void Make_sure_that_Activate_should_set_IsActive_to_true()
        {
            // Arrange
            var entity = new TestEntity();

            // Act
            entity.PublicActivate();

            // Assert
            entity.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = "Make sure that Deactivate should set IsActive to false")]
        public void Make_sure_that_Deactivate_should_set_IsActive_to_false()
        {
            // Arrange
            var entity = new TestEntity();
            entity.PublicActivate(); // ativar primeiro

            // Act
            entity.PublicDeactivate();

            // Assert
            entity.IsActive.Should().BeFalse();
        }

        [Fact(DisplayName = "Make sure that SetUpdated should set UpdatedAt to current time")]
        public void Make_sure_that_SetUpdated_should_set_UpdatedAt_to_current_time()
        {
            // Arrange
            var entity = new TestEntity();

            // Act
            entity.PublicSetUpdated();

            // Assert
            entity.UpdatedAt.Should().NotBeNull();
            entity.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }
    }
}