using System;

namespace OrphanageService.Services.Exceptions
{
    public class DuplicatedObjectException : Exception
    {
        public DuplicatedObjectException() : base(Properties.Resources.Error_ForeignKey)
        {
        }

        public DuplicatedObjectException(string message) : base(message)
        {
        }

        public DuplicatedObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicatedObjectException(Type mainClass, Type foundedClass, int foundedId) : base(Properties.Resources.Error_DuplicatedObject, createException(mainClass, foundedClass, foundedId))
        {
        }

        private static Exception createException(Type mainClass, Type foundedClass, int foundedID)
        {
            string msg = $"{mainClass.FullName} has an identical object of {foundedClass.FullName} on the Id {foundedID}";
            return new Exception(msg);
        }
    }
}