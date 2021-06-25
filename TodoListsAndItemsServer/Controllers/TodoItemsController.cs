using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;
using TodoListsAndItemsServer.Services;

namespace TodoListsAndItemsServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodosRepositoryService _todosRepository;

        public TodoItemsController(ITodosRepositoryService todosRepository)
        {
            _todosRepository = todosRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetAllItems()
        {
            var AllItems = await this._todosRepository.GetAllTodoItems();

            return Ok(AllItems);
        }
    }
}
