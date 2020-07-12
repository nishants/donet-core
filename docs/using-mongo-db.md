

- Run mongodb ([docker image](https://hub.docker.com/_/mongo)) locally

  ```bash
  docker run  -i -v $PWD/db:/data/db -p 27017:27017 mongo:3.6.18 -p mongod
  ```

- GUI for mongo db : `Robo 3T`

- Install mongoldb : 

  ```bash
  dotnet add package MongoDB.Driver
  ```

  

- Setup mongo for .NET core 

  ```bash
  dotnet add package MongoDB.Driver
  ```

  ```diff
  <Project Sdk="Microsoft.NET.Sdk.Web">
    <PropertyGroup>
      <TargetFramework>netcoreapp3.1</TargetFramework>
    </PropertyGroup>
    <ItemGroup>
      <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="3.1.5" />
      <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="3.1.5" />
  +   <PackageReference Include="MongoDB.Driver" Version="2.10.4" />
    </ItemGroup>
  </Project>
  ```



- Create a mongo model

  ```c#
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;
  
  namespace BooksApi.Models
  {
      public class Book
      {
          [BsonId]
          [BsonRepresentation(BsonType.ObjectId)]
          public string Id { get; set; }
  
          [BsonElement("Name")]
          public string BookName { get; set; }
  
          public decimal Price { get; set; }
  
          public string Category { get; set; }
  
          public string Author { get; set; }
      }
  }
  ```

  

