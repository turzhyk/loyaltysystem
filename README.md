## About
It's an API that is used for customer loyalty program.

![image](diagram.webp)
## Features
- User account creation
- Coupon activation
- Observation of user's current (used or active) discounts
- Cart price calculation for individual user based on global discounts, specific discounts, coupons and vouchers
- Adding and managing global discounts, coupons, vouchers

## Installation and Starting Up
1. `git clone github.com/turzhyk/loyaltysystem`
2. `cd loyaltysystem`
3.  `docker compose up`
## Installation
   `git clone github.com/turzhyk/loyaltysystem`

## Structure
This API uses Clean Architecture including layers:
- API (controllers, middleware)
- Application (services, calculators, exceptions, DTOs)
- Domain (domain models, enums)
- Infrastructure (database contexts, entities, seeders)

## Technologies and Tools
- .NET 8
- Strategy & Factory Patterns (to divide and encapsulate calculators for different Discount Types)
- EF Core (for convenient interactions with DB)
- Docker with Postgres container (database)
- Moq + xUnit (for unit tests)

