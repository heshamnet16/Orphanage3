﻿using System;

namespace OrphanageService.Services.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException() : base(Properties.Resources.Error_NotFound)
        {
        }

        public ObjectNotFoundException(string message) : base(message)
        {
        }

        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}