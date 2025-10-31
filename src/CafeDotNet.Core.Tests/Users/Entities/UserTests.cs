using CafeDotNet.Core.Articles.Entities;
using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.ValueObjects;
using CafeDotNet.Core.Validation;
using FluentAssertions;

namespace CafeDotNet.Core.Tests.Users.Entities;

[Trait("Entity", "User")]
public class UserTests
{
    [Fact(DisplayName = "Make sure that Create user with valid data should initialize correctly")]
    public void Make_sure_that_Create_user_with_valid_data_should_initialize_correctly()
    {
        // Arrange
        var password = Password.Create("StrongPass1");

        // Act
        var user = new User("Jomar", password);

        // Assert
        user.Username.Should().Be("Jomar");
        user.Password.Should().Be(password);
        user.IsActive.Should().BeTrue("new users should start active");
        user.CreatedAt.Should().NotBe(default);
        user.UpdatedAt.Should().BeNull("user was just created, not updated yet");
        user.Role.Should().Be(RoleType.None);
    }

    [Fact(DisplayName = "Make sure that Create user should throw when username is empty")]
    public void Make_sure_that_Create_user_should_throw_when_username_is_empty()
    {
        // Arrange
        var password = Password.Create("StrongPass1");

        // Act
        var user = new User("   ", password);

        // Assert
        user.ValidationResult.Errors.Should().NotBeEmpty();
        user.ValidationResult.Errors.Should().Contain(x => x.Message == "Usuário não pode ser vazio.");
    }

    [Fact(DisplayName = "Make sure that Create user should throw when username is too long")]
    public void Make_sure_that_Create_user_should_throw_when_username_is_too_long()
    {
        // Arrange
        var password = Password.Create("StrongPass1");

        // Act
        var user = new User("oiuyqweroiuyqweyruioqweroiuyqweryuioqweroiuyqweryuioqweroiuyqweroiuyqweroiuyqweyruioqweroiuyqweryuioqweroiuyqweryuioqweroiuyqweroiuyqweroiuyqweyruioqweroiuyqweryuioqweroiuyqweryuioqweroiuyqwer", password);

        // Assert
        user.ValidationResult.Errors.Should().NotBeEmpty();
        user.ValidationResult.Errors.Should().Contain(x => x.Message == $"Usuário precisa conter até {User.UsernameMaxLength} caracteres.");
    }

    [Fact(DisplayName = "Make sure that Change password should update password and set updated date")]
    public void Make_sure_that_Change_password_should_update_password_and_set_updated_date()
    {
        // Arrange
        var oldPassword = Password.Create("StrongPass1");
        var newPassword = Password.Create("NewStrong1");

        var user = new User("Jomar", oldPassword);

        // Act
        user.ChangePassword(newPassword);

        // Assert
        user.Password.Should().Be(newPassword);
        user.UpdatedAt.Should().NotBeNull();
        user.UpdatedAt.Should().BeAfter(user.CreatedAt);
    }

    [Fact(DisplayName = "Make sure that Change password should throw when new password is null")]
    public void Make_sure_that_Change_password_should_throw_when_new_password_is_null()
    {
        // Arrange
        var password = Password.Create("StrongPass1");
        var user = new User("Jomar", password);

        // Act
        user.ChangePassword(null);

        // Assert
        user.ValidationResult.Errors.Should().Contain(x => x.Message == "Senha deve ser informada.");
    }

    [Fact(DisplayName = "Make sure that Protected constructor allows EF instantiation")]
    public void Make_sure_that_Protected_constructor_allows_EF_instantiation()
    {
        // Act
        var user = (User)Activator.CreateInstance(typeof(User), nonPublic: true);

        // Assert
        user.Should().NotBeNull();
    }

    [Fact(DisplayName = "Make sure that SetAsAdmin should update RoleType")]
    public void Make_sure_that_SetAsAdmin_should_update_RoleYpe()
    {
        // Arrange
        var password = Password.Create("StrongPass1");
        var user = new User("Jomar", password);

        // Act
        user.SetAsAdmin();

        // Assert
        user.Role.Should().Be(RoleType.Admin);
    }

    [Fact(DisplayName = "Make sure that SetAsVisitor should update RoleType")]
    public void Make_sure_that_SetAsVisitor_should_update_RoletYpe()
    {
        // Arrange
        var password = Password.Create("StrongPass1");
        var user = new User("Jomar", password);

        // Act
        user.SetAsVisitor();

        // Assert
        user.Role.Should().Be(RoleType.Visitor);
    }

    [Fact(DisplayName = "Make sure that RemoveRole should update RoleType")]
    public void Make_sure_that_RemoveRole_should_update_RoletYpe()
    {
        // Arrange
        var password = Password.Create("StrongPass1");
        var user = new User("Jomar", password);

        // Act
        user.RemoveRole();

        // Assert
        user.Role.Should().Be(RoleType.None);
    }
}