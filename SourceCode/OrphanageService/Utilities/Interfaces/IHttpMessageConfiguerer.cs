using System.Net.Http;
using System.Threading.Tasks;

namespace OrphanageService.Utilities.Interfaces
{
    public interface IHttpMessageConfiguerer
    {
        HttpResponseMessage OK();

        HttpResponseMessage NoContent();

        HttpResponseMessage ImageContent(byte[] img);

        HttpResponseMessage PDFFileContent(byte[] pdfFile);

        Task<HttpResponseMessage> Created(int Id);

        HttpResponseMessage NothingChanged();

        HttpResponseMessage NotAcceptable();

        Task<byte[]> GetMIMIContentData(HttpRequestMessage httpRequestMessage);
    }
}