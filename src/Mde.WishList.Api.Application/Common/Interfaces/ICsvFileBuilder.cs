using Mde.WishList.Api.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace Mde.WishList.Api.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
