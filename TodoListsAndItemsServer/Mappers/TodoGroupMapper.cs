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
        public static TodoGroupDTO Map(TodoGroup group)
        {
            TodoGroupDTO groupDTO = new()
            {
                Id = group.Id,
                Caption = group.Caption,
                Description = group.Description,
                Icon = group.Icon,
                Color = group.Color
            };

            return groupDTO;
        }
    }
}
