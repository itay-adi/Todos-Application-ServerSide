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
        private bool _isDataLoaded = false;

        private Dictionary<int, TodoItem> _todoItems;
        private Dictionary<int, TodoGroup> _todoGroups;

        public TodosRepositoryService(IDataReaderService dataReader)
        {
            _dataReader = dataReader;
        }

        private async Task _LoadData()
        {
            if (_isDataLoaded)
            {
                return;
            }

            _isDataLoaded = true;
            _todoGroups = (await _dataReader.GetAllTodoGroups()).ToDictionary(i => i.Id);
            _todoItems = (await _dataReader.GetAllTodoItems()).ToDictionary(i => i.Id);
        }

        public async Task<List<TodoGroup>> GetAllTodoGroups()
        {
            await _LoadData();

            return _todoGroups.Values.ToList();
        }

        public async Task<List<TodoItem>> GetAllTodoItems()
        {
            await _LoadData();

            return _todoItems.Values.ToList();
        }


    }
}
