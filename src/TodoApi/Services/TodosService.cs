using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using System.Threading.Tasks;

namespace TodoApi.Services
{
  public class TodosService
  {
    private TodoContext _context;

    public TodosService(TodoContext context)
    {
      _context = context;
    }

    public  Task<List<Todo>> Get()
    {
      return  _context.Todos.ToListAsync();
    }
  }
}