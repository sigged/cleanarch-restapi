using Mde.WishList.Api.Application.Common.Exceptions;
using Mde.WishList.Api.Application.Common.Interfaces;
using Mde.WishList.Api.Application.Common.Security;
using Mde.WishList.Api.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.TodoLists.Commands.DeleteTodoList
{

    [Authorize]
    public class DeleteTodoListCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public DeleteTodoListCommandHandler(IApplicationDbContext context, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoLists
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.Id);
            }

            if (!await _resourceAuthorizationService.AuthorizeAny(entity, Policies.MustBeCreator, Policies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }

            _context.TodoLists.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
