using Mde.WishList.Api.Application.Common.Exceptions;
using Mde.WishList.Api.Application.Common.Interfaces;
using Mde.WishList.Api.Application.Common.Security;
using Mde.WishList.Api.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.TodoItems.Commands.DeleteTodoItem
{

    [Authorize]
    public class DeleteTodoItemCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public DeleteTodoItemCommandHandler(IApplicationDbContext context, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            if (!await _resourceAuthorizationService.AuthorizeAny(entity, Policies.MustBeCreator, Policies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }


            _context.TodoItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
