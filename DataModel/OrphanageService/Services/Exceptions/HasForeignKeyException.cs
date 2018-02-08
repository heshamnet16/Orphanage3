using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Services.Exceptions
{
    public class HasForeignKeyException : Exception
    {
        public HasForeignKeyException() : base(Properties.Resources.Error_ForeignKey)
        {
        }

        public HasForeignKeyException(string message) : base(message)
        {
        }

        public HasForeignKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public HasForeignKeyException(Type mainClass, Type subClass) : base(Properties.Resources.Error_ForeignKey,createException(mainClass,subClass))
        {            
        }
        private static Exception createException(Type mainClass, Type subClass)
        {
            string msg = $"{mainClass.FullName} has a foreign key on {subClass.FullName} please delete this relation first";
            return  new Exception(msg);
        }
    }
}
