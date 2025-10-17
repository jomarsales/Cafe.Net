using CafeDotNet.Core.Validation.Services;
using FluentAssertions;

namespace CafeDotNet.Core.Tests.Base.Validations.Services;

public class ValidationBufferTests
{
    private readonly ValidationBuffer _buffer;

    public ValidationBufferTests()
    {
        _buffer = new ValidationBuffer();
        _buffer.Clear();
    }

    [Fact(DisplayName = "Add deve inserir uma nova chave com a mensagem corretamente")]
    public void Add_DeveInserirUmaNovaChaveComMensagemCorretamente()
    {
        // Act
        _buffer.Add("Nome", "O nome é obrigatório.");

        // Assert
        _buffer.HasErrors.Should().BeTrue();
        _buffer.GetAll().Should().ContainKey("Nome");
        _buffer.GetAll()["Nome"].Should().ContainSingle().Which.Should().Be("O nome é obrigatório.");
    }

    [Fact(DisplayName = "Add deve adicionar múltiplas mensagens para a mesma chave")]
    public void Add_DeveAdicionarMultiplasMensagensParaMesmaChave()
    {
        // Act
        _buffer.Add("Email", "Email inválido.");
        _buffer.Add("Email", "Email é obrigatório.");

        // Assert
        _buffer.GetAll()["Email"].Should().HaveCount(2);
        _buffer.GetAll()["Email"].Should().Contain(new[] { "Email inválido.", "Email é obrigatório." });
    }

    [Fact(DisplayName = "HasErrors deve ser falso quando o buffer estiver vazio")]
    public void HasErrors_DeveSerFalsoQuandoBufferEstiverVazio()
    {
        // Assert
        _buffer.HasErrors.Should().BeFalse();
    }

    [Fact(DisplayName = "HasErrors deve ser verdadeiro quando houver mensagens adicionadas")]
    public void HasErrors_DeveSerVerdadeiroQuandoHouverMensagensAdicionadas()
    {
        // Arrange
        _buffer.Add("Senha", "Senha é obrigatória.");

        // Assert
        _buffer.HasErrors.Should().BeTrue();
    }

    [Fact(DisplayName = "Clear deve remover todas as mensagens do buffer")]
    public void Clear_DeveRemoverTodasAsMensagensDoBuffer()
    {
        // Arrange
        _buffer.Add("Campo", "Mensagem qualquer");

        // Act
        _buffer.Clear();

        // Assert
        _buffer.HasErrors.Should().BeFalse();
        _buffer.GetAll().Should().BeEmpty();
    }

    [Fact(DisplayName = "GetAll deve retornar todas as chaves e mensagens adicionadas")]
    public void GetAll_DeveRetornarTodasAsChavesEMensagensAdicionadas()
    {
        // Arrange
        _buffer.Add("Campo1", "Mensagem 1");
        _buffer.Add("Campo2", "Mensagem 2");

        // Act
        var result = _buffer.GetAll();

        // Assert
        result.Should().HaveCount(2);
        result["Campo1"].Should().Contain("Mensagem 1");
        result["Campo2"].Should().Contain("Mensagem 2");
    }

    [Fact(DisplayName = "Add deve ser thread safe em ambiente concorrente")]
    public void Add_DeveSerThreadSafeEmAmbienteConcorrente()
    {
        // Arrange
        var tasks = new List<Task>();

        // Act
        for (int i = 0; i < 50; i++)
        {
            int index = i;
            tasks.Add(Task.Run(() => _buffer.Add("CampoConcorrente", $"Mensagem {index}")));
        }

        Task.WaitAll(tasks.ToArray());

        // Assert
        var mensagens = _buffer.GetAll()["CampoConcorrente"];
        mensagens.Should().HaveCount(50);
        mensagens.Should().Contain("Mensagem 0");
        mensagens.Should().Contain("Mensagem 49");
    }
}
