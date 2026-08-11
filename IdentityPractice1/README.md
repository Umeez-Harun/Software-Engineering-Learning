# IdentityPractice1

A learning project built with **ASP.NET Core MVC** to practice authentication and authorization using **ASP.NET Core Identity**.

## Purpose

This project focuses on learning and implementing:

* ASP.NET Core Identity
* Authentication and authorization
* Role-based access control
* HTTPS
* Anti-forgery protection (XSRF/CSRF)
* Login and registration

## Application

The application contains four main pages:

1. **Register** — user registration
2. **Login** — user authentication
3. **Admin Dashboard** — accessible to Admin users
4. **Accounting Dashboard** — accessible to Accountant users

Two application roles are used:

* `Admin`
* `Accountant`

## Solution Structure

```text
IdentityPractice1/
├── Entities/
├── ServiceContracts/
├── Services/
└── IdentityPractice1/
```

### Entities

Contains the ASP.NET Core Identity entities and database context:

* `ApplicationUser`
* `ApplicationRole`
* `ApplicationDbContext`

`ApplicationDbContext` inherits from:

```csharp
IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
```

rather than the regular `DbContext`.

### ServiceContracts

Currently contains:

* Login DTO
* Registration DTO
* Input validator
* `UserTypeOptions` enum for application roles

### Services

The Services project currently exists as part of the solution structure but does not contain any implemented services yet.

### IdentityPractice1

The main ASP.NET Core MVC project containing:

* Controllers
* Razor Views
* Application configuration
* Static files

## Technologies

* C#
* ASP.NET Core MVC
* ASP.NET Core Identity
* Entity Framework Core
* SQL Server
* Razor Views

## Learning Focus

The purpose of this project is to understand how authentication, authorization, Identity, role-based access, anti-forgery protection, and HTTPS work together in an ASP.NET Core application.
