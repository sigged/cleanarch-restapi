using Mde.WishList.Api.Application.Common.Mappings;
using Mde.WishList.Api.Domain.Entities;

namespace Mde.WishList.Api.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
