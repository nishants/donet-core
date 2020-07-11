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
# Models/Todo.cs

namespace TodoApi.Models
{
  public class Todo
  {
    private long id { get; set; }
    private string name { get; set; }
    private bool isComplete { get; set; }
  }
}
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

