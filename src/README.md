# LabAPI
## How to run
1. Clone the repository
2. Run the following command in the terminal
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
labapi .
```
In the empty strings, you need to fill in the connection strings and email information. The connection strings are for the Azure Cosmos DB and Azure File Share. The email information is for the email that will be used to send the emails. The email information is optional, but the connection strings are required. The connection strings are used to connect to the Azure services. The email information is used to send emails to the users.
4. The API will be running on http://localhost:5002 with swagger running on http://localhost:5002/swagger/index.html