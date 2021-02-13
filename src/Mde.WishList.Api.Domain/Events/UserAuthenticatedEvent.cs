using Mde.WishList.Api.Domain.Common;
using Mde.WishList.Api.Domain.Entities;

namespace Mde.WishList.Api.Domain.Events
{
    public class UserAuthenticatedEvent<TResult> : DomainEvent
    {
        public UserAuthenticatedEvent(IUser user, TResult result)
        {
            User = user;
            Result = result;
        }

        public IUser User { get; }
        public TResult Result { get; }
    }
}
