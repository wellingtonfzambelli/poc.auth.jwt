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
![image](https://github.com/user-attachments/assets/c20a6094-6a59-4646-b621-4f5691fc4a34)

Create the AuthenticationConfig class to configure the middleware
![image](https://github.com/user-attachments/assets/df7044bb-1a45-4b08-8b94-e4b4dab8849b)

Create a AuthContoller and implement a method to get email/password and verify if exists in your database
In this case, I mocked the user object on the constructor 
![image](https://github.com/user-attachments/assets/3aec319d-4ab2-42d7-867f-d94fafa7191e)

Go to the method that you want to protect and assign the "Authorize" decorator
![image](https://github.com/user-attachments/assets/9da606aa-bf3a-4477-a61e-dff28c4cd654)

Run the project with Docker using the dockerfile
![image](https://github.com/user-attachments/assets/30361e8e-b6a1-4dd4-a014-4d6fe46d3a3a)

Here is the swagger with the method to get the token and another one to consume the api information sending the token
![image](https://github.com/user-attachments/assets/997d8661-5792-4d1d-a22b-9ef672b0703b)


Import the postman file uploaded in the root folder
![image](https://github.com/user-attachments/assets/f526c683-e3fd-4bff-b6c8-912eb3eba50c)
![image](https://github.com/user-attachments/assets/2d9dad67-e609-475f-95ec-40f841988ad5)

Postman trying to call method "Forecast" without JWT and receiving a 401 auth error
![image](https://github.com/user-attachments/assets/2713ffda-4d24-4f10-bb85-118fbc9a5a24)

Postman getting the token using the payload
{
    "email": "wellington.zambelli@test.com",
    "password": "123456"
}
![image](https://github.com/user-attachments/assets/d5ac5234-47d2-4475-ad03-64384fdb44fb)

Now we have access to the method "Forecast" using a JWT
![image](https://github.com/user-attachments/assets/b32c7091-57f9-4f03-a25e-8142a8e3b01c)



