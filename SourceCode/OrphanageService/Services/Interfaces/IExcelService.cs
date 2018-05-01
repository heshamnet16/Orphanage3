using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrphanageService.Services.Interfaces
{
    public interface IExcelService
    {
        /// <summary>
        /// convert set of string data to xlsx file.
        /// </summary>
        /// <param name="data">set of columns as keys and list of string as value</param>
        /// <returns>xlsx file as byte array</returns>
        Task<byte[]> ConvertToXlsx(IDictionary<string, IList<string>> data);

        /// <summary>
        /// convert set of string data to xlsx file.
        /// </summary>
        /// <param name="data">set of columns as keys and list of string as value</param>
        /// <param name="cancellationToken"></param>
        /// <returns>xlsx file as byte array</returns>
        Task<byte[]> ConvertToXlsx(IDictionary<string, IList<string>> data, CancellationToken cancellationToken);

        /// <summary>
        /// convert set of string data to xlsx file.
        /// </summary>
        /// <param name="data">set of columns as keys and list of string as value</param>
        /// <param name="cancellationToken"></param>
        /// <returns>xlsx file as File object</returns>
        Task<FileContentResult> ConvertToXlsxFile(IDictionary<string, IList<string>> data, CancellationToken cancellationToken);

        /// <summary>
        /// convert set of string data to xlsx file.
        /// </summary>
        /// <param name="data">set of columns as keys and list of string as value</param>
        /// <param name="cancellationToken"></param>
        /// <returns>xlsx file as File object</returns>
        Task<FileContentResult> ConvertToXlsxFile(IDictionary<string, IList<string>> data);
    }
}