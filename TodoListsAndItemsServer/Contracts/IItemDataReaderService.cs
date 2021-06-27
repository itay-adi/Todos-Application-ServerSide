using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;
using TodoListsAndItemsServer.Models.DTOs;

namespace TodoListsAndItemsServer.Services
{
    public interface IItemDataReaderService
    {
        Task<List<TodoItem>> GetAllTodoItems();
        Task<List<TodoGroup>> GetAllTodoGroups();
        Task WriteToTodoItems(List<TodoItem> todoItem);
        Task WriteToTodoGroups(List<TodoGroup> todoGroup);
    }
}