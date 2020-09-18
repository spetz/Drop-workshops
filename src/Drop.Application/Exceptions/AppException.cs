using System;

namespace Drop.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public abstract string Code { get; }

        protected AppException(string size) : base(size)
        {
        }
    }
}