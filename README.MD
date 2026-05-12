# Applicant Tracking System API

## 📖 Overview

ATS is a simplified Applicant Tracking System (ATS) built as a backend Web API using ASP.NET Core and PostgreSQL.

This project focuses on the team-side recruitment workflow, where recruiters and hiring managers collaborate on candidate applications by:

- Reviewing applications  
- Scoring candidates  
- Adding internal notes  
- Tracking stage transitions  

Each action is attributed to a team member to ensure accountability in the hiring process.

---

## 🛠 Tech Stack

- ASP.NET Core Web API  
- Entity Framework Core  
- PostgreSQL  
- Docker  
- xUnit (Testing)  

---

## Running the Project (Docker) in the ATS FOLDER

From the root project directory 


- docker compose build
- docker compose up -d


This will:

- Build the API container
- Start the application
- Start the PostgreSQL database

## Running Tests

Navigate to the root folder and run:
- dotnet test


## Database Setup & Seeding

The application uses EF Core migrations.

To set up the database manually:
Run the command below in the ATS folder.
- dotnet ef database update


## Seed Data
Initial seed data for team members is configured in Data/AppDbContext using EF Core seeding.

# Future Improvements

## Given more time, I would:

- Refactor GET /api/application/{id} to reduce nested projections and improve query readability
- Add authentication and role-based access control (RBAC)




## ⚠️ Known Limitations / Incomplete Areas

Due to time constraints, one requirement in Part Two was not fully completed.

Priority was given to delivering a stable and complete implementation of the core ATS workflow, including application management, scoring, notes, and stage transitions.

Given the remaining time, I chose to focus on ensuring correctness and completeness of the implemented features rather than partially implementing the remaining requirement.

This is a deliberate trade-off to ensure the submitted solution is consistent, functional, and well-structured.