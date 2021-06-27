using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;
using TodoListsAndItemsServer.Models.DTOs;

namespace TodoListsAndItemsServer.Mappers
{
    public class TodoGroupMapper
    {
        public static TodoGroupDTO MapToGroupDTO(TodoGroup group)
        {
            TodoGroupDTO groupDTO = new()
            {
                Id = (int)group.Id,
                Caption = group.Caption,
                Description = group.Description,
                Icon = group.Icon,
                Color = group.Color
            };

            return groupDTO;
        }

        public static TodoGroup MapToGroup(TodoGroupDTO groupDTO)
        {
            TodoGroup group = new()
            {
                Id = groupDTO.Id,
                Caption = groupDTO.Caption,
                Description = groupDTO.Description,
                Icon = groupDTO.Icon,
                Color = groupDTO.Color
            };

            return group;
        }
    }
}
