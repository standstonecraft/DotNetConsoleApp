# DotNetConsoleApp
.NET Framework and SQL Server console app sample with VS Code

If you create a .NET Framework project (not .NET Core),
auto-generated `launch.json` will not work
because that is template for .NET Core app.  
`launch.json` in this project will work😎

This project include tests in test folder.
So `tasks.json` is defferent from default.

## Contents of this sample project

- Testing and debugging
  - xUnit
  - setup / teardown
  - Ominisharp runsettings
- Logging by NLog
- Database
  - SQL Server
  - Object mapping by Dapper
  - DAO(Data Acces Object) generator
- Config file(.json)
- Embedding files and load
