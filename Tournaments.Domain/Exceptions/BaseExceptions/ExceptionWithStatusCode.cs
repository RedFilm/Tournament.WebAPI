﻿namespace Tournaments.Domain.Exceptions.BaseExceptions
{
    public class ExceptionWithStatusCode : Exception
    {
        public ExceptionWithStatusCode(string message, Exception inner) : base(message, inner)
        {

        }
        public ExceptionWithStatusCode(string message) : base(message)
        {

        }
        public ExceptionWithStatusCode()
        {

        }

        public virtual int StatusCode { get; set; }
    }
}
