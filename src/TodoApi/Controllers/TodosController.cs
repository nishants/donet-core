using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
  [ApiController]
  [Route("api/todos")]
  public class TodosController : ControllerBase
  {
    private TodoMongoService _service;
    
    public TodosController(TodoMongoService service )
    {
      _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Todo>> Get()
    {
      return await _service.Get(); 
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetById(string id)
    {
      var todo = await _service.GetById(id);
      if (todo == null) {return NotFound();}
      return todo;
    }
    
    [HttpPost]
    public async Task<ActionResult> Post(Todo todo)
    {
      await _service.Create(todo);
      // Generates location header url value from the `GetById` method route
      return CreatedAtAction("GetById", new {Id = 1}, todo);
    }

    [HttpPut("{id}")]
    public ActionResult<Todo> Update(Todo todo)
    {
      return null;
    }
  }
}