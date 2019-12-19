# trip-booking-saga

The main purpose of the created application is to deepen knowledge in the field of microservice orchestration and system design using the [saga pattern](https://microservices.io/patterns/data/saga.html). The Saga pattern describes how to solve distributed (business) transactions without two-phase-commit as this does not scale in distributed systems. The basic idea is to break the overall transaction into multiple steps or activities. Only the steps internally can be performed in atomic transactions but the overall consistency is taken care of by the Saga. The Saga has the responsibility to either get the overall business transaction completed or to leave the system in a known termination state. So in case of errors a business rollback procedure is applied which occurs by calling compensation steps or activities in reverse order. You can find more information about Sagas here [Saga: How to implement complex business transactions without two phase commit](https://blog.bernd-ruecker.com/saga-how-to-implement-complex-business-transactions-without-two-phase-commit-e00aa41a1b1b) or [Transactions and Failover using Saga Pattern in Microservices Architecture](https://medium.com/@so3da/transactions-and-failover-using-saga-pattern-in-microservices-architecture-baf5a13111c9)

Application consists of microservices:
- ``Reservations.Api`` - API Gateway
- ``Reservations.Services.CarsRental`` - Car reservation service
- ``Reservations.Services.Hotels`` - Hotel reservation service
- ``Reservations.Services.Flights`` - Flights reservation service
- ``Reservations.Transactions`` - Distributed transaction management service

## Technology stack:
- [Chronicle](https://github.com/snatch-dev/Chronicle) - manage distributed transactions
- [Jaeger](https://www.jaegertracing.io/) - distributed tracing
- [RabbitMQ](https://www.rabbitmq.com/) as a message broker
- [Autofac](https://autofac.org/) as [IoC](https://martinfowler.com/articles/injection.html) container

I encourage you to track the entire flow using debug mode. You can experimentally modify the code and throw, for example, an exception in places where the logic of flight or hotel booking is included, and then follow the cancellation process from the saga. The most important part of the system is [TripReservationSaga.cs](https://github.com/koniecznyp/trip-booking-saga/blob/master/src/Reservations.Transactions/Sagas/TripReservationSaga.cs), which contains the logic of the booking process and supports rollback in case of failure. The entire process is tracked using [Jaeger](https://www.jaegertracing.io/). The event log related to each step of the operation is available at  ``http://localhost:16686/``
