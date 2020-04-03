# nservicebus-rabbitMQ-netcore-api
This is a sample project that uses nservicebus 7 , .net core 3.1 and RabbitMQ as transport engine
this project user can register then login with provided email and password
after login user get a jwt token 

an authenticated user may send API request to get a distance between two geolocation points.
he is also able to send a GET request to see the histories of his api calls (for geoLocaltion distances)


## Instructions to run project manually

**Install and run RabbitMQ**

this project uses RabbitMQ as backbone for transporting messaging so make sure you have already installed rabbitMQ and it's running. [Install RabbitMQ](https://www.rabbitmq.com/download.html)

**Restore nuget Packages and build solution**

run ```dotnet restore```
then ```dotnet build```

**Run unit tests**

to see if all tests pass

run ```dotnet test```

**Run each command in a _seperate_ terminal(Linux) or commandPrompt(Windows)**

run StorageService Microservice : ```dotnet run --project StorageService/StorageService.csproj```

run GeoCalcService Microservice : ```dotnet run --project GeoCalcService/GeoCalcService.csproj```

run AuthService Microservice WebAPI : ```dotnet run --project AuthService/AuthService.csproj```

now all the services are running and you can make api calls to http://localhost:5000

**List of API calls**

Register a new User
_http://localhost:5000/Users/Register_ 

POST request body:
```
{
  "firstName" : "test",
  "lastName" :"test",
  "email" : "test@gmail.com",
  "password" : "strongPassword"
}
```
POST response body:
```
{
  "message" : "test@gmail.com created successfully, now you can login with provided username and password"
}
```

login user 
_http://localhost:5000/users/Authenticate_

POST request body:
```
{
  "email" : "test@gmail.com",
  "password" : "strongPassword"
}
```
POST response body: (you will get a different one)
```
{
  "token" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImU2ZTkyMDA5LWU1NzgtNDViZi1iZTRmLTYwNGM4M2M3MDcyMiIsIm5iZiI6MTU4NTkyNzc1MCwiZXhwIjoxNTg2NTMyNTUwLCJpYXQiOjE1ODU5Mjc3NTB9.m8HSA6zpBSnoPU_q5Met69N6gwLwssdfsdfpw7_orfZ4"
}
```

next api calls must be authorized with token received in the last api call as Bearer token

calculate distance between two geolocation points  _http://localhost:5000/Geo/CalculateDistance_
POST request body:
```
{
  "fromLat" : "LATITUDE",
  "fromLong" : "LONGITUDE",
  "toLat": "LATITUDE2",
  "toLong" : "LONGITUDE2"
}
```

see all previouse geo api calls made by me (USER) : _http://localhost:5000/geo/history_ (GET Request)

**uinsg postman**
all of the above api calls are stored in a postman collection named : _GeoAPI calls.postman_collection.json_ in solution folder
