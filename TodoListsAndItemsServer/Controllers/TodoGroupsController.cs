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
            try
            {
                var AllGroups = await this._todosRepository.GetAllTodoGroups();
                var AllGroupsDTO = AllGroups.Select(g => TodoGroupMapper.MapToGroupDTO(g)).ToList();

                return Ok(AllGroupsDTO);
            }

            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoGroupDTO>> GetTodoGroupById(int id)
        {
            try
            {
                var group = await this._todosRepository.GetTodoGroupById(id);
                var groupDTO = TodoGroupMapper.MapToGroupDTO(group);

                return Ok(groupDTO);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("countAll")]
        public async Task<ActionResult<int>> GetNumberOfTodoGroups()
        {
            try
            {
                var AllGroupsCounter = (await this._todosRepository.GetAllTodoGroups())
                                                                   .Count;

                return Ok(AllGroupsCounter);
            }

            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveGroup(int id)
        {
            try
            {
                await _todosRepository.DeleteTodoGroupById(id);

                return Ok();
            }

            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoGroupDTO>> RemoveGroup(int id, [FromBody] TodoGroupDTO groupDTO)
        {
            if(id != groupDTO.Id)
            {
                return BadRequest();
            }

            var group = TodoGroupMapper.MapToGroup(groupDTO);

            try
            {
                await _todosRepository.EditTodoGroup(group);

                return Ok(groupDTO);
            }

            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoGroup>> AddNewGroup([FromBody] TodoGroupDTO groupDTO)
        {
            var group = TodoGroupMapper.MapToGroup(groupDTO);
            
            try
            {

                var newGroup = await _todosRepository.AddNewGroup(group);

                var finalGroupDTO = TodoGroupMapper.MapToGroupDTO(newGroup);

                return Created(nameof(GetTodoGroupById), finalGroupDTO);
            }

            catch
            {
                return BadRequest();
            }
        }
    }
}
