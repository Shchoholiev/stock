# Stock Management Application

A desktop application designed for efficient stock management, utilizing the Onion (Clean) Architecture with .NET Core and WPF.

## Table of Contents

- [Features](#features)
- [Stack](#stack)
- [Installation](#installation)
  - [Prerequisites](#prerequisites)
  - [Setup Instructions](#setup-instructions)
- [Configuration](#configuration)
- [Examples](#examples)

## Features

- **Stock Management**: Efficiently manage stock items, including adding, updating, and deleting records.
- **User Interface**: Intuitive and responsive UI built with WPF.
- **Architecture**: Implements Onion (Clean) Architecture for maintainability and scalability.
- **Database Integration**: Utilizes Entity Framework Core with MSSQL Database for data persistence.

## Stack

- **Programming Language**: C#
- **Frameworks**:
  - .NET Core
  - WPF (Windows Presentation Foundation)
  - Entity Framework Core
- **Database**: MSSQL

## Installation

### Prerequisites

- **.NET Core SDK**: Ensure that the .NET Core SDK is installed on your machine. You can download it from the official [.NET download page](https://dotnet.microsoft.com/download).
- **MSSQL Server**: Install and configure MSSQL Server. You can download it from the [Microsoft SQL Server download page](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

### Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/Shchoholiev/stock.git
   cd stock
   ```

2. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the Application**:
   ```bash
   dotnet build
   ```

4. **Run the Application**:
   ```bash
   dotnet run
   ```

## Configuration

- **Database Connection**: Configure the connection string in the `appsettings.json` file located in the `Stock` project directory. Update the `ConnectionStrings` section with your MSSQL Server details:
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
    }
  }
  ```

## Examples

- **Adding a New Stock Item**:
  - Navigate to the "Add Stock" section in the application.
  - Enter the required details such as item name, quantity, and price.
  - Click the "Save" button to add the item to the database.

- **Updating an Existing Stock Item**:
  - Select the item from the stock list.
  - Modify the necessary fields.
  - Click the "Update" button to save the changes.

- **Deleting a Stock Item**:
  - Select the item from the stock list.
  - Click the "Delete" button to remove the item from the database.
