using Mde.WishList.Api.Application.Common.Interfaces;
using Mde.WishList.Api.Application.Common.Security;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.TodoLists.Commands.PurgeTodoLists
{
    [Authorize(Policy = Policies.MustBeAdmin)]
    public class PurgeTodoListsCommand : IRequest
    {
    }

    public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
    {
        private readonly IApplicationDbContext _context;

        public PurgeTodoListsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
        {
            _context.TodoLists.RemoveRange(_context.TodoLists);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
