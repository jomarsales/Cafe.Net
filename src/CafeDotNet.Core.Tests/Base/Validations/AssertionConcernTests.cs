using CafeDotNet.Core.Validation;
using FluentAssertions;

namespace CafeDotNet.Core.Tests.Base.Validations;

public class AssertionConcernTests
{
    [Fact(DisplayName = "AssertArgumentNotNull retorna erro quando valor é nulo")]
    public void AssertArgumentNotNull_DeveRetornarErro_QuandoValorNulo()
    {
        var result = AssertionConcern.AssertArgumentNotNull("Campo", null, "Campo não pode ser nulo");

        result.Should().NotBeNull();
        result!.Name.Should().Be("Campo");
        result.Message.Should().Be("Campo não pode ser nulo");
    }

    [Fact(DisplayName = "AssertArgumentNotEmpty string retorna erro quando vazia")]
    public void AssertArgumentNotEmpty_String_DeveRetornarErro_QuandoVazia()
    {
        var result = AssertionConcern.AssertArgumentNotEmpty("Nome", "", "Nome é obrigatório");

        result.Should().NotBeNull();
        result!.Message.Should().Be("Nome é obrigatório");
    }

    [Fact(DisplayName = "AssertArgumentNotEmpty coleção retorna erro quando nula ou vazia")]
    public void AssertArgumentNotEmpty_Collection_DeveRetornarErro_QuandoNulaOuVazia()
    {
        var r1 = AssertionConcern.AssertArgumentNotEmpty("Itens", null!, "Itens obrigatórios");
        var r2 = AssertionConcern.AssertArgumentNotEmpty("Itens", new List<int>(), "Itens obrigatórios");

        r1.Should().NotBeNull();
        r2.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentLength retorna erro quando excede máximo")]
    public void AssertArgumentLength_DeveRetornarErro_QuandoExcedeMaximo()
    {
        var result = AssertionConcern.AssertArgumentLength("Campo", "abcde", 3, "Campo muito longo");

        result.Should().NotBeNull();
        result!.Message.Should().Be("Campo muito longo");
    }

    [Fact(DisplayName = "AssertArgumentLength retorna erro quando fora do intervalo")]
    public void AssertArgumentLength_DeveRetornarErro_QuandoForaDoIntervalo()
    {
        var r1 = AssertionConcern.AssertArgumentLength("Campo", "ab", 3, 5, "Campo fora do intervalo");
        var r2 = AssertionConcern.AssertArgumentLength("Campo2", "abcdef", 3, 5, "Campo2 fora do intervalo");

        r1.Should().NotBeNull();
        r2.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentMinLength retorna erro quando menor que mínimo")]
    public void AssertArgumentMinLength_DeveRetornarErro_QuandoMenorQueMinimo()
    {
        var result = AssertionConcern.AssertArgumentMinLength("Senha", "abc", 5, "Senha muito curta");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentPositive retorna erro quando valor <= 0")]
    public void AssertArgumentPositive_DeveRetornarErro_QuandoZeroOuNegativo()
    {
        var r1 = AssertionConcern.AssertArgumentPositive("Qtd", 0, "Deve ser positivo");
        var r2 = AssertionConcern.AssertArgumentPositive("Preco", -1m, "Deve ser positivo");

        r1.Should().NotBeNull();
        r2.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentRange retorna erro quando fora do intervalo")]
    public void AssertArgumentRange_DeveRetornarErro_QuandoForaDoIntervalo()
    {
        var r1 = AssertionConcern.AssertArgumentRange("Idade", 15, 18, 65, "Idade inválida");
        var r2 = AssertionConcern.AssertArgumentRange("Valor", 1000m, 1, 999, "Valor inválido");

        r1.Should().NotBeNull();
        r2.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentDateNotInFuture retorna erro quando data futura")]
    public void AssertArgumentDateNotInFuture_DeveRetornarErro_QuandoDataFutura()
    {
        var result = AssertionConcern.AssertArgumentDateNotInFuture("Data", DateTime.Now.AddDays(1), "Data inválida");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentDateNotInPast retorna erro quando data passada")]
    public void AssertArgumentDateNotInPast_DeveRetornarErro_QuandoDataPassada()
    {
        var result = AssertionConcern.AssertArgumentDateNotInPast("Data", DateTime.Now.AddDays(-1), "Data inválida");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentDateRange retorna erro quando fora do intervalo")]
    public void AssertArgumentDateRange_DeveRetornarErro_QuandoForaDoIntervalo()
    {
        var min = DateTime.Now.AddDays(-5);
        var max = DateTime.Now.AddDays(5);

        var r1 = AssertionConcern.AssertArgumentDateRange("Data1", DateTime.Now.AddDays(-6), min, max, "Fora do range");
        var r2 = AssertionConcern.AssertArgumentDateRange("Data2", DateTime.Now.AddDays(6), min, max, "Fora do range");

        r1.Should().NotBeNull();
        r2.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentGuidNotEmpty retorna erro quando Guid.Empty")]
    public void AssertArgumentGuidNotEmpty_DeveRetornarErro_QuandoEmpty()
    {
        var result = AssertionConcern.AssertArgumentGuidNotEmpty("Id", Guid.Empty, "Id inválido");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentTrue retorna erro quando condição falsa")]
    public void AssertArgumentTrue_DeveRetornarErro_QuandoFalso()
    {
        var result = AssertionConcern.AssertArgumentTrue("Cond", false, "Deve ser true");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentFalse retorna erro quando condição verdadeira")]
    public void AssertArgumentFalse_DeveRetornarErro_QuandoVerdadeiro()
    {
        var result = AssertionConcern.AssertArgumentFalse("Cond", true, "Deve ser false");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentEquals retorna erro quando diferentes")]
    public void AssertArgumentEquals_DeveRetornarErro_QuandoDiferentes()
    {
        var result = AssertionConcern.AssertArgumentEquals("Campo", 1, 2, "Valores diferentes");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentNotEquals retorna erro quando iguais")]
    public void AssertArgumentNotEquals_DeveRetornarErro_QuandoIguais()
    {
        var result = AssertionConcern.AssertArgumentNotEquals("Campo", 1, 1, "Valores iguais");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentMatches retorna erro quando regex falha")]
    public void AssertArgumentMatches_DeveRetornarErro_QuandoRegexFalha()
    {
        var result = AssertionConcern.AssertArgumentMatches("Campo", "abc", @"^\d+$", "Deve ser número");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentEmail retorna erro quando email inválido")]
    public void AssertArgumentEmail_DeveRetornarErro_QuandoInvalido()
    {
        var result = AssertionConcern.AssertArgumentEmail("Email", "abc@", "Email inválido");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentCpf retorna erro quando CPF inválido")]
    public void AssertArgumentCpf_DeveRetornarErro_QuandoInvalido()
    {
        var result = AssertionConcern.AssertArgumentCpf("Cpf", "12345678900", "CPF inválido");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "AssertArgumentCnpj retorna erro quando CNPJ inválido")]
    public void AssertArgumentCnpj_DeveRetornarErro_QuandoInvalido()
    {
        var result = AssertionConcern.AssertArgumentCnpj("Cnpj", "12345678000100", "CNPJ inválido");
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "Retorna null quando nenhum erro encontrado")]
    public void MetodosDevemRetornarNull_QuandoValido()
    {
        AssertionConcern.AssertArgumentNotNull("Campo", "ok", "msg").Should().BeNull();
        AssertionConcern.AssertArgumentNotEmpty("Campo", "abc", "msg").Should().BeNull();
        AssertionConcern.AssertArgumentNotEmpty("Campo", new[] { 1 }, "msg").Should().BeNull();
        AssertionConcern.AssertArgumentLength("Campo", "abc", 1, 5, "msg").Should().BeNull();
        AssertionConcern.AssertArgumentMinLength("Campo", "abcde", 3, "msg").Should().BeNull();
        AssertionConcern.AssertArgumentPositive("Campo", 10, "msg").Should().BeNull();
        AssertionConcern.AssertArgumentRange("Campo", 5, 1, 10, "msg").Should().BeNull();
        AssertionConcern.AssertArgumentGuidNotEmpty("Campo", Guid.NewGuid(), "msg").Should().BeNull();
        AssertionConcern.AssertArgumentTrue("Campo", true, "msg").Should().BeNull();
        AssertionConcern.AssertArgumentFalse("Campo", false, "msg").Should().BeNull();
        AssertionConcern.AssertArgumentEquals("Campo", 1, 1, "msg").Should().BeNull();
        AssertionConcern.AssertArgumentNotEquals("Campo", 1, 2, "msg").Should().BeNull();
        AssertionConcern.AssertArgumentMatches("Campo", "123", @"^\d+$", "msg").Should().BeNull();
        AssertionConcern.AssertArgumentEmail("Campo", "teste@dominio.com", "msg").Should().BeNull();
        AssertionConcern.AssertArgumentCpf("Campo", "52998224725", "msg").Should().BeNull();
        AssertionConcern.AssertArgumentCnpj("Campo", "11444777000161", "msg").Should().BeNull();
    }
}
