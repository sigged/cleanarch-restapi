using Mde.WishList.Api.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mde.WishList.Api.WebApi.Controllers
{
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        //[HttpGet]
        //public async Task<ActionResult<TodosVm>> Get()
        //{
        //    return await Mediator.Send(new GetTodosQuery());
        //}

        //[HttpGet("{id}")]
        //public async Task<FileResult> Get(int id)
        //{
        //    var vm = await Mediator.Send(new ExportTodosQuery { ListId = id });

        //    return File(vm.Content, vm.ContentType, vm.FileName);
        //}

        [HttpPost]
        [AllowAnonymous]
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
            var result = await Mediator.Send(command);
            if (result.Succeeded)
                return Ok();
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
