# Product Service Management System

## Overview
A C# application for managing products, services, and bundled packages. The solution includes a reusable Core library, a console app, and a WPF desktop UI built with MVVM. Data can be loaded from XML or created manually in the UI, with inline editing and CRUD operations.

## Projects
- `ProductService.Core` - Domain models and managers (products, services, packages)
- `Proiect` - Console app for CLI workflows
- `ProductServiceWpf` - WPF desktop UI (MVVM)

## Key Features
- Create and manage products and services
- Create packages and add products/services to them
- Inline editing directly in DataGrids
- CRUD operations for products, services, and packages
- XML import for initial data
- MVVM separation with commands and validation

## Architecture Highlights
- Core domain models in a separate library
- Managers for products, services, and packages
- MVVM UI layer with view models, commands, and validation
- Data access abstracted through a service interface

## Running the App
- Console: set `Proiect` as Startup Project and run
- WPF UI: set `ProductServiceWpf` as Startup Project and run

## Data
- Sample XML is in `Data/p_s.xml` and is copied to output on build

## Screenshots

<img width="1345" height="753" alt="Screenshot 2026-03-04 120245" src="https://github.com/user-attachments/assets/f82942bb-545d-494b-8e5a-e00cb4512901" />

<img width="1348" height="755" alt="Screenshot 2026-03-04 120627" src="https://github.com/user-attachments/assets/ca9fd1b0-5786-4307-b64d-f876dd78259d" />
