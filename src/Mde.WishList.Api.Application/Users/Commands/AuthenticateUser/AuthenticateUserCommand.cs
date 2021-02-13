using Mde.WishList.Api.Application.Common.Interfaces;
using Mde.WishList.Api.Application.Common.Models;
using Mde.WishList.Api.Application.Common.Security;
using Mde.WishList.Api.Domain.Entities;
using Mde.WishList.Api.Domain.Events;
using Mde.WishList.Api.WebApi.V1.Users.Dto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.Users.Commands.CreateUser
{
    public class AuthenticateUserCommand : IRequest<AuthenticationResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticationResult>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenManager _tokenManager;
        private readonly IDomainEventService _domainEventService;

        public AuthenticateUserCommandHandler(IIdentityService identityService, ITokenManager tokenManager, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _tokenManager = tokenManager;
            _domainEventService = domainEventService;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.AuthenticateAsync(request.UserName, request.Password);
            if (result.Succeeded)
            {
                var entity = await _identityService.FindByName(request.UserName);
                await _domainEventService.Publish(new UserAuthenticatedEvent<Result>(entity, result));
                return new AuthenticationResult(true, result.Errors)
                {
                    Token = await _tokenManager.GenerateAuthTokenAsync(entity)
                };
            }

            await _domainEventService.Publish(new UserAuthenticatedEvent<Result>(null, result));
            return new AuthenticationResult(false, result.Errors);
        }
    }
}
