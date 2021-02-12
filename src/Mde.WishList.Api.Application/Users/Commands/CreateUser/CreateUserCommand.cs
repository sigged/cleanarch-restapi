using Mde.WishList.Api.Application.Common.Interfaces;
using Mde.WishList.Api.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string UserName { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateUserAsync(request.UserName, "sderdeyn@gmail.com123A");
            if (result.Result.Succeeded)
            {
                return result.UserId;
            }
            else
            {
                return null;
            }
            //var entity = new User();

            //entity.Title = request.Title;

            //_context.TodoLists.Add(entity);

            //await _context.SaveChangesAsync(cancellationToken);

            //return entity.Id;
            //throw new NotImplementedException();
        }
    }
}
