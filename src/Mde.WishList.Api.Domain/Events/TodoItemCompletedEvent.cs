using Mde.WishList.Api.Domain.Common;
using Mde.WishList.Api.Domain.Entities;

namespace Mde.WishList.Api.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
