# Sample architecture for distributed workflows
This repository contains a sample architecture for distributed workflows with microservices. 

Consists in the implementation of a sales workflow (buy side) for a ficticious client "ChickenPower", an small energy provider which buys energy from independent generators and sells to business consumers.

The workflow is the following:
1. A new client wants to sell their energy to Chicken power.
1. Chicken power registers in the CRM system all client details including characteristics of the power generation system.
1. Generates a draft contract with T&Cs for the product selected by the client.
1. Chicken power calculates the price for that energy.
1. The client accepts/rejects the offer.
1. The contract is registered in Chicken power’s portfolio.

## Tech stack
* .NET Core
* MassTransit: service bus
* MassTransit.Automatonymous: state machine library.
* Quartz.NET: scheduler.
* Autofac, Serilog,….
* RabbitMQ: message bus.
* SeqLogging: centralised structured logging server.
* Docker: for service containerisation.

## Instructions to run it
This sample architecture is self-contained and doesn't require installing any external components as these are containerised and included in the docker-compose file in the solution.
1. Clone this repository.
1. Start Docker.
1. Open solution in Visual Studio.
1. Build and run the solution with the docker-compose.yaml file as starting project.

The solution will start four different microservices and two external dependencies: RabbitMQ and SeqLogging.
The RabbitMQ admin console can be accessed with your browser on port 15672, whereas SeqLogging is on port 89.

## Demo
The workflow actions are triggered by the BackendForFrontend service.
1. To kick off the workflow, send a POST request to this service with route `/api/proposal/new` including an id in the body. This creates a new instance of the workflow which is uniquely identified by the id provided.
1. The interaction between different microservices can be seen from SeqLogging, including the state of each workflow instances.
Once the workflow has been started, it will be waiting for a request to generate a price for the contract. To do that, send another POST request to `/api/proposal/price` including in the body same id sent in the previous request.
This step can be repeated multiple times until the client is happy with the calculated price, which can be seen in SeqLogging.
1. Finally, we can finish the execution of the workflow by accepting the price. To do so send another POST to `/api/proposa/accept` including the id of the workflow in the body.
1. Try now creating another instance of the workflow by repeating step 1. and play with multiple instances running simultaneously in different stages.
