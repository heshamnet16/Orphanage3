using OrphanageV3.Services.Interfaces;
using System;
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

        private async Task<T> HandleApiDublicatedException<T>(Func<int, Task<T>> getObjectFunction, ApiClientException apiClientException)
        {
            if (apiClientException.StatusCode == "409")
            {
                if (apiClientException.Response.Contains("Id"))
                {
                    int id = getIdfromDublicatedMessage(apiClientException.Response);
                    var rr = await getObjectFunction(id);
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

        private int getIdfromDublicatedMessage(string message)
        {
            int IdIndex = message.IndexOf("Id") + 3;
            string idString = message.Substring(IdIndex, message.Length - IdIndex);
            return System.Convert.ToInt32(idString);
        }

        public bool HandleApiSaveException(ApiClientException apiClientException)
        {
            //nothing changed
            if (apiClientException.StatusCode != "304")
            {
                MessageBox.Show(apiClientException.Message, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<T> HandleApiPostFunctions<T>(Func<int, Task<T>> getObjectFunction, ApiClientException apiClientException)
        {
            if (apiClientException.StatusCode == "201")
            {
                return HandleApiCreatedException<T>(apiClientException);
            }
            else if (apiClientException.StatusCode == "409")
            {
                return await HandleApiDublicatedException(getObjectFunction, apiClientException);
            }
            else
                return default(T);
        }

        public async Task<T> HandleApiPostFunctionsAndShowErrors<T>(Func<int, Task<T>> getObject, ApiClientException apiEx)
        {
            if (apiEx.StatusCode == "201")
            {
                var retObject = HandleApiCreatedException<T>(apiEx);
                return retObject;
            }
            else if (apiEx.StatusCode == "409")
            {
                dynamic retObject = await HandleApiDublicatedException(getObject, apiEx);
                if (retObject != default(T))
                {
                    int id = getIdfromDublicatedMessage(apiEx.Response);
                    string errorMessage = Properties.Resources.ErrorMessageDublicated + " " + id;
                    MessageBox.Show(errorMessage, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return default(T);
            }
            else
                return default(T);
        }
    }
}