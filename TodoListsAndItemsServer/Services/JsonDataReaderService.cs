using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;

namespace TodoListsAndItemsServer.Services
{
    public class JsonDataReaderService : IDataReaderService
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
    }
}
