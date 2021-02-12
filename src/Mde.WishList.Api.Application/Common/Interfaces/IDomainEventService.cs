using Mde.WishList.Api.Domain.Common;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
