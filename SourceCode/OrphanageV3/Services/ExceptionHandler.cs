using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrphanageV3.Services
{
    public class ExceptionHandler : IExceptionHandler
    {
        private T HandleApiCreatedException<T>(ApiClientException apiClientException)
        {
            //Created
            if (apiClientException.StatusCode == "201")
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(apiClientException.Response);
                }
                catch
                {
                    return default(T);
                }
            }
            else
                return default(T);
        }

        private T HandleApiDublicatedException<T>(Func<int, Task<T>> getObjectFunction, ApiClientException apiClientException)
        {
            if (apiClientException.StatusCode == "409")
            {
                if (apiClientException.Response.Contains("Id"))
                {
                    int IdIndex = apiClientException.Response.IndexOf("Id") + 3;
                    string idString = apiClientException.Response.Substring(IdIndex, apiClientException.Response.Length - IdIndex - 1);
                    int id = System.Convert.ToInt32(idString);
                    var rr = getObjectFunction(id).Result;
                    return rr;
                }
                else
                {
                    return default(T);
                }
            }
            else
                return default(T);
        }

        public bool HandleApiSaveException(ApiClientException apiClientException)
        {
            //nothing changed
            if (apiClientException.StatusCode != "304")
            {
                MessageBox.Show(apiClientException.Message, "Orphanage3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        public T HandleApiPostFunctions<T>(Func<int, Task<T>> getObjectFunction, ApiClientException apiClientException)
        {
            if (apiClientException.StatusCode == "201")
            {
                return HandleApiCreatedException<T>(apiClientException);
            }
            else if (apiClientException.StatusCode == "409")
            {
                return HandleApiDublicatedException(getObjectFunction, apiClientException);
            }
            else
                return default(T);
        }
    }
}