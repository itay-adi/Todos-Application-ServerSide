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
    public class TodoGroupsController : ControllerBase
    {
        private readonly ITodosRepositoryService _todosRepository;

        public TodoGroupsController(ITodosRepositoryService todosRepository)
        {
            _todosRepository = todosRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoGroup>>> GetAllGroups()
        {
            var AllGroups = await this._todosRepository.GetAllTodoGroups();

            return Ok(AllGroups);
        }

        
    }
}
