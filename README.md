
# Microservices Solution with .NET 8

## Overview
This project demonstrates a microservices architecture using .NET 8. It consists of two Web API services, `RestaurantService.API` and `ItemService.API`. The solution utilizes RabbitMQ for asynchronous communication between the services and employs SQL Server and PostgreSQL as respective databases for each service.

## Services Structure
### 1. RestaurantService.API
- **Purpose**: Manages restaurant-related operations.
- **Database**: SQL Server.
- **Ports**: 
  - HTTP: 5101
  - HTTPS: 5100
- **Key Functionality**: 
  - Handles the creation of new restaurants and publishes the restaurant ID to RabbitMQ.

### 2. ItemService.API
- **Purpose**: Manages item-related operations and maintains references to restaurants.
- **Database**: PostgreSQL.
- **Ports**: 
  - HTTP: 5001
  - HTTPS: 5000
- **Key Functionality**: 
  - Listens for messages from RabbitMQ to register restaurant references.

## Architecture
The project is organized following clean architecture principles:
- **Application**: Contains business logic as use cases services.
- **Domain**: Core domain entities and repository interfaces.
- **Infra**: Handles database interactions and repositories implementation.
- **Controllers**: Exposes endpoints for client interaction.
- **ExternalServices**: Implements RabbitMQ message producers and consumers.
- **Communication**: Contains data mappers and request and response body types.

## Asynchronous Communication
### Message Flow
1. A new restaurant is created in `RestaurantService.API` (via `POST /api/Restaurants`).
2. The service publishes the restaurant ID to RabbitMQ.
3. `ItemService.API` listens for this message and stores the restaurant ID in its database under the `RestaurantReference` entity.

## Prerequisites
- Docker
- RabbitMQ
- .NET 8 SDK

## Setup and Execution
### Step 1: Start RabbitMQ
Run the following command to start RabbitMQ:
```bash
docker run -it --rm --name rabbitmq -p 5672:5672 rabbitmq:4.0-management
```

### Step 2: Start Databases
#### RestaurantService.API (SQL Server)
Use the `docker-compose.yml` file in the `RestaurantService.API` project:
```bash
docker-compose up -d
```
#### ItemService.API (PostgreSQL)
Use the `docker-compose.yml` file in the `ItemService.API` project:
```bash
docker-compose up -d
```

### Step 3: Run the Services
1. Start `ItemService.API` (runs on ports 5000 and 5001).
2. Start `RestaurantService.API` (runs on ports 5100 and 5101).

### Endpoints
#### RestaurantService.API
- **POST /api/Restaurants**: Creates a new restaurant and sends the restaurant ID to RabbitMQ.

#### ItemService.API
- **RabbitMQ Listener**: Listens for new restaurant IDs and stores them as `RestaurantReference` in the database.

## Technologies Used
- **.NET 8**
- **Entity Framework Core**: ORM for database operations.
- **AutoMapper**: Simplifies object-to-object mapping.
- **RabbitMQ.Client**: Manages message queues.

## Docker Configuration
### Dockerfiles
Each project contains a `Dockerfile` to build Docker images for deployment and clustering.
### Docker Compose
- `RestaurantService.API`: Configures a SQL Server instance.
- `ItemService.API`: Configures a PostgreSQL instance.

## Code-First Migrations
Entity Framework Core is used for code-first migrations. The migrations are automatically applied at runtime, eliminating the need for manual commands.

## Project Workflow
1. **Start the services**: Ensure databases and RabbitMQ are running.
2. **Create a restaurant**: Send a `POST` request to `RestaurantService.API`.
3. **RabbitMQ communication**: The restaurant ID is sent to `ItemService.API`.
4. **Reference management**: The restaurant ID is saved in `ItemService.API`.
5. **Add items**: Items can now be linked to existing restaurants in `ItemService.API`.

## Example Workflow
1. Run RabbitMQ and databases.
2. Start `ItemService.API` and `RestaurantService.API`.
3. Send a `POST` request to `http://localhost:5100/api/Restaurants`:
```json
{
  "name": "Restaurant Name",
  "address": "Restaurant Address",
  "siteURL": "Restaurant SiteURL"
}
```
4. Verify that `ItemService.API` saved the restaurant ID in its `RestaurantReference` table.

## Possible Future Enhancements
### Docker Network for Unified Communication
To improve container communication and represent a deployment idea, a custom Docker network can be created. This network will bridge all containers (services, databases, RabbitMQ) together for seamless interaction.

#### Steps to Create a Network
1. Create a custom Docker network:
   ```bash
   docker network create --driver bridge restaurant-bridge
   ```

2. Add all containers to this network by modifying their respective Docker Compose files or Docker run commands.

This setup ensures that all containers can communicate directly within the same network, simplifying configurations and enhancing deployment scalability.
