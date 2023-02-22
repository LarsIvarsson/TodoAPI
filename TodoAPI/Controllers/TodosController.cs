using Microsoft.AspNetCore.Mvc;
using TodoAPI.Data;
using TodoAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly AppDbContext context;
        public TodosController(AppDbContext context)
        {
            this.context = context;
        }



        // GET api/<TodosController>
        [HttpGet]
        public ActionResult<IEnumerable<TodoModel>> Get()
        {
            return Ok(context.Todos.ToList());
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public ActionResult<TodoModel> Get(int id)
        {
            List<TodoModel> todos = context.Todos.ToList();

            TodoModel? dbModel = todos.FirstOrDefault(t => t.Id == id);
            if (dbModel != null)
            {
                return Ok(context.Todos.FirstOrDefault(t => t.Id == id));
            }
            return NotFound("Todo task does not exist");
        }

        // POST api/<TodosController>
        [HttpPost]
        public IActionResult Post([FromBody] TodoModel todo)
        {
            List<TodoModel> todos = context.Todos.ToList();

            TodoModel? dbModel = todos.FirstOrDefault(t => t.Todo == todo.Todo);
            if (dbModel == null)
            {
                context.Todos.Add(todo);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest("Todo task already exists");
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TodoModel todo)
        {
            // code not yet implemented for updating Todos
            var existingTodo = context.Todos.FirstOrDefault(t => t.Id == id);

            if (existingTodo == null)
            {
                return NotFound("Todo task was not found");
            }
            existingTodo.Todo = todo.Todo;
            existingTodo.Description = todo.Description;
            existingTodo.IsCompleted = todo.IsCompleted;
            return Ok();
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TodoModel? todoToDelete = context.Todos.FirstOrDefault(t => t.Id == id);
            if (todoToDelete == null)
            {
                return NotFound("Todo task wot found");
            }
            context.Todos.Remove(todoToDelete);
            context.SaveChanges();
            return Ok();
        }
    }
}
