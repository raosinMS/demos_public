# SpeakToSQL

## Pre requisites

### Azure DB
1. Create a single Azure SQL database according to [tutorial](https://learn.microsoft.com/en-us/azure/azure-sql/database/single-database-create-quickstart?view=azuresql&tabs=azure-portal)
    - Set Database Name to Northwindsales
    - In the Section "Data Source" for the property "Use existing data" select "None" 

2. Connect to Azure SQL database through Query editor on Azure portal (according to tutorial mentioned above) or use SSMS 
- Run T-SQL commands from file [NorthwindSales_for_AzureSqlDB.sql](NorthwindSales_for_AzureSqlDB.sql) to create tables and insert data into tables
    - If you have an issue with connection check if under dbserver there is public access and your local IP address is added to firewall rules

3. In [ExecuteSQL.cs](SpeakToSQL.ConsoleUI/ExecuteSQL.cs) provide connection string to your Azure DB 

### Azure OpenAI
- Create or use existing Azure OpenAI service
- Provide Key and Endpoint inside [appsettings.Development.json](SpeakToSQL.API/appsettings.Development.json)

## Demo

The demo contains two .NET applications:
- SpeakToSQL.API - contains endpoints that are responsible for generating full prompts to OpenAI
- SpeakToSQL.ConsoleUI - gives you interaction with API to provide prompts and quesy SQL data

If using VS Code run first:
```
dotnet dev-certs https --trust
```

1. Using VS Code
- Open new terminal and navigate to SPeakToSQL.API and execute "dotnet run" command
- Open new terminal and navigate to SpeakToSQL.ConsoleUI and execute "dotnet run" command
- Start providing you promts

Notes:
- This application does not store context of previous prompts if you want to precise your query after previous execution you need to provide full updated version of initial prompt


### If you want to use your own database for this demo
 1. Export database script for your database via SQL Server Management Studio through context menu "Generate scripts".
      - Choose "Select specific database objects" and mark all tables in database
      - If you want to script also indexes for tables or other database objects, click on button Advanced and change settings for script creation.        
      - Select "Save as script file" with option "Single script file" and filename "DBScript.sql"
 2. Remove T-SQL commands unnecessary for OpenAI context from exported script file
      - Decrease number of tokens, which will be used by system message of OpenAI. To achieve this, remove all rows containing comments (beginning with /* and ended with */). Remove also all rows, which contains commands "SET" and remove also followed lines with command "GO".
 3. Overwrite file DBScript.sql in folder \OpenAISQL\SpeakToSQL.API\Scripts with file DBscript.sql prepared in previous steps. 
 4. In ExecuteSQL.cs provide connection string to your database.
