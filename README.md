# ChadsLibraryPortfolio
This application is a simple library scenario to serve as an example of a full-stack approach to development. Angular front end, C# WebApi middle tier, SQL backend.

### Prereqs and tools
- Visual Studio Community 2022 configured for IIS Express use
- Fluent Validation, Automapper, Swagger
- Net core 8
- SQL Express + SSMS
- VS Code
- Angular CLI: 18.1.1
- Node: 20.15.1
- Package Manager: npm 10.7.0

### To get this up and running...
1. I used SQL express and that connection string is found in the appsettings.json file in the API.WebAPI project. Running the project will create the db and apply migrations.
2. I run the API through Visual Studio IIS Express. I have the project set to open a browser window to swagger.
3. The project uses Bogus to seed the db upon startup. If you delete records and want to start with a fresh set of seed data, just delete all the data and re-run the project. As long as there are records, Bogus will leave the data as is. 
4. For the front end, I use a VS Code terminal, npm install, ng serve. I used all default ports that Angular CLI typically generates.

### A few notes
- For the front end I just used the basic angular cli setup and angular 18 schematics for most of what you see. This UI is a long way from anything nice. I didn't address responsive design with Sass, or deal with ADA compliance, or spend much time making error messages show in toasters, or theme anything. But it is functional and simple enough to show some RxJs usage, RESTful get/post/put/delete. It will deliver API validations to the front end (try to add/edit a book title as a duplicate for example). I did not implement the usual web features like confirmation dialogs or telemetry or logging. 

### Updates
- In order to add authentication at the controller/action level, I removed the nswag open api implementation from the startup and replaced it with the swagger implementation instead (adding the fliter nuget package to get the swagger UI to pass the bearer token). I updated the front-end to include the bearer token in the header and the content-type where necessary. Since the front end already discriminated actions by login, I test the web API authentication on the controller actions using swagger. Here are the steps I use:
  - Ensure you have registered a customer user and a librarian user through the front end app (or you can use swagger endpoints, but I find the app easier myself)
  - From swagger (https://localhost:44316/swagger/index.html), notice the Authorize button unlocked.
  - Find the Books > Delete action, and try it out with any valid book Id. You will receive a 401.
  - Find the Accounts > Login action and use your customer user. Copy the resulting bearer token.
  - Return to the Authorize button, click that, paste in your token.
  - Return to the Books > Delete action and try it again. This time you will receive a 403 instead, since only a Librarian can delete a book.
  - Return to the Authorize button, click that, logout. 
  - Find the Accounts > Login action and use your librarian user. Copy the resulting bearer token.
  - Return to the Authorize button, click that, paste in your token.
  - Return to the Books > Delete action and try it again. This time you will receive a 200 and you can also check the db to ensure the book was deleted.