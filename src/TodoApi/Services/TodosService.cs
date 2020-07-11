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