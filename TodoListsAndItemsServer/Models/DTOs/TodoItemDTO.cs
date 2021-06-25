using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListsAndItemsServer.Models.DTOs
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Caption { get; set; }
        public bool IsCompleted { get; set; }
    }
}
