using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services.Interfaces
{
    public interface IExceptionHandler
    {
        bool HandleApiSaveException(ApiClientException apiClientException);

        //T HandleApiCreatedException<T>(ApiClientException apiClientException);

        //T HandleApiDublicatedException<T>(Func<int, Task<T>> getObject, ApiClientException apiClientException);

        Task<T> HandleApiPostFunctions<T>(Func<int, Task<T>> getObject, ApiClientException apiEx);

        Task<T> HandleApiPostFunctionsAndShowErrors<T>(Func<int, Task<T>> getObject, ApiClientException apiEx);
    }
}