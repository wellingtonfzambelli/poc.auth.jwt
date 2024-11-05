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
![image](https://github.com/user-attachments/assets/3aec319d-4ab2-42d7-867f-d94fafa7191e)

Go to the method that you want to protect and assign the "Authorize" decorator
![image](https://github.com/user-attachments/assets/9da606aa-bf3a-4477-a61e-dff28c4cd654)

Run the project with Docker using the dockerfile

Here is the swagger with the method to get the token and another one to consume the api information sending the token
![image](https://github.com/user-attachments/assets/1e3aa6c6-9d64-4426-916f-e5ee2c990227)

Postman trying to call method "Forecast" without JWT and receiving a 401 auth error
![image](https://github.com/user-attachments/assets/fa9a81c2-abe4-4de7-9c5a-e1aaec81c9c0)

Postman getting the token using the payload
{
    "email": "wellington.zambelli@test.com",
    "password": "123456"
}
![image](https://github.com/user-attachments/assets/f0f00c0c-21ad-4a94-9e41-05ce5a675915)

Now we have access to the method "Forecast" using a JWT
![image](https://github.com/user-attachments/assets/93e857e5-0191-442e-a314-6baa61c05cb9)

