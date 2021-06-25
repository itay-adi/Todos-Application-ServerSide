using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;
using TodoListsAndItemsServer.Mappers;
using TodoListsAndItemsServer.Models.DTOs;
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
        public async Task<ActionResult<List<TodoGroupDTO>>> GetAllGroups()
        {
            var AllGroups = await this._todosRepository.GetAllTodoGroups();
            var AllGroupsDTO = AllGroups.Select(g => TodoGroupMapper.Map(g)).ToList();

            return Ok(AllGroupsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoGroupDTO>> GetTodoGroupById(int id)
        {
            try
            {
                var group = await this._todosRepository.GetTodoGroupById(id);
                var groupDTO = TodoGroupMapper.Map(group);

                return Ok(groupDTO);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
