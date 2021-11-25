using System;

namespace Mde.WishList.Api.Application.Common.Exceptions
{
    public class ForbiddenAccessException : ApplicationException
    {
        public ForbiddenAccessException() : base() { }
    }
}
