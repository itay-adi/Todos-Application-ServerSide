using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.Entities;
using TodoListsAndItemsServer.Models.DTOs;

namespace TodoListsAndItemsServer.Mappers
{
    public class TodoItemMapper
    {
        public static TodoItemDTO Map(TodoItem item)
        {
            TodoItemDTO itemDTO = new()
            {
                Id = item.Id,
                ListId = item.GroupId,
                Caption = item.Caption,
                IsCompleted = item.IsCompleted
            };

            return itemDTO;
        }
    }
}
