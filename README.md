# Todos-Application
An end to end Application, which consists a [client side](https://github.com/itay-adi/Todos-Application-ClientSide) and a [server side](https://github.com/itay-adi/Todos-Application-ServerSide)

## ServerSide
An ASP.NET 5.0 Server application, supporting http request through Web api (GET/POST/PUT/PATCH/DELETE).
The purpose of this server is to function as the back side of the todo web application.
The server has the option to manage the data (todo items, and todo groups/lists) through JSON files, or through SQL server

## Server api
## TodoItems controller:
  #### GET:
  1. **[HttpGet]** Task<ActionResult<List<TodoItemDTO>>> GetAllItems()
      * Retruns all the todoitems
      
  2. **[HttpGet("{id}")]** Task<ActionResult<TodoItemDTO>> GetTodoItemById(int id)
      * Return a todo item by its ID
      
  3. **[HttpGet("completed")]** Task<ActionResult<TodoItemDTO>> GetCompletedTodoItems()
      * Return all the completed todo items
      
  4. **[HttpGet("active")]** Task<ActionResult<TodoItemDTO>> GetActiveTodoItems()
      * Return all the active todo items
      
  5. **[HttpGet("countActive")]** Task<ActionResult<int>> GetNumberOfActiveTodoItems()
      * Return the number of active todo items
      
  6. **[HttpGet("{id}/status")]** Task<ActionResult<Boolean>> GetTodoItemStatus(int id)
      * Return the status (active/completed) of a todo item
      
  7. **[HttpGet("countAll")]** Task<ActionResult<int>> GetNumberOfTodoItems()
      * Return the number of all todo items
      
  8. **[HttpGet("countItemsPerGroup/{groupId}")]** Task<ActionResult<int>> GetNumberOfTodoItemsPerGroup(int groupId)
      * Return the number of todo items per group ID 
      
  9. **[HttpGet("getItemsPerGroup/{groupId}")]** Task<ActionResult<List<TodoItemDTO>>> GetTodoItemsPerGroup(int groupId)
      * Return the doto items per group id
  
  #### POST:
  1. **[HttpPost]** Task<ActionResult<TodoItemDTO>> AddTodoItem([FromBody] TodoItemDTO todoItemDTO)
      * Insert a new Todo Item 
  
  #### PATCH:
  1. **[HttpPatch("{id}/status")]** Task<ActionResult<TodoItemDTO>> ChangeItemStatus(int id)
      * Changes item status from uncomplited to completed and vice versa
  
  #### PATCH:
  1. **[HttpDelete("{id}")]** Task<ActionResult> RemoveItem(int id)
      * Delete a todo item according to its ID

  ## TodoGroups controller:
  #### GET:
  1. **[HttpGet]** Task<ActionResult<List<TodoGroupDTO>>> GetAllGroups()
      * Return all the todo groups

  2. **[HttpGet("{id}")]** Task<ActionResult<TodoGroupDTO>> GetTodoGroupById(int id)
      * Retrun a group by its ID

  3. **[HttpGet("countAll")]** Task<ActionResult<int>> GetNumberOfTodoGroups()
      * Return the number of todo groups

  #### POST:
  1. **[HttpPost]** Task<ActionResult<TodoGroup>> AddNewGroup([FromBody] TodoGroupDTO groupDTO)
      * Insert a new todo group

  #### PUT:
  1. **[HttpPut("{id}")]** Task<ActionResult<TodoGroupDTO>> EditGroup(int id, [FromBody] TodoGroupDTO groupDTO)
      * Edit an existing todo group with new details (color, icon, etc.)

  #### DELETE:
  1. **[HttpDelete("{id}")]** Task<ActionResult> RemoveGroup(int id)
      * Remove a todo group **AND ITS ITEMS**
  
    
    
