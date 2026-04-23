EstateBid Microservices Platform

EstateBid is a scalable microservices-based auction platform where users can create auctions, place bids, and search listings in real-time. The system is designed using modern distributed architecture principles with a strong focus on reliability, scalability, and eventual consistency.

 Architecture Overview

The system is built using a Microservices Architecture, where each service is independently deployable and responsible for a specific business capability.

Core Services
Auction Service
Manages auction creation and lifecycle.
Bidding Service
Handles bid placement and bid-related operations.
Search Service
Provides optimized querying and search capabilities using a dedicated read database.
Notification Service
Sends real-time updates for auction events.
Identity Service
Handles authentication and authorization.
API Gateway
Central entry point for routing client requests.
 Tech Stack
Backend: ASP.NET Core (.NET 8)
API Gateway: YARP (Yet Another Reverse Proxy)
Authentication: Duende IdentityServer
Inter-Service Communication: gRPC
Message Broker: RabbitMQ
Message Bus: MassTransit
Databases:
Auction DB (Write Model)
Search DB (Read Model)
 Key Features & Flow
  1. Auction Creation Flow
Seller creates an auction via Auction Service
Auction is stored in AuctionDB
Event is published via RabbitMQ (MassTransit)
Search Service consumes the event and stores data in SearchDB

Ensures eventual consistency between services

 2. Data Consistency (Inbox/Outbox Pattern)

To prevent data inconsistency:

Implemented Inbox/Outbox Pattern
Guarantees reliable message delivery
Ensures data sync between AuctionDB and SearchDB even in failure scenarios
 3. Bidding Flow with gRPC Fallback
When placing a bid:
If auction data is missing in Bidding DB
Bidding Service calls Auction Service using gRPC
Fetches auction details using .proto contract

 Ensures resilience and fault tolerance

 4. Event-Driven Updates
When a bid is placed or auction ends:
Event is published via RabbitMQ
Auction Service & Search Service act as consumers
Both databases are updated accordingly
 5. API Gateway
Implemented using YARP
Routes incoming requests to appropriate services
Acts as a single entry point
 Authentication & Authorization
Implemented using Duende IdentityServer
Centralized identity management for all services
 Highlights
✅ Microservices architecture with clear service boundaries
✅ Event-driven communication using RabbitMQ
✅ gRPC-based synchronous communication
✅ Inbox/Outbox pattern for reliability
✅ API Gateway with YARP
✅ Scalable and fault-tolerant design
