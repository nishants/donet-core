Queries

- [ ] how many db context per project ?
- [ ] 

# Creating  a TODO app with .NET core



- resources
  - https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code



### Goals

- [ ] Create web api
- [ ] Add database
- [ ] Add unit tests and moq
- [ ] Add integration tests
- [ ] Add enum in model
- [ ] Add database migrations
- [ ] Add seed data
- [ ] Containerize
- [ ] Deploy to aks



### Create a WebApi Project

```bash
# create a web api project
dotnet new webapi -o TodoApi

# Add dependendicies 
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

```diff
+  <ItemGroup>
+    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="3.1.5" />
+    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="3.1.5" />
+  </ItemGroup>
```



### Create Todo Model

```c#
namespace TodoApi.Models
{
  public class Todo
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
  }
}
```



### Create a controller

- Create an API endpoint:
  ```c#
  using System.Collections.Generic;
  using Microsoft.AspNetCore.Mvc;

  namespace TodoApi.Controllers
  {
    [ApiController]
    [Route("api/todos")]
    public class TodosController : ControllerBase
    {
      [HttpGet]
      public IEnumerable<string> Get()
      {
        return new[] {"one", "two"};
      }
    }
  }
  ```

- now check url `https://localhost:5001/api/todos`

  ```
  [
    "one",
    "two"
  ]
  ```



### Create a service class

```c#
using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Services
{
  public class TodosService
  {
    public IEnumerable<Todo> Get()
    {
      var one = new Todo(){Id = 1, Name="Do first thing", IsComplete = false};
      var two = new Todo(){Id = 1, Name="Do second thing", IsComplete = true};
      IList<Todo> list = new List<Todo>();
      list.Add(one);
      list.Add(two);
      return list;
    }
  }
}
```



### Dependency Injection Container registration for Service

- to `Startup.cs` add the serivce as singleton dependency

  ```diff
  +using TodoApi.Services;

   namespace TodoApi
   {
           public void ConfigureServices(IServiceCollection services)
           {
  +            services.AddSingleton<TodosService>();
               services.AddControllers();
           }
  ```

- Now inject the service into controller : 

  ```c#
  using System.Collections.Generic;
  using Microsoft.AspNetCore.Mvc;
  using TodoApi.Models;
  using TodoApi.Services;
  
  namespace TodoApi.Controllers
  {
    [ApiController]
    [Route("api/todos")]
    public class TodosController : ControllerBase
    {
      private TodosService _service;
      
    public TodosController(TodosService service )
      {
      _service = service;
      }

      [HttpGet]
      public IEnumerable<Todo> Get()
      {
        return _service.Get();
      }
    }
  }
  ```
  
  
  
- check api to figure out if dependency injection worked  `https://localhost:5001/api/todos` : 

  ```json
  [
    {
      "id": 1,
      "name": "Do first thing",
      "isComplete": false
    },
    {
      "id": 1,
      "name": "Do second thing",
      "isComplete": true
    }
  ]
  ```

  

  

  
  

### Create TodoContext (database context with EntityFrameworkCore)

```c#
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
  public class TodoContext : DbContext
  {
    public TodoContext(DbContextOptions<TodoContext> options): base(options) { }
    
    public DbSet<Todo> Todos { get; set; }

  }
}
```



### Register Database Context

```diff
 using Microsoft.Extensions.Logging;
+using Microsoft.EntityFrameworkCore;
+using TodoApi.Models;

 namespace TodoApi
 {
@@ -26,6 +28,7 @@ namespace TodoApi
         public void ConfigureServices(IServiceCollection services)
         {
             services.AddControllers();
+            services.AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("TodoDatabase"))
         }
```





### Create the controller class 

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(long id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: api/Todo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(long id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(long id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        private bool TodoExists(long id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}

```

