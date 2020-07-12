

- Does not support parallel
- For different threads, different objects need to be created
- Hence is `scoped` for dependency injection by default.

- One db context object is suppose to be used by one thread only

- If multiple thread try to use the same dbcontext in parallel, it throws exception: 

  ```
  A second operation started on this context before a previous operation completed. This is usually caused by different threads using the same instance of DbContext, however instance members are not guaranteed to be thread safe.
  ```

  

Question : 

- why is dbcontext injection is scoped ?
  - becasue does not support threads using same object. Every reqeust must create its on own dbcontext.
- Is a controller instantiated only once  ?
  - no instigated for every request. and scopes are injected
- What is difference between `transient`, `scope` and `singleton` depedencies ?
  - scope: created for every request (e.g. dbcontext)
  - transient: created everytime it needs to be injected
  - singleton: created only once after app starts
- 



