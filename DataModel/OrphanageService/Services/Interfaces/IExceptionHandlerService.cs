using System.Data.Entity.Validation;
using System.Web.Http;

namespace OrphanageService.Services.Interfaces
{
    public interface IExceptionHandlerService
    {
        HttpResponseException HandleValidationException(DbEntityValidationException dbEntityValidation);
    }
}