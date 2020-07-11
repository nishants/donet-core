using System;
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
      Console.WriteLine("another controller created....");
    }

    public  Task<List<Todo>> Get()
    {
      return  _context.Todos.ToListAsync();
    }
    public async Task<Todo> GetById(long id)
    {
      return await _context.Todos.FindAsync(id);
    }

    public async void Create(Todo todo)
    {
      _context.Todos.Add(todo);
      await _context.SaveChangesAsync();
    }
    
  }
}