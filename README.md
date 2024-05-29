# Ecom_API_latest
# create a new repository on the command line
echo "# Ecom_API_latest" >> README.md
git init
git add README.md
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/ItsAmaan09/Ecom_API_latest.git
git push -u origin main

# push an existing repository from the command line
git remote add origin https://github.com/ItsAmaan09/Ecom_API_latest.git
git branch -M main
git push -u origin main

# project refernce :-
https://dotnettutorials.net/lesson/e-commerce-real-time-application-using-asp-net-core-web-api/

# Add solution
dotnet new sln

# Add class library
dotnet new classlib -o "name"

# Add the library project to the solution:
dotnet sln add StringLibrary/StringLibrary.csproj


# CMD for Webapi
dotnet new webapi --use-controllers -o TodoApi
cd TodoApi

# Add package
dotnet add package Microsoft.EntityFrameworkCore.InMemory
code -r ../TodoApi


# webapi tutorial:-
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code


