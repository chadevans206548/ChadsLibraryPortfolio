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
3. To seed the database, I use swagger, navigate to the TestData controller and execute from there. I was fighting with the OnModelCreating running twice upon startup and decided finally that my time was better served moving the Bogus generation somewhere else than fix that obscure bug.
4. For the front end, I use a VS Code terminal, npm install, ng serve. I used all default ports that Angular CLI typically generates.

### A few notes
- In hindsight, I probably spent too much time API side. However, controllers, interfaces, services, validations and data contexts are all set up and ready for more front end work.
- I opted to get functionality in place first and ran out of time to get to ASP.Net Identity, which was new to me. I was able to get the migrations in place, and some work in the startup.cs file, but that's all.
- For the front end I just used the basic angular cli setup and angular 18 schematics for most of what you see. The featured books page works. Book detail is somewhat functional showing some properties and reviews, but no checkout button. I didn't get to the search functionality, so that page just lists all books.
- For the cover page image, I intended to provide a local image per book in an asset folder, so that is why the Bogus data is set up to use a URL there.
