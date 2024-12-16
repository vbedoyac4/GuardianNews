# Guardian News Project

A comprehensive solution for displaying, fetching, and managing news content from The Guardian, consisting of a modern web application (UI) and a robust backend service.

## Database Scripts Documentation

This repository contains SQL scripts for setting up two databases: Guardian DB and Hangfire DB.

### Database Structure

#### Guardian Database (`guardian_db`)
Contains tables for storing news articles and search parameters.

#### Tables:
1. `news`
   - `id` (varchar 150) - Primary Key
   - `date` (date) - News article date
   - `category` (varchar 80) - News category
   - `title` (varchar 300) - Article title
   - `link` (varchar 300) - Article URL
   - `elements` (varchar 20) - Optional elements

2. `search_params`
   - `id` (int) - Primary Key
   - `type` (varchar 45) - Parameter type
   - `value` (varchar 45) - Parameter value

#### Hangfire Database (`hangfire_db`)
Used for job scheduling and background processing.

### Setup Instructions

1. Create Guardian Database:
mysql -u root -p < guardian-db-scripts.sql

2. Create Hangfire Database:
mysql -u root -p < hangfire-db-scripts.sql


## Frontend: Guardian News UI

A modern web application for displaying news content from The Guardian.

### Features

Browse latest news articles

Search functionality

Responsive design

Category filtering

Prerequisites

Node.js (v14 or higher)

npm (v6 or higher)

### Installation

#### Clone the repository:

git clone https://github.com/vbedoyac4/GuardianNews

Install node modules:

npm install

### Development

Start the development server. The application will be available at http://localhost:8080:

npm run serve

### Project Structure

guardian-news-ui/
├── src/             # Source files
├── public/          # Static files
├── dist/            # Production build
└── node_modules/    # Dependencies

### Technologies Used

Vue.js

Vue CLI

TypeScript

CSS3

HTML5

## Backend: Guardian News App

A modern application for fetching and managing news from The Guardian API, built with .NET 8.0.

### Project Structure

The solution follows a clean architecture pattern organized into distinct layers:

#### GuardianNewsApp.API

ASP.NET Core Web API project

REST endpoints for the application

Swagger/OpenAPI integration for API documentation

MVC application parts configuration

#### GuardianNewsApp.Application

Core business logic

Application services

Domain models and interfaces

Use cases and business rules

#### GuardianNewsApp.Infrastructure

External services implementation

Data access layer

Third-party integrations

Infrastructure concerns

#### GuardianNewsApp.Worker

Background service implementation

Scheduled tasks and jobs

News fetching and processing


Each project is built with .NET 8.0 and follows a modular architecture for better separation of concerns and maintainability.

### Features

Integration with The Guardian API

Background news fetching and processing

RESTful API endpoints

Swagger/OpenAPI documentation

### Prerequisites

.NET 8.0 SDK

MySQL

The Guardian API key

### Getting Started

Navigate to the project directory:

cd GuardianNewsApp

Restore dependencies:

dotnet restore

Build the solution:

dotnet build

Run the projects:

dotnet run --project GuardianNewsApp.API
dotnet run --project GuardianNewsApp.Worker

API Documentation

Once the API is running, you can access the Swagger documentation at: https://localhost:5001/swagger