# Rowad El-Teb Hospital

> A digital healthcare platform for Rowad El-Teb Hospital, providing patients with access to hospital information, medical services, clinic appointments, and home medical visit requests.

---

## 📖 Description

**Rowad El-Teb Hospital** is a web-based healthcare platform designed to establish a strong digital presence for the hospital and make its healthcare services more accessible to patients.

The system provides patients with an easy way to explore the hospital's departments, medical services, and doctors, while allowing them to interact with the hospital through online services such as **clinic appointment booking** and **home medical visit requests**.

The backend is developed using **ASP.NET Core 8**, following a scalable and maintainable architecture that separates business logic, data access, and application concerns.

---

## ✨ Features

* 🏥 Hospital information and services
* 🩺 Medical departments and specialties
* 👨‍⚕️ Doctors management
* 📅 Clinic appointment booking
* 🏠 Home medical visit requests
* 👤 Patient management
* 📋 Appointment management
* 🔐 Authentication and authorization
* ✅ Request validation
* ⚠️ Centralized error handling
* 🔄 RESTful APIs
* 🗄️ Database management using Entity Framework Core

---

## 🛠 Technologies

* **ASP.NET Core 8**
* **C#**
* **Entity Framework Core**
* **SQL Server**
* **LINQ**
* **ASP.NET Core Web API**
* **RESTful APIs**
* **Clean Architecture / Onion Architecture**
* **Repository Pattern**
* **Unit of Work**
* **AutoMapper**
* **FluentValidation**
* **JWT Authentication**
* **Dependency Injection**
* **Git & GitHub**

---

## 📂 Project Structure

```text
RowadElTeb
│
├── src
│   ├── Presentation
│   ├── Application
│   ├── Domain
│   ├── Infrastructure
│   └── Persistence
│
├── tests
│
├── docs
│
├── database
│
├── README.md
└── .gitignore
```

---

## 🏗 Architecture

The project follows a **Clean Architecture / Onion Architecture** approach to ensure separation of concerns, maintainability, testability, and scalability.

### Layers

#### Presentation Layer

Responsible for handling HTTP requests, responses, API endpoints, authentication, and communication with the application layer.

#### Application Layer

Contains application business logic, use cases, services, DTOs, validation, and application-level abstractions.

#### Domain Layer

Contains the core business entities, domain rules, interfaces, and business logic independent of external frameworks and technologies.

#### Infrastructure Layer

Contains implementations for external services and infrastructure-related concerns.

#### Persistence Layer

Responsible for database access, Entity Framework Core, DbContext, repositories, migrations, and data configurations.

---

## 🚀 Getting Started

### Prerequisites

Make sure you have the following installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* SQL Server
* Visual Studio 2022 / VS Code
* Git

### Installation

#### 1. Clone the Repository

```bash
git clone <repository-url>
cd RowadElTeb
```

#### 2. Configure the Database

Update the connection string in:

```text
appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=RowadElTebDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

#### 3. Apply Database Migrations

```bash
dotnet ef database update
```

#### 4. Run the Application

```bash
dotnet run
```

The API will be available through the configured application URL.

---

## 📷 Screenshots

Screenshots of the hospital website and application interfaces will be added here.

---

## 🔮 Future Enhancements

The system is designed to be extensible and can be enhanced with additional healthcare services, including:

* Patient dashboard
* Doctor dashboard
* Medical records management
* Online consultations
* Prescription management
* Laboratory services
* Radiology services
* Online payments
* Notifications and appointment reminders
* Admin dashboard
* Advanced reporting and analytics

---

## 📄 License

This project is licensed under the **MIT License**.

---

## 👨‍💻 Author

**Mohamed Saeed**

**Backend .NET Developer**

Developed using **ASP.NET Core 8**, **Entity Framework Core**, and **SQL Server**.
