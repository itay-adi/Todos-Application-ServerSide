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
            _todoGroups = (await _dataReader.GetAllTodoGroups()).ToDictionary(g => g.Id);
        }

        //Todo Group Methods----------------------------------------------------------------------------
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

        public async Task<Task> DeleteTodoGroupById(int groupId)
        {
            await _LoadGroups();
            await _LoadItems();

            if (!_todoGroups.ContainsKey(groupId))
            {
                throw new ArgumentException($"TodoGroup: {groupId} doesnt exist");
            }

            var filteredItems = _todoItems.Values.Where(i => i.GroupId != groupId).ToList();

            await _dataReader.WriteToTodoItems(filteredItems);

            _todoGroups.Remove(groupId);

            await _dataReader.WriteToTodoGroups(_todoGroups.Values.ToList());

            return Task.CompletedTask;
        }

        //Todo Item Methods----------------------------------------------------------------------------
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
           
        public async Task<Task> DeleteTodoItemById(int itemId)
        {
            await _LoadItems();
     
            if (!_todoItems.ContainsKey(itemId))
            {
                throw new ArgumentException($"TodoItem: {itemId} doesnt exist");
            }

            _todoItems.Remove(itemId);

            await _dataReader.WriteToTodoItems(_todoItems.Values.ToList());

            return Task.CompletedTask;
        }

        public async Task<TodoItem> ChangeTodoItemStatus(int itemId)
        {
            await _LoadItems();

            if (!_todoItems.ContainsKey(itemId))
            {
                throw new ArgumentException($"TodoItem: {itemId} doesnt exist");
            }

            var isCompleted = _todoItems[itemId].IsCompleted;

            _todoItems[itemId].IsCompleted = !isCompleted;

            await _dataReader.WriteToTodoItems(_todoItems.Values.ToList());

            return _todoItems[itemId];
        }

        /*public async Task<TodoItem> AddTodoItem(TodoItem todoItem)
        {
            await _LoadItems();
            await _LoadGroups();

            todoItem.Id = _todoItems.Keys.Max() + 1;

            if (_todoItems.ContainsKey(todoItem.Id))
            {
                throw new ArgumentException($"TodoItem: {todoItem.Id} is already existing");
            }

            if (!_todoGroups.ContainsKey(todoItem.GroupId))
            {
                throw new ArgumentException($"TodoGroup: {todoItem.GroupId} doesnt exist");
            }

            _todoItems.Add(todoItem.Id, todoItem);

            await _dataReader.WriteToTodoItems(_todoItems.Values.ToList());

            return todoItem;
        }*/
    }
}
