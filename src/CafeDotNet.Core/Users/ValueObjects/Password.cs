using CafeDotNet.Core.Base.ValueObjects;
using CafeDotNet.Core.Validation;
using System.Security.Cryptography;
using System.Text;

namespace CafeDotNet.Core.Users.ValueObjects;

public sealed class Password : ValueObjectBase, IEquatable<Password>
{
    public const int SaltMaxLength = 50;
    public const int HashMaxLength = 256;

    private static string? _plainPassword;

    public string Hash { get; }
    public string Salt { get; }

    private Password() { }

    private Password(string hash, string salt)
    {
        Hash = hash;
        Salt = salt;

        Validate();
    }

    /// <summary>
    /// Cria uma nova senha a partir de texto puro (gera salt + hash).
    /// </summary>
    public static Password Create(string plainPassword)
    {
        _plainPassword = plainPassword ?? string.Empty;

        var salt = GenerateSalt();
        var hash = HashPassword(plainPassword, salt);

        return new Password(hash, salt);
    }

    /// <summary>
    /// Reconstrói um VO a partir de hash e salt já existentes (ex: carregados do banco).
    /// </summary>
    public static Password FromHash(string hash, string salt)
    {
        return new Password(hash, salt);
    }

    public bool Verify(string plainPassword)
    {
        ValidationResult.Add(AssertionConcern.AssertArgumentNotEmpty(nameof(Password), plainPassword, "Senha não pode ser vazia."));

        var hashToCompare = HashPassword(plainPassword, Salt);

        return hashToCompare == Hash;
    }

    private static string GenerateSalt()
    {
        var buffer = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(buffer);

        return Convert.ToBase64String(buffer);
    }

    private static string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var saltedPassword = password + salt;
        var bytes = Encoding.UTF8.GetBytes(saltedPassword);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

    private static bool IsStrongEnough(string password)
    {
        // Exemplo simplificado: pelo menos 8 chars, 1 número, 1 maiúscula
        return password?.Length >= 8 &&
               System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]") &&
               System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]");
    }

    #region Equality

    public bool Equals(Password other)
    {
        if (other is null) return false;

        return Hash == other.Hash && Salt == other.Salt;
    }

    public override bool Equals(object obj) => Equals((Password)obj);

    public override int GetHashCode() => Hash.GetHashCode() ^ Salt.GetHashCode();

    protected override void Validate()
    {
        ValidationResult.Add(AssertionConcern.AssertArgumentTrue(nameof(Password), IsStrongEnough(_plainPassword), "Senha não atende aos requisitos mínimos de segurança."));
        ValidationResult.Add(AssertionConcern.AssertArgumentNotEmpty(nameof(Hash), Hash, "Hash da senha não pode ser vazia."));
        ValidationResult.Add(AssertionConcern.AssertArgumentLength(nameof(Hash), Hash, HashMaxLength, $"Hash da senha precisa conter {HashMaxLength} caracteres"));

        ValidationResult.Add(AssertionConcern.AssertArgumentNotEmpty(nameof(Salt), Salt, "Salt da senha não pode ser vazia."));
        ValidationResult.Add(AssertionConcern.AssertArgumentLength(nameof(Salt), Salt, HashMaxLength, $"Salt da senha precisa conter {HashMaxLength} caracteres"));
    }

    #endregion
}