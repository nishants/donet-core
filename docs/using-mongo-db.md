

- Run mongodb ([docker image](https://hub.docker.com/_/mongo)) locally

  ```bash
  docker run  -i -v $PWD/db:/data/db -p 27017:27017 mongo:3.6.18 -p mongod
  ```

- GUI for mongo db : `Robo 3T`

- Install mongoldb : 

  ```bash
  dotnet add package MongoDB.Driver
  ```

  

- 

