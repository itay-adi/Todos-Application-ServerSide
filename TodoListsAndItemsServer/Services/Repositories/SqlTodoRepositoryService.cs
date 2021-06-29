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
        
        public async Task<TodoGroup> AddNewGroup(TodoGroup group)
        {
            var newGroup = await this._dataContext.TodoGroups.AddAsync(group);
            await _dataContext.SaveChangesAsync();

            return newGroup.Entity;
        }

        public async Task<TodoItem> AddTodoItem(TodoItem todoItem)
        {
            var newItem = await this._dataContext.TodoItems.AddAsync(todoItem);
            await _dataContext.SaveChangesAsync();

            return newItem.Entity;
        }

        public async Task<TodoItem> ChangeTodoItemStatus(int itemId)
        {
            var item = await GetTodoItemById(itemId);
            item.IsCompleted = !item.IsCompleted;
            await _dataContext.SaveChangesAsync();

            return item;
        }

        public async Task<Task> DeleteTodoGroupById(int groupId)
        {
            var items = (await GetAllTodoItems()).Where(i => i.GroupId == groupId);
            _dataContext.TodoItems.RemoveRange(items);

            var group = await GetTodoGroupById(groupId);
            _dataContext.TodoGroups.Remove(group);

            await _dataContext.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<Task> DeleteTodoItemById(int itemId)
        {
            var item = (await GetTodoItemById(itemId));
            _dataContext.TodoItems.Remove(item);

            await _dataContext.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<TodoGroup> EditTodoGroup(TodoGroup group)
        {
            var exsitingGroup = await GetTodoGroupById(group.Id);

            exsitingGroup.Caption = group.Caption;
            exsitingGroup.Color = group.Color;
            exsitingGroup.Description = group.Description;
            exsitingGroup.Icon = group.Icon;

            await _dataContext.SaveChangesAsync();

            return exsitingGroup;
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

        public async Task<TodoGroup> GetTodoGroupById(int groupId)
        {
            var group = await this._dataContext.TodoGroups.FindAsync(groupId);

            return group;
        }

        public async Task<TodoItem> GetTodoItemById(int itemId)
        {
            try
            {
                var item = await this._dataContext.TodoItems.FindAsync(itemId);

                return item;
            }

            catch
            {
                throw new ArgumentException($"Could not find Item No. {itemId}");
            }
        }
    }
}
