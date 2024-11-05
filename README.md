# About
Simple code showing how to generate a JWT

# Stacks of this project
- .NET 8
- Swagger
- Web Api
- Authorize/Authentication
- JWT
- Docker
- Postman _(for API testing)_
- Visual Studio 2022

# Generate/Use JWT
Install the nuget package
![image](https://github.com/user-attachments/assets/107bc07d-15d7-4558-9a51-6df1907280a7)

Right click on the project and let's add a Secret Key in a "secrets.json" to simulate security envioroment server
![image](https://github.com/user-attachments/assets/f5a40f48-bd21-41ab-9e71-a533637428cf)

Add a secret key that will be used to generate and validate the token
![image](https://github.com/user-attachments/assets/5ce1690e-8050-4125-8fd4-7bcbbe3dc617)

Create TokenProvider class to implement the token logic
![image](https://github.com/user-attachments/assets/013bec87-cd86-4bb7-9502-b641515bfd00)

Create a AuthContoller and implement a method to get email/password and verify if exists in your database
In this case, I mocked the user object on the constructor 
![image](https://github.com/user-attachments/assets/e44b7037-6aa8-4283-8a41-4528f714250c)

Go to the method that you want to protect and assign the "Authorize" decorator
![image](https://github.com/user-attachments/assets/9da606aa-bf3a-4477-a61e-dff28c4cd654)

Postman trying to call method without JWT and receiving a 401 auth error
![image](https://github.com/user-attachments/assets/fa9a81c2-abe4-4de7-9c5a-e1aaec81c9c0)


