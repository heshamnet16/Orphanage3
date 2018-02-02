using System.Net.Http;

namespace OrphanageService.Utilities.Interfaces
{
    public interface IHttpResponseMessageConfiguerer
    {
        HttpResponseMessage NoContent();

        HttpResponseMessage ImageContent(byte[] img);

        HttpResponseMessage PDFFileContent(byte[] pdfFile);

        HttpResponseMessage Created();
    }
}