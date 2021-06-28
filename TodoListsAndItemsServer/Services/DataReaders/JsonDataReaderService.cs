using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;

namespace TodoListsAndItemsServer.Services
{
    public class JsonDataReaderService : IItemDataReaderService
    {
        private const string _basePath = "Data";
        private const string _todoItemsFile = "TodoItems.json";
        private const string _todoGroupsFile = "TodoGroups.json";

        public Task<List<TodoGroup>> GetAllTodoGroups()
        {
            var filePath = Path.Combine(_basePath, _todoGroupsFile);
            var JsonContent = File.ReadAllText(filePath);
            var allTodoGroupsAsList = JsonConvert.DeserializeObject<List<TodoGroup>>(JsonContent);

            return Task.FromResult(allTodoGroupsAsList);
        }

        public Task<List<TodoItem>> GetAllTodoItems()
        {
            var filePath = Path.Combine(_basePath, _todoItemsFile);
            var JsonContent = File.ReadAllText(filePath);
            var allTodoItemsAsList = JsonConvert.DeserializeObject<List<TodoItem>>(JsonContent);

            return Task.FromResult(allTodoItemsAsList);
        }

        public Task WriteToTodoItems(List<TodoItem> todoItem)
        {
            try
            {
                string json = JsonConvert.SerializeObject(todoItem);
                var filePath = Path.Combine(_basePath, _todoItemsFile);
                File.WriteAllText(filePath, json);

                return Task.CompletedTask;
            }
            catch
            {
                throw new Exception("Could not write to TodoItemFile");
            }
        }

        public Task WriteToTodoGroups(List<TodoGroup> todoGroup)
        {
            try
            {
                string json = JsonConvert.SerializeObject(todoGroup);
                var filePath = Path.Combine(_basePath, _todoGroupsFile);
                File.WriteAllText(filePath, json);

                return Task.CompletedTask;
            }
            catch
            {
                throw new Exception("Could not write to TodoGroupFile");
            }
        }
    }
}
