using System.Net.Http;
using System.Threading.Tasks;

namespace OrphanageService.Utilities.Interfaces
{
    public interface IHttpMessageConfiguerer
    {
        HttpResponseMessage NoContent();

        HttpResponseMessage ImageContent(byte[] img);

        HttpResponseMessage PDFFileContent(byte[] pdfFile);

        HttpResponseMessage Created();

        HttpResponseMessage NotAcceptable();

        Task<byte[]> GetMIMIContentData(HttpRequestMessage httpRequestMessage);
    }
}