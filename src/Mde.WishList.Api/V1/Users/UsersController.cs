using Mde.WishList.Api.Application.Users.Commands.CreateUser;
using Mde.WishList.Api.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mde.WishList.Api.WebApi.V1.Users
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult> Authenticate(AuthenticateUserCommand command)
        {
            //Mediator.
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);
            else
                return Unauthorized(result);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, UpdateTodoListCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await Mediator.Send(new DeleteTodoListCommand { Id = id });

        //    return NoContent();
        //}
    }
}
