using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Mappers;
using TodoListsAndItemsServer.Models.DTOs;
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
        public async Task<ActionResult<List<TodoItemDTO>>> GetAllItems()
        {
            try
            {
                var AllItems = await this._todosRepository.GetAllTodoItems();
                var AllItemsDTO = AllItems.Select(i => TodoItemMapper.MapToItemDTO(i));

                return Ok(AllItemsDTO);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItemById(int id)
        {
            try
            {
                var item = await this._todosRepository.GetTodoItemById(id);
                var itemDTO = TodoItemMapper.MapToItemDTO(item);

                return Ok(item);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("completed")]
        public async Task<ActionResult<TodoItemDTO>> GetCompletedTodoItems()
        {
            try
            {
                var AllItems = await this._todosRepository.GetAllTodoItems();
                var AllItemsDTO = AllItems.Where(i => i.IsCompleted == true)
                                          .Select(i => TodoItemMapper.MapToItemDTO(i));

                return Ok(AllItemsDTO);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<TodoItemDTO>> GetActiveTodoItems()
        {
            try
            {
                var AllItems = await this._todosRepository.GetAllTodoItems();
                var AllActiveItemsDTO = AllItems.Select(i => TodoItemMapper.MapToItemDTO(i))
                                                .Where(i => i.IsCompleted == false);

                return Ok(AllActiveItemsDTO);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("countActive")]
        public async Task<ActionResult<int>> GetNumberOfActiveTodoItems()
        {
            try
            {
                var AllActiveItemsCounter = (await this._todosRepository.GetAllTodoItems())
                                                                        .Where(i => i.IsCompleted == false)
                                                                        .Count();

                return Ok(AllActiveItemsCounter);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/status")]
        public async Task<ActionResult<Boolean>> GetTodoItemStatus(int id)
        {
            try
            {
                var itemStatus = (await this._todosRepository.GetTodoItemById(id)).IsCompleted;

                return Ok(itemStatus);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("countAll")]
        public async Task<ActionResult<int>> GetNumberOfTodoItems()
        {
            try
            {
                var AllItemsCounter = (await this._todosRepository.GetAllTodoItems())
                                                                  .Count;

                return Ok(AllItemsCounter);
            }

            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("countItemsPerGroup/{groupId}")]
        public async Task<ActionResult<int>> GetNumberOfTodoItemsPerGroup(int groupId)
        {
            try
            {
                var AllItemsCounter = (await this._todosRepository.GetAllTodoItems())
                                                                  .Where(i => i.GroupId == groupId)
                                                                  .Count();

                return Ok(AllItemsCounter);
            }    

            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getItemsPerGroup/{groupId}")]
        public async Task<ActionResult<List<TodoItemDTO>>> GetTodoItemsPerGroup(int groupId)
        {
            try
            {
                var AllItemsPerGroup = (await this._todosRepository.GetAllTodoItems())
                                                                  .Where(i => i.GroupId == groupId);

                return Ok(AllItemsPerGroup);
            }

            catch
            {
                return BadRequest();
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

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<TodoItemDTO>> ChangeItemStatus(int id)
        {
            try
            {
                var todoItem = await _todosRepository.ChangeTodoItemStatus(id);

                return Ok(TodoItemMapper.MapToItemDTO(todoItem));
            }

            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> AddTodoItem([FromBody] TodoItemDTO todoItemDTO)
        {
            var todoItem = TodoItemMapper.MapToItem(todoItemDTO);

            try
            {
                var item = await _todosRepository.AddTodoItem(todoItem);

                var newItemDTO = TodoItemMapper.MapToItemDTO(item);

                return Created(nameof(GetTodoItemById), newItemDTO);
            }

            catch
            {
                return BadRequest();
            }
        }
    }
}
