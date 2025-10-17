using CafeDotNet.Core.Validation;
using FluentAssertions;

namespace CafeDotNet.Core.Tests.Base.Validations;

public class AssertionConcernTests
{
    public AssertionConcernTests()
    {
        AssertionConcern.Clear(); // garante buffer limpo antes de cada teste
    }

    [Fact(DisplayName = "AssertArgumentNotNull adiciona mensagem quando valor é nulo")]
    public void AssertArgumentNotNull_adicionaMensagem_quandoValorNulo()
    {
        // Act
        AssertionConcern.AssertArgumentNotNull("Campo", null, "Campo não pode ser nulo");

        // Assert
        AssertionConcern.HasErrors.Should().BeTrue();
        AssertionConcern.Errors.Should().ContainKey("Campo");
        AssertionConcern.Errors["Campo"].First().Should().Be("Campo não pode ser nulo");
    }

    [Fact(DisplayName = "AssertArgumentNotEmpty string adiciona mensagem quando string vazia")]
    public void AssertArgumentNotEmpty_string_adicionaMensagem_quandoVazia()
    {
        AssertionConcern.AssertArgumentNotEmpty("Nome", "", "Nome é obrigatório");
        AssertionConcern.HasErrors.Should().BeTrue();
        AssertionConcern.Errors["Nome"].First().Should().Be("Nome é obrigatório");
    }

    [Fact(DisplayName = "AssertArgumentNotEmpty coleção adiciona mensagem quando coleção nula ou vazia")]
    public void AssertArgumentNotEmpty_collection_adicionaMensagem_quandoNulaOuVazia()
    {
        AssertionConcern.AssertArgumentNotEmpty("Itens", null!, "Itens obrigatórios");
        AssertionConcern.AssertArgumentNotEmpty("ItensVazia", new List<int>(), "Itens obrigatórios");

        AssertionConcern.HasErrors.Should().BeTrue();
        AssertionConcern.Errors.Should().ContainKeys("Itens", "ItensVazia");
    }

    [Fact(DisplayName = "AssertArgumentLength adiciona mensagem quando valor excede máximo")]
    public void AssertArgumentLength_adicionaMensagem_quandoExcedeMaximo()
    {
        AssertionConcern.AssertArgumentLength("Campo", "abcde", 3, "Campo muito longo");
        AssertionConcern.Errors["Campo"].First().Should().Be("Campo muito longo");
    }

    [Fact(DisplayName = "AssertArgumentLength adiciona mensagem quando valor fora do intervalo")]
    public void AssertArgumentLength_adicionaMensagem_quandoForaDoIntervalo()
    {
        AssertionConcern.AssertArgumentLength("Campo", "ab", 3, 5, "Campo fora do intervalo");
        AssertionConcern.Errors["Campo"].First().Should().Be("Campo fora do intervalo");

        AssertionConcern.AssertArgumentLength("Campo2", "abcdef", 3, 5, "Campo2 fora do intervalo");
        AssertionConcern.Errors["Campo2"].First().Should().Be("Campo2 fora do intervalo");
    }

    [Fact(DisplayName = "AssertArgumentMinLength adiciona mensagem quando valor menor que mínimo")]
    public void AssertArgumentMinLength_adicionaMensagem_quandoMenorQueMinimo()
    {
        AssertionConcern.AssertArgumentMinLength("Senha", "abc", 5, "Senha muito curta");
        AssertionConcern.Errors["Senha"].First().Should().Be("Senha muito curta");
    }

    [Fact(DisplayName = "AssertArgumentPositive adiciona mensagem quando valor <= 0")]
    public void AssertArgumentPositive_adicionaMensagem_quandoZeroOuNegativo()
    {
        AssertionConcern.AssertArgumentPositive("Quantidade", 0, "Deve ser positivo");
        AssertionConcern.AssertArgumentPositive("Preco", -10m, "Deve ser positivo");

        AssertionConcern.Errors.Should().ContainKeys("Quantidade", "Preco");
    }

    [Fact(DisplayName = "AssertArgumentRange adiciona mensagem quando valor fora do intervalo")]
    public void AssertArgumentRange_adicionaMensagem_quandoForaDoIntervalo()
    {
        AssertionConcern.AssertArgumentRange("Idade", 15, 18, 65, "Idade inválida");
        AssertionConcern.AssertArgumentRange("Valor", 1000m, 1, 999, "Valor inválido");

        AssertionConcern.Errors.Should().ContainKeys("Idade", "Valor");
    }

    [Fact(DisplayName = "AssertArgumentDateNotInFuture adiciona mensagem quando data no futuro")]
    public void AssertArgumentDateNotInFuture_adicionaMensagem_quandoDataFutura()
    {
        AssertionConcern.AssertArgumentDateNotInFuture("Data", DateTime.Now.AddDays(1), "Data inválida");
        AssertionConcern.Errors.Should().ContainKey("Data");
    }

    [Fact(DisplayName = "AssertArgumentDateNotInPast adiciona mensagem quando data no passado")]
    public void AssertArgumentDateNotInPast_adicionaMensagem_quandoDataPassada()
    {
        AssertionConcern.AssertArgumentDateNotInPast("Data", DateTime.Now.AddDays(-1), "Data inválida");
        AssertionConcern.Errors.Should().ContainKey("Data");
    }

    [Fact(DisplayName = "AssertArgumentDateRange adiciona mensagem quando fora do intervalo")]
    public void AssertArgumentDateRange_adicionaMensagem_quandoForaDoIntervalo()
    {
        var min = DateTime.Now.AddDays(-5);
        var max = DateTime.Now.AddDays(5);

        AssertionConcern.AssertArgumentDateRange("Data", DateTime.Now.AddDays(-6), min, max, "Fora do range");
        AssertionConcern.AssertArgumentDateRange("Data2", DateTime.Now.AddDays(6), min, max, "Fora do range");

        AssertionConcern.Errors.Should().ContainKeys("Data", "Data2");
    }

    [Fact(DisplayName = "AssertArgumentGuidNotEmpty adiciona mensagem quando Guid.Empty")]
    public void AssertArgumentGuidNotEmpty_adicionaMensagem_quandoEmpty()
    {
        AssertionConcern.AssertArgumentGuidNotEmpty("Id", Guid.Empty, "Id inválido");
        AssertionConcern.Errors.Should().ContainKey("Id");
    }

    [Fact(DisplayName = "AssertArgumentTrue adiciona mensagem quando condição falsa")]
    public void AssertArgumentTrue_adicionaMensagem_quandoFalso()
    {
        AssertionConcern.AssertArgumentTrue("Condição", false, "Deve ser true");
        AssertionConcern.Errors.Should().ContainKey("Condição");
    }

    [Fact(DisplayName = "AssertArgumentFalse adiciona mensagem quando condição verdadeira")]
    public void AssertArgumentFalse_adicionaMensagem_quandoVerdadeiro()
    {
        AssertionConcern.AssertArgumentFalse("Condição", true, "Deve ser false");
        AssertionConcern.Errors.Should().ContainKey("Condição");
    }

    [Fact(DisplayName = "AssertArgumentEquals adiciona mensagem quando valores diferentes")]
    public void AssertArgumentEquals_adicionaMensagem_quandoDiferentes()
    {
        AssertionConcern.AssertArgumentEquals("Campo", 1, 2, "Valores diferentes");
        AssertionConcern.Errors.Should().ContainKey("Campo");
    }

    [Fact(DisplayName = "AssertArgumentNotEquals adiciona mensagem quando valores iguais")]
    public void AssertArgumentNotEquals_adicionaMensagem_quandoIguais()
    {
        AssertionConcern.AssertArgumentNotEquals("Campo", 1, 1, "Valores iguais");
        AssertionConcern.Errors.Should().ContainKey("Campo");
    }

    [Fact(DisplayName = "AssertArgumentMatches adiciona mensagem quando regex não bate")]
    public void AssertArgumentMatches_adicionaMensagem_quandoRegexFalha()
    {
        AssertionConcern.AssertArgumentMatches("Campo", "abc", @"^\d+$", "Deve ser número");
        AssertionConcern.Errors.Should().ContainKey("Campo");
    }

    [Fact(DisplayName = "AssertArgumentEmail adiciona mensagem quando email inválido")]
    public void AssertArgumentEmail_adicionaMensagem_quandoInvalido()
    {
        AssertionConcern.AssertArgumentEmail("Email", "abc@", "Email inválido");
        AssertionConcern.Errors.Should().ContainKey("Email");
    }

    [Fact(DisplayName = "AssertArgumentCpf adiciona mensagem quando CPF inválido")]
    public void AssertArgumentCpf_adicionaMensagem_quandoInvalido()
    {
        AssertionConcern.AssertArgumentCpf("Cpf", "12345678900", "CPF inválido");
        AssertionConcern.Errors.Should().ContainKey("Cpf");
    }

    [Fact(DisplayName = "AssertArgumentCnpj adiciona mensagem quando CNPJ inválido")]
    public void AssertArgumentCnpj_adicionaMensagem_quandoInvalido()
    {
        AssertionConcern.AssertArgumentCnpj("Cnpj", "12345678000100", "CNPJ inválido");
        AssertionConcern.Errors.Should().ContainKey("Cnpj");
    }

    /* Valid Properties Scenarious */

    [Fact(DisplayName = "HasErrors deve ser falso quando nenhum erro foi adicionado")]
    public void HasErrors_DeveSerFalso_QuandoNenhumErro()
    {
        AssertionConcern.HasErrors.Should().BeFalse();
        AssertionConcern.Errors.Should().BeEmpty();
    }

    [Fact(DisplayName = "AssertArgumentNotNull não adiciona erro quando valor não nulo")]
    public void AssertArgumentNotNull_NaoAdicionaErro_QuandoValorValido()
    {
        AssertionConcern.AssertArgumentNotNull("Campo", "valor", "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentNotEmpty string não adiciona erro quando valor válido")]
    public void AssertArgumentNotEmpty_String_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentNotEmpty("Nome", "abc", "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentNotEmpty coleção não adiciona erro quando coleção não vazia")]
    public void AssertArgumentNotEmpty_Collection_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentNotEmpty("Itens", new List<int> { 1 }, "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentLength não adiciona erro quando dentro do limite")]
    public void AssertArgumentLength_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentLength("Campo", "abc", 1, 5, "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentMinLength não adiciona erro quando maior ou igual ao mínimo")]
    public void AssertArgumentMinLength_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentMinLength("Senha", "abcde", 3, "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentPositive não adiciona erro quando valor positivo")]
    public void AssertArgumentPositive_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentPositive("Quantidade", 1, "Mensagem");
        AssertionConcern.AssertArgumentPositive("Preco", 10m, "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentRange não adiciona erro quando dentro do intervalo")]
    public void AssertArgumentRange_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentRange("Idade", 30, 18, 65, "Mensagem");
        AssertionConcern.AssertArgumentRange("Valor", 50m, 1, 100, "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentGuidNotEmpty não adiciona erro quando Guid válido")]
    public void AssertArgumentGuidNotEmpty_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentGuidNotEmpty("Id", Guid.NewGuid(), "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentTrue não adiciona erro quando condição verdadeira")]
    public void AssertArgumentTrue_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentTrue("Condicao", true, "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentFalse não adiciona erro quando condição falsa")]
    public void AssertArgumentFalse_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentFalse("Condicao", false, "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentEquals não adiciona erro quando valores iguais")]
    public void AssertArgumentEquals_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentEquals("Campo", 10, 10, "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentNotEquals não adiciona erro quando valores diferentes")]
    public void AssertArgumentNotEquals_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentNotEquals("Campo", 10, 20, "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentMatches não adiciona erro quando regex bate")]
    public void AssertArgumentMatches_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentMatches("Campo", "12345", @"^\d+$", "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentEmail não adiciona erro quando email válido")]
    public void AssertArgumentEmail_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentEmail("Email", "teste@dominio.com", "Mensagem");
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentCpf não adiciona erro quando CPF válido")]
    public void AssertArgumentCpf_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentCpf("Cpf", "52998224725", "Mensagem"); // CPF válido
        AssertionConcern.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "AssertArgumentCnpj não adiciona erro quando CNPJ válido")]
    public void AssertArgumentCnpj_NaoAdicionaErro()
    {
        AssertionConcern.AssertArgumentCnpj("Cnpj", "11444777000161", "Mensagem"); // CNPJ válido
        AssertionConcern.HasErrors.Should().BeFalse();
    }
}