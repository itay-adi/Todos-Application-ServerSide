using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListsAndItemsServer.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Caption { get; set; }
        public bool IsCompleted { get; set; }
    }
}