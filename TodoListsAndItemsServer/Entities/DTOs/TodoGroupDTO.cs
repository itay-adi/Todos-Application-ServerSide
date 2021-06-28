using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListsAndItemsServer.Models.DTOs
{
    public class TodoGroupDTO
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
    }
}
