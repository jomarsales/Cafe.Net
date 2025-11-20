using CafeDotNet.Core.Gallery.DTOs;

namespace CafeDotNet.Core.Gallery.Interfaces
{
    /// <summary>
    /// Define o contrato para serviços de armazenamento de imagens.
    /// Permite abstrair o meio físico (disco, CDN, blob storage etc.).
    /// </summary>
    public interface IImageStorage
    {
        /// <summary>
        /// Salva uma imagem no armazenamento físico e retorna informações sobre o arquivo salvo.
        /// </summary>
        /// <param name="fileName">Nome original ou gerado do arquivo.</param>
        /// <param name="content">Conteúdo binário do arquivo.</param>
        /// <param name="contentType">Tipo MIME do arquivo (ex: image/jpeg).</param>
        /// <returns>Informações sobre o arquivo salvo (nome físico e URL de acesso público).</returns>
        Task<ImageStorageResult?> SaveAsync(string fileName, Stream content, string contentType);

        /// <summary>
        /// Remove uma imagem do armazenamento físico.
        /// </summary>
        /// <param name="fileName">Nome físico do arquivo salvo.</param>
        /// <returns>True se a exclusão foi bem-sucedida.</returns>
        Task<bool> DeleteAsync(string fileName);
    }
}
