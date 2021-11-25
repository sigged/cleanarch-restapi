using Mde.WishList.Api.Application.Common.Models;
using Mde.WishList.Api.Application.TodoItems.Commands.CreateTodoItem;
using Mde.WishList.Api.Application.TodoItems.Commands.DeleteTodoItem;
using Mde.WishList.Api.Application.TodoItems.Commands.UpdateTodoItem;
using Mde.WishList.Api.Application.TodoItems.Commands.UpdateTodoItemDetail;
using Mde.WishList.Api.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Mde.WishList.Api.Application.TodoLists.Queries.GetTodos;
using Mde.WishList.Api.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Mde.WishList.Api.WebApi.V1.Users
{
    [ApiVersion("1.0")]
    [ApiController]
    public class TodoItemsController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<TodoItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PaginatedList<TodoItemDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> Update(UpdateTodoItemCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateItemDetails(UpdateTodoItemDetailCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await Mediator.Send(new DeleteTodoItemCommand { Id = id });

            return NoContent();
        }
    }
}
