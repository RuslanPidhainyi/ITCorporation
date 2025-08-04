# 🏢 ITCorporations — FullStack Web Application

## 🎯 Project Objective

The goal of this project is to demonstrate a complete working example of a full-stack web application, including seamless interaction between frontend and backend, RESTful API communication, database operations (including stored procedures), and automated testing with both unit and integration coverage.

---

## ⚙️ Technologies Used

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

### 👷Pattern
- **Data Transfer Object**
- **Repository**
- **Client-Server Architecture**

---

## 🔗 Implemented API Endpoints

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

## 🧪 Backend Tests

### ✅ Unit Tests
- `GetAllProjects_ReturnsAllProjects`

### ❌ Integration Tests
- `CreateProject`
- `CreateProject_ShouldFail_When...` (fails due to validation mismatch)

---

## 🖥️ Frontend Features

- **Project List Page** — Displays all projects in a table format with actions to edit or delete.
- **Add Project Page** — Form to create a new project with name and status fields.
- **Edit Project Page** — Allows updating the project name and status.
- **Project Details Page** — Shows the project along with assigned employees and their roles.

---

## 📷 UI Preview

> The screenshots below show some of the implemented UI components:

- Project List with Status and Actions  
- Add Project Form  
- Edit Project Form  
- Project Details with Assigned Employees

---

## ✅ Conclusion

**ITCorporations** is a complete example of a CRUD-based enterprise app that includes:
- Full frontend-backend interaction
- Client-Server Architecture
- Automated tests
- Real-world practices like stored procedures, REST APIs, and component-based frontend development.

