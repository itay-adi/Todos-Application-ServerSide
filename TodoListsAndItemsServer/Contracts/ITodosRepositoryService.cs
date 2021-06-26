using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;

namespace TodoListsAndItemsServer.Services
{
    public interface ITodosRepositoryService
    {
        //Todo Group Methods
        Task<List<TodoGroup>> GetAllTodoGroups();
        Task<TodoGroup> GetTodoGroupById(int groupId);
        Task<Task> DeleteTodoGroupById(int groupId);

        //Todo Item Methods
        Task<List<TodoItem>> GetAllTodoItems();
        Task<TodoItem> GetTodoItemById(int itemId);
        Task<Task> DeleteTodoItemById(int itemId);
        Task<TodoItem> ChangeTodoItemStatus(int itemId);
        
        //Task<TodoItem> AddTodoItem(TodoItem todoItem);
    }
}
