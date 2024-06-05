# LabAPI
## How to run
1. Clone the repository
2. Execute the following command in the terminal
```zsh
  docker build -t LabAPI .
```
3. When the build is complete, run the following command
```zsh
  docker run -p 5002:5000 -p 5001:5001 \
  -e ASPNETCORE_URLS=http://+:5000 \
  -e ASPNETCORE_HTTP_PORT=https://+:5001 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e AZURECOSMOSDB_CONNECTIONSTRING="" \
  -e AZUREFILESHARE_CONNECTIONSTRING="" \
  -e EMAIL_ADDRESS="" \
  -e EMAIL_PASSWORD="" \
  -e EMAIL_SMTP="" \
  -e FRONTEND_URL=""
  labapi .
```
4. In the empty strings, you need to provide the connection strings and email information. The connection strings are for Azure Cosmos DB and Azure File Share. The email information is for the account that will be used to send emails. While the email information is optional, the connection strings are mandatory. The connection strings are used to establish connections to the Azure services. The email information is used to send emails to users.
The API will be accessible at http://localhost:5002 with Swagger UI available at http://localhost:5002/swagger/index.html

## Endpoints
Every endpoint starts with /api
The API has the following endpoints:
### Customer
**POST** - /customer/register - Registers a new customer
**POST** - /customer/login - Logs in a customer
**PATCH** - /customer/changepassword - Changes the password of a customer

### Worker
**GET** - /worker - Gets all workers

**POST** - /worker/register - Registers a new worker

**POST** - /worker/login - Logs in a worker

**PATCH** - /worker/resetPassword/{email} - Resets the password of a worker

**PATCH** - /worker/changePassword - Changes the password of a worker

### Test
**GET** - /tests - Gets all tests (without specific markers)

**GET** - /tests/{id} - Gets a test by id

**GET** - /tests/page - Gets a page of tests

**POST** - /tests - Creates a new test

**PUT** - /tests/{id} - Updates a test

**DELETE** - /tests/{id} - Deletes a test

## Order
**GET** - /order - Gets page of orders

**GET** - /order/{orderNumber} - Gets an order by order number

**POST** - /order - Creates a new order

**PUT** - /order/{orderNumber} - Updates an order

**DELETE** - /order/{orderNumber} - Deletes an order

**PATCH** - /order/{orderNumber} - Updates results of an order

**GET** /order/pesel - Gets order pdf by pesel

**PATCH** /order/{orderNumber}/acceptResults - Accepts the results of an order

## Authentication
The API uses JWT for authentication. The JWT token is required for every request except for the registration and login endpoints. The token is passed in the Authorization header in the format "Bearer {token}".

## Database
The API uses Azure Cosmos DB as the database. The connection string to the database must be provided in the environment variable AZURECOSMOSDB_CONNECTIONSTRING. The database has the following collections:
* Customers - Contains information about customer accounts - partition key is id
* Orders - Contains information about orders - partition key is orderNumber
* Tests - Contains information about tests - partition key is shortName
* Workers - Contains information about worker accounts - partition key is id

## File Storage
The API uses Azure File Share to store files. The connection string to the file share must be provided in the environment variable AZUREFILESHARE_CONNECTIONSTRING. Directories are created automatically when needed.

## Email
The API uses email to send notifications to users. The email information must be provided in the environment variables EMAIL_ADDRESS, EMAIL_PASSWORD, and EMAIL_SMTP. The email information is used to send emails to users. The email information is optional.

## Frontend
The API uses the frontend to display the user interface. The frontend URL must be provided in the environment variable FRONTEND_URL. The frontend URL is used to redirect users to the frontend.

## Frontend repository
The frontend repository is available at [repository](github.com/kamildobkowski/LabAPI.Web)
