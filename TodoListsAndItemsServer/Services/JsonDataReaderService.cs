using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;
using TodoListsAndItemsServer.Models.DTOs;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

        public Task WriteToTodoItems(List<TodoItem> todoItem)
        {
            try
            {
                string json = JsonSerializer.Serialize(todoItem);
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
                string json = JsonSerializer.Serialize(todoGroup);
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
