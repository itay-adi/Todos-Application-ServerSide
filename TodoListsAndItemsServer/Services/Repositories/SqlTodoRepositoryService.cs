using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.DataAccess;
using TodoListsAndItemsServer.Entities;

namespace TodoListsAndItemsServer.Services.Repositories
{
    public class SqlTodoRepositoryService : ITodosRepositoryService
    {
        private readonly DataContext _dataContext;

        public SqlTodoRepositoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

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

        public async Task<List<TodoGroup>> GetAllTodoGroups()
        {
            var allGroups = await this._dataContext.TodoGroups.ToListAsync();

            return allGroups;
        }

        public async Task<List<TodoItem>> GetAllTodoItems()
        {
            var allItems = await this._dataContext.TodoItems.ToListAsync();

            return allItems;
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
