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