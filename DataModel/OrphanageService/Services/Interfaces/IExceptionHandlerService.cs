using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Services.Interfaces
{
    public interface IExceptionHandlerService
    {
        HttpResponseException HandleValidationException(DbEntityValidationException dbEntityValidation);
    }
}
