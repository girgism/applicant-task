# Task Submission – Backend Implementation

## Overview
This repository contains the backend implementation for the interview task.  
The backend is fully functional and follows **Clean Architecture** principles to ensure maintainability, testability, and separation of concerns.

> ⚠️ The frontend part is not completed due to limited time.  
> The focus of this submission is on the backend structure and functionality.

---

## 🧱 Architecture Overview

The project follows **Clean Architecture**, structured into distinct layers:

### 1. **Domain Layer**
- Contains core business logic and domain entities.
- Completely independent of any external frameworks.
- Defines domain models and business rules.

### 2. **Application Layer**
- Contains use cases (application logic) and interfaces for repositories.
- Uses **MediatR** for CQRS pattern (if applicable).
- Handles validation, DTOs, and mapping.

### 3. **Infrastructure Layer**
- Responsible for data access and external integrations.
- Implements repository interfaces using **Entity Framework Core**.
- Manages database migrations, configurations, and connection strings.

### 4. **API Layer (Presentation)**
- ASP.NET Core Web API project.
- Exposes endpoints to interact with the application layer.
- Uses **Dependency Injection** to link all layers.

---

## ⚙️ Technologies Used
- **.NET 8.0**
- **Entity Framework Core**
- **MediatR** (for CQRS pattern)
- **FluentValidation**
---

## 🧩 How to Run

### Prerequisites
- Install **.NET 8.0 SDK** or higher  
  [Download .NET 8 here](https://dotnet.microsoft.com/download/dotnet/8.0)
- (Optional) SQL Server or LocalDB if using EF migrations

### Steps
1. Clone the repository  
   ```bash
   git clone https://github.com/girgism/applicant-task.git

