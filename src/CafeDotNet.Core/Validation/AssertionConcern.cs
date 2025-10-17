using CafeDotNet.Core.Validation.Interfaces;
using CafeDotNet.Core.Validation.Services;
using System.Text.RegularExpressions;

namespace CafeDotNet.Core.Validation;

/// <summary>
/// Classe utilitária para centralizar validações de argumentos e invariantes de domínio.
/// Inspirada nas amostras de DDD (Vaughn Vernon) e implementações públicas.
/// </summary>
public static class AssertionConcern
{
    #region Validation buffer

    private static readonly IValidationBuffer _buffer = new ValidationBuffer();

    public static bool HasErrors => _buffer.HasErrors;

    public static IReadOnlyDictionary<string, List<string>> Errors => _buffer.GetAll();

    public static void Clear() => _buffer.Clear();

    #endregion

    public static void AssertArgumentNotNull(string key, object? value, string message)
    {
        if (value is null)
            Throw(key, message);
    }

    public static void AssertArgumentNotEmpty(string key, string? value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            Throw(key, message);
    }

    public static void AssertArgumentNotEmpty<T>(string key, IEnumerable<T>? collection, string message)
    {
        if (collection is null || !collection.Any())
            Throw(key, message);
    }

    public static void AssertArgumentLength(string key, string value, int max, string message)
    {
        if (value.Length > max)
            Throw(key, message);
    }

    public static void AssertArgumentLength(string key, string value, int min, int max, string message)
    {
        var length = value?.Length ?? 0;
        if (length < min || length > max)
            Throw(key, message);
    }

    public static void AssertArgumentMinLength(string key, string value, int min, string message)
    {
        if (string.IsNullOrEmpty(value) || value.Length < min)
            Throw(key, message);
    }

    public static void AssertArgumentPositive(string key, decimal value, string message)
    {
        if (value <= 0)
            Throw(key, message);
    }

    public static void AssertArgumentPositive(string key, int value, string message)
    {
        if (value <= 0)
            Throw(key, message);
    }

    public static void AssertArgumentRange(string key, decimal value, decimal min, decimal max, string message)
    {
        if (value < min || value > max)
            Throw(key, message);
    }

    public static void AssertArgumentRange(string key, int value, int min, int max, string message)
    {
        if (value < min || value > max)
            Throw(key, message);
    }

    public static void AssertArgumentDateNotInFuture(string key, DateTime value, string message)
    {
        if (value > DateTime.Now)
            Throw(key, message);
    }

    public static void AssertArgumentDateNotInPast(string key, DateTime value, string message)
    {
        if (value < DateTime.Now)
            Throw(key, message);
    }

    public static void AssertArgumentDateRange(string key, DateTime value, DateTime min, DateTime max, string message)
    {
        if (value < min || value > max)
            Throw(key, message);
    }

    public static void AssertArgumentGuidNotEmpty(string key, Guid value, string message)
    {
        if (value == Guid.Empty)
            Throw(key, message);
    }

    public static void AssertArgumentTrue(string key, bool condition, string message)
    {
        if (!condition)
            Throw(key, message);
    }

    public static void AssertArgumentFalse(string key, bool condition, string message)
    {
        if (condition)
            Throw(key, message);
    }

    public static void AssertArgumentEquals(string key, object? expected, object? actual, string message)
    {
        if (!object.Equals(expected, actual))
            Throw(key, message);
    }

    public static void AssertArgumentNotEquals(string key, object? notExpected, object? actual, string message)
    {
        if (object.Equals(notExpected, actual))
            Throw(key, message);
    }

    public static void AssertArgumentMatches(string key, string value, string pattern, string message)
    {
        if (value is null || !Regex.IsMatch(value, pattern))
            Throw(key, message);
    }

    public static void AssertArgumentEmail(string key, string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            Throw(key, message);

        const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if (!Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
            Throw(key, message);
    }

    public static void AssertArgumentCpf(string key, string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsCpf(value))
            Throw(key, message);
    }

    public static void AssertArgumentCnpj(string key, string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsCnpj(value))
            Throw(key, message);
    }

    private static void Throw(string key, string message)
    {
        _buffer.Add(key, message);
    }

    private static bool IsCpf(string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());
        if (cpf.Length != 11) return false;
        if (cpf.Distinct().Count() == 1) return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        string digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }

    private static bool IsCnpj(string cnpj)
    {
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
        if (cnpj.Length != 14) return false;
        if (cnpj.Distinct().Count() == 1) return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        int resto = (soma % 11);
        resto = resto < 2 ? 0 : 11 - resto;
        string digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = (soma % 11);
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }
}
