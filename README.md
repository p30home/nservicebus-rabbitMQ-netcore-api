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
