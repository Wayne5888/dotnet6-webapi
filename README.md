# dotnet6-webapi
This is a service template for dotnet6 + webapi + docker
Api infrastructure have mutiple function e.g : Azure application insight / entity framework / exception handling / JWT Authorization / Swagger / logger implement

#How to use it?
1. Replace the app insight InstrumentationKey & database connection string in appsetting.json
   ![image](https://github.com/Wayne5888/dotnet6-webapi/assets/63963809/128b84f4-1021-40ac-b7e2-cdda7a160eb1)

2. Use commands to sync entity framework to remote database.
   -1 "dotnet ef migrations add {update(this is name)}"
   -2 "dotnet ef database update" to apply database change.

3. Use command "docker build -t dotnetwebapi ." to build up docker image.

4. Use command "docker run -d -p 8080:5001 --name containername dotnetwebapi" to run the command.

5. access to (http://localhost:8080/swagger/index.html), you may see seagger service is running.
![image](https://github.com/Wayne5888/dotnet6-webapi/assets/63963809/dc4c6b29-35ec-4534-95b0-35c2e368122f)



* Why log the file as a .txt in /log/*.txt but not remote service?
   *This is a demo service, for production/test service, you may push the log to remote for better analysis.
   *Log including request/response/entity framework logs/Exception logs/http status.

* Why you don't mix entity framework command in dockerfile?
  *This is a demo service, for better understand and flexible(some people may not want to use docker), seperate the service from dockerfile.







