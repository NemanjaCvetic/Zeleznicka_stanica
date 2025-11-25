#  Railway Station Management System

A comprehensive ASP.NET MVC web application for managing railway station operations, train schedules, and ticket reservations with an intuitive interface for passengers and administrators.

## Overview

This Railway Station Management System (Železnička stanica) provides a complete solution for railway operations. Passengers can search train schedules, view route details, and book tickets online, while administrators manage the entire railway network including schedules, stations, and user accounts.

##  Core Features

**Passenger Experience**  
Search for trains by departure station, destination, and date with real-time results showing available seats, ticket prices, and platform information. Make instant reservations and manage your bookings through a personalized dashboard that tracks all your upcoming trips.

**Administrative Control**  
Administrators have complete oversight of the system, managing user accounts with role-based permissions, creating and updating train schedules, maintaining station information, and monitoring all reservations across the network. Staff members can update train statuses, delays, and cancellations in real-time.

**Smart Reservation System**  
The application automatically tracks seat availability, prevents overbooking, and provides instant confirmation. Passengers receive clear information about their reservations and can easily cancel when needed.

## Technology Stack

Built with **ASP.NET MVC 5.2.7** and **Entity Framework 6** for robust data management with SQL Server. The frontend combines **Bootstrap 3** for responsive design with **jQuery** for dynamic user interactions. Authentication is handled through ASP.NET Forms Authentication with a custom role provider implementing the repository pattern for clean architecture.

## User Roles

The system supports three distinct roles: **Putnik (Passenger)** for regular users making reservations, **Radnik (Staff)** for railway employees with operational privileges, and **Admin** for complete system management.

## Database Architecture

Using Entity Framework's Database-First approach, the system models real-world railway operations with entities representing Users, Stations, Trains, Schedules, Routes, and Reservations. The schema efficiently handles complex relationships between stations, intermediate stops, and train capacity management.

## Getting Started

**Prerequisites:** Visual Studio 2019+, SQL Server 2012+, and .NET Framework 4.7.2

**Quick Setup:** Create a SQL Server database, update the connection string in Web.config, restore NuGet packages in Visual Studio, and run the application. Register a new account to start using the system (admin privileges require database assignment).

## Project Architecture

The application follows MVC best practices with clearly separated Controllers for request handling, Models for business logic and data access through the repository pattern, and Views using Razor for dynamic content rendering. The EFRepository folder contains Entity Framework models, while shared layouts ensure consistent UI across the application.

## Security

Built with security in mind, featuring password-protected accounts, role-based authorization, anti-forgery tokens on all forms, secure session management, and parameterized queries to prevent SQL injection attacks.




