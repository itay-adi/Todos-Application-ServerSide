using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;

namespace TodoListsAndItemsServer.Services.Repositories
{
    public class TodosRepositoryService : ITodosRepositoryService
    {
        private IDataReaderService _dataReader;
        //private bool _isDataLoaded = false;

        private Dictionary<int, TodoItem> _todoItems;
        private Dictionary<int, TodoGroup> _todoGroups;

        public TodosRepositoryService(IDataReaderService dataReader)
        {
            _dataReader = dataReader;
        }

        private async Task _LoadItems()
        {
            _todoItems = (await _dataReader.GetAllTodoItems()).ToDictionary(i => i.Id);
        }

        private async Task _LoadGroups()
        {
            _todoGroups = (await _dataReader.GetAllTodoGroups()).ToDictionary(i => i.Id);
        }

        //Todo Group Methods
        public async Task<List<TodoGroup>> GetAllTodoGroups()
        {
            await _LoadGroups();

            return _todoGroups.Values.ToList();
        }

        public async Task<TodoGroup> GetTodoGroupById(int groupId)
        {
            await _LoadGroups();

            return _todoGroups[groupId];
        }

        //Todo Item Methods
        public async Task<List<TodoItem>> GetAllTodoItems()
        {
            await _LoadItems();

            return _todoItems.Values.ToList();
        }

        public async Task<TodoItem> GetTodoItemById(int itemId)
        {
            await _LoadItems();

            return _todoItems[itemId];
        }

        public Task DeleteTodoItemById(int itemId)
        {
            if (!_todoItems.ContainsKey(itemId))
            {
                throw new ArgumentException($"TodoItem: {itemId} doesnt exist");
            }

            _todoItems.Remove(itemId);

            return Task.CompletedTask;
        }

        public Task<TodoItem> AddTodoItem(TodoItem todoItem)
        {
            if (_todoItems.ContainsKey(todoItem.Id))
            {
                throw new ArgumentException($"TodoItem: {todoItem.Id} is already existing");
            }

            _todoItems.Add(todoItem.Id, todoItem);

            return Task.FromResult(todoItem);
        }
    }
}
