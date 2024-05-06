## GENERAL REQUIREMENTS

- Entity Framework DbContext (using code first or DB first)
- Dependency Injection
- Repository Design Pattern (Generic Repository)
- Data Annotations
- DTOs (or AutoMapper)
- Authentication Login and Register using tokens and Hashing Password
- DB design (Schema) is required

## SPECIFIC REQUIREMENTS

- Actors (User/Admin)
- Authentication
- Authorization
- CRUD Operations

## SYSTEM USERS

### Admin

- Login/ Logout
- Manage new accounts (accept or reject)
- View Rides’ Results and feedbacks
- Block drivers with low rates

### Passenger

- Login/ Logout/ Register
- Search for rides and view drivers profiles
- Book rides based on availability
- Rate the driver and provide feedback
- Pay for the ride
- Show rides’ history

### Driver

- Login / Logout / Register
- Accept/Decline received ride requests
- Get total income for all rides in specific day or month