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