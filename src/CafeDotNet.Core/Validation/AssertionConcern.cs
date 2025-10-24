using System.Text.RegularExpressions;

namespace CafeDotNet.Core.Validation;

/// <summary>
/// Classe utilitária para centralizar validações de argumentos e invariantes de domínio.
/// Inspirada nas amostras de DDD (Vaughn Vernon) e implementações públicas.
/// </summary>
public static class AssertionConcern
{
    public static ValidationError? AssertArgumentNotNull(string key, object? value, string message)
    {
        return (value is null) ? Throw(key, message) : null; ;
    }

    public static ValidationError? AssertArgumentNotEmpty(string key, string? value, string message)
    {
        return (string.IsNullOrWhiteSpace(value)) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentNotEmpty<T>(string key, IEnumerable<T>? collection, string message)
    {
        return (collection is null || !collection.Any()) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentLength(string key, string value, int max, string message)
    {
        return (value.Length > max) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentLength(string key, string value, int min, int max, string message)
    {
        var length = value?.Length ?? 0;

        return (length < min || length > max) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentMinLength(string key, string value, int min, string message)
    {
        return (string.IsNullOrEmpty(value) || value.Length < min) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentPositive(string key, decimal value, string message)
    {
        return (value <= 0) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentPositive(string key, int value, string message)
    {
        return (value <= 0) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentRange(string key, decimal value, decimal min, decimal max, string message)
    {
        return (value < min || value > max) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentRange(string key, int value, int min, int max, string message)
    {
        return (value < min || value > max) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentDateNotInFuture(string key, DateTime value, string message)
    {
        return (value > DateTime.Now) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentDateNotInPast(string key, DateTime value, string message)
    {
        return (value < DateTime.Now) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentDateRange(string key, DateTime value, DateTime min, DateTime max, string message)
    {
        return (value < min || value > max) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentGuidNotEmpty(string key, Guid value, string message)
    {
        return (value == Guid.Empty) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentTrue(string key, bool condition, string message)
    {
        return (!condition) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentFalse(string key, bool condition, string message)
    {
        return (condition) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentEquals(string key, object? expected, object? actual, string message)
    {
        return (!object.Equals(expected, actual)) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentNotEquals(string key, object? notExpected, object? actual, string message)
    {
        return (object.Equals(notExpected, actual)) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentMatches(string key, string value, string pattern, string message)
    {
        return (value is null || !Regex.IsMatch(value, pattern)) ? Throw(key, message) : null;
    }

    public static ValidationError? AssertArgumentEmail(string key, string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Throw(key, message);

        const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        return (!Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase)) ? Throw(key, message) : null; ;
    }

    public static ValidationError? AssertArgumentCpf(string key, string value, string message)
    {
        return (string.IsNullOrWhiteSpace(value) || !IsCpf(value)) ? Throw(key, message) : null; ;
    }

    public static ValidationError? AssertArgumentCnpj(string key, string value, string message)
    {
        return string.IsNullOrWhiteSpace(value) || !IsCnpj(value) ? Throw(key, message) : null; ;
    }

    private static ValidationError? Throw(string key, string message)
    {
        return new ValidationError(key, message);
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
