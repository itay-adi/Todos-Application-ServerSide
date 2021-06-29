using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;

namespace TodoListsAndItemsServer.Services.Repositories
{
    public class JsonTodoRepositoryService : ITodosRepositoryService
    {
        private readonly IItemDataReaderService _dataReader;

        private Dictionary<int, TodoItem> _todoItems;
        private Dictionary<int, TodoGroup> _todoGroups;

        public JsonTodoRepositoryService(IItemDataReaderService dataReader)
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

            try
            {
                return _todoGroups.Values.ToList();
            }

            catch
            {
                throw new Exception("Could not get all groups");
            }
        }

        public async Task<TodoGroup> GetTodoGroupById(int groupId)
        {
            await _LoadGroups();

            try
            {
                return _todoGroups[groupId];
            }

            catch
            {
                throw new ArgumentException($"Could not find groups No. {groupId}");
            }
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

        public async Task<TodoGroup> EditTodoGroup(TodoGroup group)
        {
            await _LoadGroups();

            if (!_todoGroups.ContainsKey(group.Id))
            {
                throw new ArgumentException($"TodoGroup: {group.Id} doesnt exist");
            }

            _todoGroups.Remove(group.Id);

            _todoGroups[group.Id] = group;

            await _dataReader.WriteToTodoGroups(_todoGroups.Values.ToList());

            return group;
        }

        public async Task<TodoGroup> AddNewGroup(TodoGroup group)
        {
            await _LoadGroups();

            if (_todoGroups.Count > 0) {
                group.Id = _todoGroups.Keys.Max() + 1; 
            }

            else
            {
                group.Id = 1;
            }

            _todoGroups.Add(group.Id, group);

            await _dataReader.WriteToTodoGroups(_todoGroups.Values.ToList());

            return group;
        }

        //Todo Item Methods----------------------------------------------------------------------------
        public async Task<List<TodoItem>> GetAllTodoItems()
        {
            await _LoadItems();

            try
            {
                return _todoItems.Values.ToList();
            }

            catch
            {
                throw new Exception("Could not get all Items");
            }
        }

        public async Task<TodoItem> GetTodoItemById(int itemId)
        {
            await _LoadItems();

            try
            {
                return _todoItems[itemId];
            }

            catch
            {
                throw new ArgumentException($"Could not find Item No. {itemId}");
            }
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

        public async Task<TodoItem> AddTodoItem(TodoItem todoItem)
        {
            await _LoadItems();
            await _LoadGroups();

            if (_todoItems.Count > 0)
            {
                todoItem.Id = _todoItems.Keys.Max() + 1;
            }

            else
            {
                todoItem.Id = 1;
            }

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
        }
    }
}
