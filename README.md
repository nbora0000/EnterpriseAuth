# Enterprise Application API

## Overview

This API provides secure access and token management for enterprise applications. It supports generating secrets and tokens to facilitate authentication and authorization.

## Table of Contents

- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
  - [Get Secret](#get-secret)
  - [Generate Token](#generate-token)
  - [Generate Secret](#generate-secret)
- [Usage Examples](#usage-examples)
- [Error Handling](#error-handling)
- [Contributing](#contributing)
- [License](#license)

## Getting Started
This section provides an overview of how to set up the application.

### Prerequisites

- **.NET 8**
- **Microsoft SQL Server**
- **.NET SDK**

### Installation

 **Clone the repository:**

   ```bash
   git clone https://github.com/nbora0000/enterprise-auth-api.git
   cd enterprise-auth-api
```
# API Setup and Usage

## Setup

1. **Restore Dependencies**:
```bash
   dotnet restore
```
3. **Configure Database Connection**:
   Edit your `appsettings.json` to set up your database connection.

4. **Run the Application**:
   ```bash
   dotnet run
   ```
   The API will be accessible at: 'https://localhost:44368`.

## API Endpoints

### Get Secret

- **Method**: `GET`
- **URL**: `https://localhost:44368/api/Secret/GetSecret`
- **Headers**:
  ```bash
  ClientId: string
  ```
- **Description**: Retrieves a secret associated with the specified client ID.

### Generate Token

- **Method**: `POST`
- **URL**: `https://localhost:44368/api/AuthToken/GenerateToken`
- **Request Body**:
```bash
{ "clientId": "abe950cc-6d87-4955-9c0c-24b0e7d773e5", "clientSecret": "string", "audId": "string" }
```

- **Description**: Generates an authentication token for the specified client.

### Generate Secret

- **Method**: `POST`
- **URL**: `https://localhost:44368/api/Secret/GenerateSecret`
- **Headers**:
  ```bash
  AudId: string
  ```
- **Description**: Generates a new secret associated with the specified audience ID.

## Usage Examples

### Get Secret
```bash
curl -X GET https://localhost:44368/api/Secret/GetSecret -H "ClientId: your-client-id"
```


### Generate Token
```bash
curl -X POST https://localhost:44368/api/AuthToken/GenerateToken
-H "Content-Type: application/json"
-d '{ "clientId": "abe950cc-6d87-4955-9c0c-24b0e7d773e5", "clientSecret": "your-client-secret", "audId": "your-audience-id" }'
```

### Generate Secret
```bash
curl -X POST https://localhost:44368/api/Secret/GenerateSecret -H "AudId: your-audience-id"
```


## Error Handling

- **400 Bad Request**: Invalid request parameters.
- **401 Unauthorized**: Invalid client credentials.
- **404 Not Found**: Resource not found.
- **500 Internal Server Error**: Unexpected server error.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License
This application code is governed by Apache License 2.0

