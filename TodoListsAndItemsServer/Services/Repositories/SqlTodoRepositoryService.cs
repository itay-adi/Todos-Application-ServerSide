using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;

namespace TodoListsAndItemsServer.Services.Repositories
{
    public class SqlTodoRepositoryService : ITodosRepositoryService
    {
        public Task<TodoGroup> AddNewGroup(TodoGroup group)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> AddTodoItem(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> ChangeTodoItemStatus(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Task> DeleteTodoGroupById(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<Task> DeleteTodoItemById(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<TodoGroup> EditTodoGroupById(int id, TodoGroup group)
        {
            throw new NotImplementedException();
        }

        public Task<List<TodoGroup>> GetAllTodoGroups()
        {
            throw new NotImplementedException();
        }

        public Task<List<TodoItem>> GetAllTodoItems()
        {
            throw new NotImplementedException();
        }

        public Task<TodoGroup> GetTodoGroupById(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> GetTodoItemById(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
