using CafeDotNet.Core.Users.ValueObjects;
using FluentAssertions;

namespace CafeDotNet.Core.Tests.Users.ValueObjects;

[Trait("ValueObject", "Password")]
public class PasswordTests
{
    [Fact(DisplayName = "Make sure that Create should generate hash and salt for valid password")]
    public void Make_sure_that_Create_should_generate_hash_and_salt_for_valid_password()
    {
        // Arrange
        var plainPassword = "StrongPass1";

        // Act
        var password = Password.Create(plainPassword);

        // Assert
        password.Hash.Should().NotBeNullOrWhiteSpace();
        password.Salt.Should().NotBeNullOrWhiteSpace();
        password.Hash.Should().NotBe(password.Salt);
    }

    [Fact(DisplayName = "Make sure that Create should throw when password is empty")]
    public void Make_sure_that_Create_should_throw_when_password_is_empty()
    {
        // Arrange
        var plainPassword = "   ";

        // Act
        Action act = () => Password.Create(plainPassword);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Senha não pode ser vazia.*");
    }

    [Fact(DisplayName = "Make sure that Create should throw when password is weak")]
    public void Make_sure_that_Create_should_throw_when_password_is_weak()
    {
        // Arrange
        var plainPassword = "weakpass";

        // Act
        Action act = () => Password.Create(plainPassword);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*requisitos mínimos de segurança*");
    }

    [Fact(DisplayName = "Make sure that FromHash should create password from existing hash and salt")]
    public void Make_sure_that_FromHash_should_create_password_from_existing_hash_and_salt()
    {
        // Arrange
        var hash = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        var salt = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        // Act
        var password = Password.FromHash(hash, salt);

        // Assert
        password.Hash.Should().Be(hash);
        password.Salt.Should().Be(salt);
    }

    [Fact(DisplayName = "Make sure that FromHash should throw when hash or salt is invalid")]
    public void Make_sure_that_FromHash_should_throw_when_hash_or_salt_is_invalid()
    {
        // Arrange
        var hash = " ";
        var salt = " ";

        // Act
        Action act = () => Password.FromHash(hash, salt);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Hash e Salt devem ser informados*");
    }

    [Fact(DisplayName = "Make sure that Verify should return true for correct password")]
    public void Make_sure_that_Verify_should_return_true_for_correct_password()
    {
        // Arrange
        var plainPassword = "ValidPass1";
        var password = Password.Create(plainPassword);

        // Act
        var result = password.Verify(plainPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact(DisplayName = "Make sure that Verify should return false for incorrect password")]
    public void Make_sure_that_Verify_should_return_false_for_incorrect_password()
    {
        // Arrange
        var password = Password.Create("StrongPass9");

        // Act
        var result = password.Verify("WrongPass9");

        // Assert
        result.Should().BeFalse();
    }

    [Fact(DisplayName = "Make sure that equals should return true for same hash and salt")]
    public void Make_sure_that_equals_should_return_true_for_same_hash_and_salt()
    {
        // Arrange
        var hash = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        var salt = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        var p1 = Password.FromHash(hash, salt);
        var p2 = Password.FromHash(hash, salt);

        // Act
        var result = p1.Equals(p2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact(DisplayName = "Make sure that equals should return false for different hash or salt")]
    public void Make_sure_that_equals_should_return_false_for_different_hash_or_salt()
    {
        // Arrange
        var p1 = Password.FromHash("hash1", "salt1");
        var p2 = Password.FromHash("hash2", "salt2");

        // Act
        var result = p1.Equals(p2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact(DisplayName = "Make sure that GetHashCode should be consistent for same values")]
    public void Make_sure_that_GetHashCode_should_be_consistent_for_same_values()
    {
        // Arrange
        var hash = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        var salt = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        var p1 = Password.FromHash(hash, salt);
        var p2 = Password.FromHash(hash, salt);

        // Act
        var hashCode1 = p1.GetHashCode();
        var hashCode2 = p2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }
}
