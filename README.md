# 🏢 ITCorporations — FullStack Web Application

---

## Contents 📋

0. [Project Objective](#intro)
1. [Technologies Used](#techn)
2. [Diagrams](#diagrams)
3. [Project architecture](#arch)
4. [Implemented API Endpoints](#endpoints)
5. [Backend Tests](#tests)
6. [Frontend Features](#frontend_features)
7. [UI Preview](#ui_preview)
8. [Conclusion](#conclusion)
9. [Instructions for installing the application locally](#init)

---

<a name="intro"></a>
## 0. Project Objective 🎯

The goal of this project is to demonstrate a complete working example of a full-stack web application, including seamless interaction between frontend and backend, RESTful API communication, database operations (including stored procedures), and automated testing with both unit and integration coverage.

---

<a name="techn"></a>
## 1. Technologies Used ⚙️

### 🔧 Backend
- **ASP.NET Core 9**, **C#**
- **Entity Framework Core** – used for database read operations
- **Stored Procedures (PostgreSQL)** – used for updating project data
- **PostgreSQL** – relational database
- **MSTest** – for unit and integration tests

### 🌐 API
- **RESTful API**
- **Swagger** – for API documentation and testing
- **Postman** – for manual API testing

### 💻 Frontend
- **Angular 17**
- **HTML**, **TypeScript**, **CSS**

### 👷Patterns
- **Data Transfer Object**
- **Repository**
- **Client-Server Architecture**
- **AAA**

---

<a name="diagrams"></a>
## 2. Diagrams 📊

### Diagram Class (UML)

![alt text](source/DiagramClass.png)
 
### Entity–Relationship Diagram (ERD)

![alt text](source/ERD.png)

---

<a name="arch"></a>
## 3. Project architecture 📑

```
├── 📁 .git/ 🚫 (auto-hidden)
├── 📁 .vscode/ 🚫 (auto-hidden)
├── 📁 API/
│   ├── 📁 Controllers/
│   │   ├── 🟣 EmployeesController.cs
│   │   └── 🟣 ProjectsController.cs
│   ├── 📁 DTOs/
│   │   ├── 🟣 CreateUpdateEmployeeDto.cs
│   │   ├── 🟣 CreateUpdateProjectDto.cs
│   │   ├── 🟣 EmployeeDto.cs
│   │   ├── 🟣 ProjectDto.cs
│   │   └── 🟣 ProjectWithEmployeesDto.cs
│   ├── 📁 Data/
│   │   └── 🟣 AppDbContext.cs
│   ├── 📁 Entities/
│   │   ├── 🟣 Employee.cs
│   │   ├── 🟣 EmployeeDetails.cs
│   │   ├── 🟣 Employee_Project.cs
│   │   └── 🟣 Project.cs
│   ├── 📁 Helpers/
│   │   └── 🟣 AutoMapperProfile.cs
│   ├── 📁 Interfaces/
│   │   ├── 🟣 IEmployeeRepository.cs
│   │   └── 🟣 IProjectRepository.cs
│   ├── 📁 Properties/
│   │   └── 📄 launchSettings.json
│   ├── 📁 Repositories/
│   │   ├── 🟣 EmployeeRepository.cs
│   │   └── 🟣 ProjectRepository.cs
│   ├── 🟣 API.csproj
│   ├── 📄 API.http
│   ├── 🟣 Program.cs
│   ├── 📄 appsettings.Development.json
│   └── 📄 appsettings.json
├── 📁 API.Tests/
│   ├── 📁 Integration/
│   │   ├── 📁 Controller/
│   │   │   └── 🟣 ProjectsControllerTests.cs
│   │   └── 🟣 IntegrationTestClassBase.cs
│   ├── 📁 Unit/
│   │   ├── 📁 Controller/
│   │   │   └── 🟣 ProjectsControllerTests.cs
│   │   └── 🟣 UnitTestClassBase.cs
│   ├── 🟣 API.Tests.csproj
│   └── 🟣 MSTestSettings.cs
├── 📁 Client/
│   ├── 📁 .vscode/ 🚫 (auto-hidden)
│   ├── 📁 src/
│   │   ├── 📁 app/
│   │   │   ├── 📁 _models/
│   │   │   │   ├── 📄 employee.model.ts
│   │   │   │   └── 📄 project.model.ts
│   │   │   ├── 📁 _services/
│   │   │   │   ├── 📄 employee.service.ts
│   │   │   │   └── 📄 project.service.ts
│   │   │   ├── 📁 projects/
│   │   │   │   ├── 📁 add-project/
│   │   │   │   │   ├── 🎨 add-project.component.css
│   │   │   │   │   ├── 🌐 add-project.component.html
│   │   │   │   │   └── 📄 add-project.component.ts
│   │   │   │   ├── 📁 edit-project/
│   │   │   │   │   ├── 🎨 edit-project.component.css
│   │   │   │   │   ├── 🌐 edit-project.component.html
│   │   │   │   │   └── 📄 edit-project.component.ts
│   │   │   │   ├── 📁 project-details/
│   │   │   │   │   ├── 🎨 project-details.component.css
│   │   │   │   │   ├── 🌐 project-details.component.html
│   │   │   │   │   └── 📄 project-details.component.ts
│   │   │   │   └── 📁 project-list/
│   │   │   │       ├── 🎨 project-list.component.css
│   │   │   │       ├── 🌐 project-list.component.html
│   │   │   │       └── 📄 project-list.component.ts
│   │   │   ├── 🎨 app.component.css
│   │   │   ├── 🌐 app.component.html
│   │   │   ├── 📄 app.component.ts
│   │   │   ├── 📄 app.config.ts
│   │   │   └── 📄 app.routes.ts
│   │   ├── 📁 assets/
│   │   │   └── 📄 .gitkeep
│   │   ├── 🖼️ favicon.ico
│   │   ├── 🌐 index.html
│   │   ├── 📄 main.ts
│   │   └── 🎨 styles.css
│   ├── 📄 .editorconfig
│   ├── 🚫 .gitignore
│   ├── 📖 README.md
│   ├── 📄 angular.json
│   ├── 📄 package-lock.json
│   ├── 📄 package.json
│   ├── 📄 tsconfig.app.json
│   ├── 📄 tsconfig.json
│   └── 📄 tsconfig.spec.json
├── 📁 Database/
│   ├── 🖼️ ERD-ITCorporationDb.png
│   └── 🗄️ ScriptSQL-ITCorporation.sql
├── 📁 Presentations/
│   └── 📊 Presentation_Backend_Database.pptx
├── 📁 source/
│   ├── 🖼️ DiagramClass.png
│   ├── 🖼️ ERD.png
│   ├── 🖼️ image-1.png
│   ├── 🖼️ image-2.png
│   ├── 🖼️ image-3.png
│   ├── 🖼️ image-4.png
│   ├── 🖼️ image-5.png
│   ├── 🖼️ image-6.png
│   ├── 🖼️ image-7.png
│   └── 🖼️ image.png
├── 📄 .gitattributes
├── 🚫 .gitignore
├── 🟣 ITCorporation.sln
└── 📖 README.md
```

---

<a name="endpoints"></a>
## 4. Implemented API Endpoints 🔗

### 👨‍💼 Employees

| Method | Endpoint                        | Description                  |
|--------|----------------------------------|------------------------------|
| GET    | `/api/Employees`                | Get all employees            |
| POST   | `/api/Employees`                | Create a new employee        |
| GET    | `/api/Employees/{id}`           | Get employee by ID           |
| PUT    | `/api/Employees/{id}`           | Update employee              |
| DELETE | `/api/Employees/{id}`           | Delete employee              |

### 📁 Projects

| Method | Endpoint                                                             | Description                                      |
|--------|----------------------------------------------------------------------|--------------------------------------------------|
| GET    | `/api/Projects`                                                     | Get all projects                                 |
| POST   | `/api/Projects`                                                     | Create a new project                             |
| GET    | `/api/Projects/{id}`                                                | Get project by ID                                |
| PUT    | `/api/Projects/{id}`                                                | Update project (via stored procedure)            |
| DELETE | `/api/Projects/{id}`                                                | Delete project                                   |
| GET    | `/api/Projects/{id}/with-employees`                                 | Get project along with assigned employees        |
| POST   | `/api/Projects/{projectId}/employees/{employeeId}`                 | Assign employee to project                       |
| DELETE | `/api/Projects/{projectId}/employees/{employeeId}`                 | Remove employee from project                     |
| PUT    | `/api/Projects/simulate-not-found-project/{id}`                    | Simulate not found error for testing             |
| POST   | `/api/Projects/simulate-bad-request-create-project`                | Simulate bad request on project creation         |

---

<a name="tests"></a>
## 5. Backend Tests 🧪

### ✅ Unit Tests
- `GetAllProjects_ReturnsAllProjects`

### ❌ Integration Tests
- `CreateProject`
- `CreateProject_ShouldFail_When...` (fails due to validation mismatch)

---

<a name="frontend_features"></a>
## 6. Frontend Features 🖥️

- **Project List Page** — Displays all projects in a table format with actions to edit or delete.
- **Add Project Page** — Form to create a new project with name and status fields.
- **Edit Project Page** — Allows updating the project name and status.
- **Project Details Page** — Shows the project along with assigned employees and their roles.

---

<a name="ui_preview"></a>
## 7. UI Preview 📷

> The screenshots below show some of the implemented UI components:

- Project List with Actions  

![alt text](source/image.png)

- Add Project  

![alt text](source/image-1.png)

- Edit Project

![alt text](source/image-3.png)

- Project Details with Assigned Employees

![alt text](source/image-5.png)

---

<a name="conclusion"></a>
## 8 Conclusion ✅

**ITCorporations** is a complete example of a CRUD-based enterprise app that includes:
- Full frontend-backend interaction
- Client-Server Architecture
- Automated tests
- Real-world practices like stored procedures, REST APIs, and component-based frontend development.

---

<a name="init"></a>
## 9. Instructions for installing the application locally ☕️

### I. Install Programs

1. Download Visual Studio Code  
https://code.visualstudio.com/download

2. Download Node.js  
https://nodejs.org/en/download

2.1. Check if Node is installed (PowerShell terminal command):  

``` node --version ```

3. Download .NET SDK 9.0  
https://dotnet.microsoft.com/en-us/download

4. Download DBeaver  
https://dbeaver.io/download/

5. Download PostgreSQL Server  
https://www.enterprisedb.com/downloads/postgres-postgresql-downloads

6. Download Git  
https://git-scm.com/downloads

7. VS Code Extensions:  
- .NET Install Tool (by Microsoft) 

![alt text](source/image-2.png)

- C# (by Microsoft)  

![alt text](source/image-4.png)

- C# Dev Kit (by Microsoft)  

![alt text](source/image-6.png)

- Angular Language Service (by Angular)

![alt text](source/image-7.png)

### II. Build the Project

1. Clone the repository in VS Code:  
https://dmytrokiselyov0389@dev.azure.com/dmytrokiselyov0389/Dev%20Workshop%20DK/_git/Dev%20Workshop%20DK

2. Go to the Client directory inside the project using the PowerShell command:

``` cd Client ```

3. Install dependencies inside the Client directory (PowerShell command):

``` npm install ```

3.1. Check npm dependencies inside the Client directory (PowerShell command):

``` npm -version ```

4. Install Angular CLI globally inside the Client directory (PowerShell terminal command

``` npm install -g @angular/cli ```

4.1. Check Angular dependencies inside the Client directory (PowerShell command):

``` ng version ```

5. If the Migrations folder exists inside API/Data, delete it.

![alt text](folder-migration.png)

6. In DBeaver, create a new PostgreSQL database.

7. Connect the database to the server by editing appsettings.json in the API directory:
- Set the database name
- Set the username
- Set the password

7. Run the first database migration (PowerShell terminal):

7.1. Navigate to the backend (API directory):

   ``` cd API ```
   
7.2. Initialize the migration; this will create a Migrations folder inside Data:

``` dotnet ef migrations add InitialCreate --output-dir Data/Migrations ```

7.3. Apply the migration to update the database:

``` dotnet ef database update ```

8. Seed the database:

8.1. Use the SQL script found in the Database folder of the cloned project.

8.2. In DBeaver, create a new SQL script file and paste the contents from the Database folder.

8.3. Run the script to insert initial data (via INSERT INTO) required for the project to work properly.

8.4. Also run the stored procedure script so the project works correctly.
   
III. Run the Project

1. To run the backend server, navigate to the API directory and use this command:

``` dotnet run ```

2. Once the server is running, open Swagger in your browser:

https://localhost:7067/swagger/index.html

3. To run the frontend client, navigate to the Client directory and use the command:

``` ng serve ``` or ``` npm run ```


