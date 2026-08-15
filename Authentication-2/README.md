# Authentication-2

A **practice ASP.NET Core application** built primarily to implement and understand **authentication, authorization, ASP.NET Core Identity, and global exception handling**.

> **Note:** This is a learning/practice project and is **not intended to be production-grade software**.

## Features

* Login and sign-up functionality
* Role-based authorization with **Admin** and **Employee** roles
* Separate dashboards for Admins and Employees
* Admin-only employee account creation
* Employee information management
* Employee CRUD operations:

  * Add
  * View
  * Delete
* Dedicated error page for unhandled exceptions
* Global exception handling
* HTTPS enabled
* XSRF/CSRF protection using `ValidateAntiForgeryToken`
* One API endpoint for role setup/testing

## Authentication & Authorization

The application uses **ASP.NET Core Identity** for authentication and role management.

Account and role-related logic provided by Identity was abstracted into an `AccountService` rather than being placed directly inside controllers.

Authentication uses **cookie-based authentication** for session security. **JWT authentication is not used.**

### Account Creation Flow

Employee accounts cannot be created by employees themselves.

1. An Admin creates an account for an employee.
2. The Admin assigns the account one of the following roles:

   * Admin
   * Employee
3. The Admin then creates the corresponding employee record, including:

   * Name
   * Identification Number
   * The `UserId` of the newly created Identity account
4. The `UserId` establishes the association between the employee record and its Identity account.
5. The employee can subsequently sign in and access their own dashboard.

## Architecture

The application separates responsibilities into service classes:

* **`AccountService`** — handles authentication, account management, and role assignment.
* **`EmployeeService`** — handles employee-related business logic and CRUD operations.

The primary business domain implemented in this project is **Employee Management**.

The application uses **Entity Framework Core** for database access and configuration.

The UI is implemented using **Razor Views**, with **Areas** used to organize functionality based on roles.

## Employee Management

The employee functionality provides basic CRUD operations:

* Add employee
* View employees
* Delete employees

No employee search functionality is implemented.

## API

A single API endpoint is included for **role setup/testing purposes**.

The endpoint is intentionally not protected by authorization because it was created for practicing API development. It is **not intended to represent a production-secured API endpoint**.

## Security

This project uses:

* **ASP.NET Core Identity** for authentication and role management
* **Cookie-based authentication** for session security
* **Role-based authorization**
* **HTTPS**
* **XSRF/CSRF protection** using `ValidateAntiForgeryToken`
* **Global exception handling**

JWT authentication is **not** used in this project.

## Technologies

* C#
* ASP.NET Core
* ASP.NET Core MVC / Controllers
* ASP.NET Core Web API
* ASP.NET Core Identity
* Entity Framework Core
* SQL Server
* Razor Views
* Areas
* Cookie Authentication
* HTTPS
* XSRF/CSRF Protection
* Global Exception Handling

## Purpose

This project was created as a hands-on practice project to understand how **authentication, authorization, ASP.NET Core Identity, role-based access, service-layer abstraction, Entity Framework Core, exception handling, and web security features** work together in an ASP.NET Core application.
