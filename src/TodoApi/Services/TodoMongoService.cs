using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

using MongoDB.Driver;

namespace TodoApi.Services
{
  public class TodoMongoService
  {
    private IMongoCollection<Todo> collection;
    
    public TodoMongoService(TodosMongoSettings settings)
    {
      collection = new MongoClient(settings.Connection)
              .GetDatabase(settings.DbName).
              GetCollection<Todo>(settings.Collection);
    }

    public async Task<IEnumerable<Todo>> Get()
    {
      return collection.Find(t => true).ToList();
    }

    public async Task<Todo> GetById(string id)
    {
      throw new NotImplementedException();
    }

    public async Task Create(Todo data)
    {
      await collection.InsertOneAsync(data);
    }
  }
}