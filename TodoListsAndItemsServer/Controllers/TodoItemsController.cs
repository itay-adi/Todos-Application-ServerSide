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

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoGroup>> GetTodoItemById(int id)
        {
            try
            {
                var group = await this._todosRepository.GetTodoItemById(id);

                return Ok(group);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveItem(int id)
        {
            try
            {
                await _todosRepository.DeleteTodoItemById(id);

                return Ok();
            }

            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> AddTodoItem([FromBody] TodoItem todoItem)
        {
            try
            {
                var item = await _todosRepository.AddTodoItem(todoItem);

                return CreatedAtRoute(nameof(todoItem), new { TodoItemId = todoItem.Id }, item);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
