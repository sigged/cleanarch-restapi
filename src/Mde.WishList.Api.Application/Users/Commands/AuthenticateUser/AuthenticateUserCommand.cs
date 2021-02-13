using Mde.WishList.Api.Application.Common.Interfaces;
using Mde.WishList.Api.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.Users.Commands.CreateUser
{
    public class AuthenticateUserCommand : IRequest<Result>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public AuthenticateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.AuthenticateAsync(request.UserName, request.Password);
        }
    }
}
