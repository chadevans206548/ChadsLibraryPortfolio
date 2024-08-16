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
- I had to make a call on where to draw the line on user roles/claims and API side authentication. I ended up implementing the roles on the front end which I thought was more appropriate for a demo app. The front end menu changes depending on role, and if you memorize a URL and navigate there with the wrong role, you will get handed off to a forbidden page.