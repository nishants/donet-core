# Creating  a TODO app with .NET core



- resources
  - https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code



### Goals

- Create web api
- Add database
- Add unit tests and moq
- Add integration tests



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

