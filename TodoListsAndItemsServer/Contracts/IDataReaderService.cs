using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;

namespace TodoListsAndItemsServer.Services
{
    public interface IDataReaderService
    {
        Task<List<TodoItem>> GetAllTodoItems();
        Task<List<TodoGroup>> GetAllTodoGroups();
    }
}