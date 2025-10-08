using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.ValueObjects;
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
    }

    [Fact(DisplayName = "Make sure that Create user should throw when username is empty")]
    public void Make_sure_that_Create_user_should_throw_when_username_is_empty()
    {
        // Arrange
        var password = Password.Create("StrongPass1");

        // Act
        Action act = () => new User("   ", password);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Username não pode ser vazio*");
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
        Action act = () => user.ChangePassword(null);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("newPassword");
    }

    [Fact(DisplayName = "Make sure that Protected constructor allows EF instantiation")]
    public void Make_sure_that_Protected_constructor_allows_EF_instantiation()
    {
        // Act
        var user = (User)Activator.CreateInstance(typeof(User), nonPublic: true);

        // Assert
        user.Should().NotBeNull();
    }
}