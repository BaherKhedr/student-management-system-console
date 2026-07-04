# Student Management System (Console Application)

## Overview

A C# Console Application built to manage student records using a clean layered architecture and Entity Framework Core.

The application allows users to perform full CRUD operations, search and filter students, display statistics, and store data in a SQL Server database.

This project was created as a learning project to practice C#, Object-Oriented Programming, LINQ, Delegates, Dependency Injection, and Entity Framework Core.

---

## Features

### Student Management

- Add a new student
- Update student information
- Delete student by ID or Name
- Search by:
  - ID
  - Name
  - Grade (Higher Than / Lower Than / Equal)
- Display all students
- Sort students by:
  - ID
  - Grade
- Filter students by Age

---

### Statistics

- Student with the highest grade
- Student with the lowest grade
- Average grade
- Total number of students
- Number of passed students
- Number of failed students

---

### Validation

- Prevent duplicate IDs
- Age validation
- Grade validation
- Input validation
- Friendly error messages

---

## Technologies Used

- C#
- .NET
- Entity Framework Core
- SQL Server
- LINQ
- Delegates (`Func<T, bool>`)
- Dependency Injection
- Fluent API
- EF Core Migrations
- Object-Oriented Programming (OOP)

---

## Project Structure

```
StudentManagementSystem
│
├── Data
│   ├── AppDbContext.cs
│   └── AppDbContextFactory.cs
│
├── Config
│   └── StudentConfiguration.cs
│
├── Models
│   └── Student.cs
│
├── Services
│   └── StudentService.cs
│
├── Helpers
│   ├── ConsoleHelper.cs
│   └── InputHelper.cs
│
├── Enums
│   └── MenuOptions.cs
│
├── Migrations
│
├── Program.cs
│
└── appsettings.json
```

---

## Concepts Practiced

- Object-Oriented Programming (OOP)
- Separation of Concerns
- Delegates
- LINQ
- Entity Framework Core
- Fluent API Configuration
- Dependency Injection
- Design-Time DbContext Factory
- CRUD Operations
- SQL Server Integration
- Input Validation
- Console Application Design

---

## How to Run

1. Clone the repository

```bash
git clone https://github.com/your-username/student-management-system.git
```

2. Open the solution in Visual Studio.

3. Update the connection string inside:

```
appsettings.json
```

4. Apply the database migrations:

```bash
dotnet ef database update
```

5. Run the project.

---

## Example Menu

```
Student Management System

[1] Add Student
[2] Search Student
[3] Delete Student
[4] List Students
[5] Update Student
[6] Statistics
[7] Exit
```

---

## Future Improvements

- Repository Pattern
- Generic Repository
- Unit of Work
- Asynchronous EF Core Methods
- DTOs
- Logging
- Unit Testing
- ASP.NET Core Web API
- Authentication & Authorization
- Clean Architecture

---

## Author

**Baher Khedr**

Junior .NET Developer

---

## What I Learned

While building this project I practiced:

- Designing applications using OOP principles.
- Working with LINQ to query collections and databases.
- Using Delegates to reduce duplicated code.
- Applying Dependency Injection in .NET applications.
- Configuring Entity Framework Core using Fluent API.
- Creating and applying EF Core Migrations.
- Understanding the difference between Runtime and Design-Time DbContext creation.
