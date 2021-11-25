using Mde.WishList.Api.Application.Common.Exceptions;
using Mde.WishList.Api.Application.Common.Interfaces;
using Mde.WishList.Api.Domain.Entities;
using Mde.WishList.Api.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.TodoItems.Commands.UpdateTodoItemDetail
{
    public class UpdateTodoItemDetailCommand : IRequest
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public PriorityLevel Priority { get; set; }

        public string Note { get; set; }
    }

    public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.ListId = request.ListId;
            entity.Priority = request.Priority;
            entity.Note = request.Note;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
