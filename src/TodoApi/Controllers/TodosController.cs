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
    private TodosService _service;
    
    public TodosController(TodosService service )
    {
      _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Todo>> Get()
    {
      return await _service.Get();
    }
  }
}