using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;

namespace TodoListsAndItemsServer.Services
{
    public interface ITodosRepositoryService
    {
        Task<List<TodoGroup>> GetAllTodoGroups();
        Task<List<TodoItem>> GetAllTodoItems();
    }
}
