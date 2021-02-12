using Mde.WishList.Api.Application.Common.Interfaces;
using System;

namespace Mde.WishList.Api.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
