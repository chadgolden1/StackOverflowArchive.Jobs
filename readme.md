# StackOverflowArchive.Jobs
Reference application of a job scheduler using Quartz with ASP.NET Core worker service integration. References a 2010 version of the StackOverflow SQL Server database from the Stack Exchange Network.

## Concepts
* Quartz for scheduled jobs
* ASP.NET Core worker service w/ Quartz integration
* Vertical slice architecture w/ MediatR
* EF Core 5 and Dapper for data access
* Reverse engineered EF Core 5 DbContext w/ scaffold script
* xUnit test project w/ EF Core 5 InMemory provider and Moq

## Tools
* Visual Studio 2022 Preview
* .NET 6 Preview

## Reference Links
[Quartz: Open-source job scheduling system for .NET](https://www.quartz-scheduler.net/)
[Andrew Lock: Using Quartz.NET with ASP.NET Core and Worker Services](https://andrewlock.net/using-quartz-net-with-asp-net-core-and-worker-services/)
[Brent Ozar: How to Download the Stack Overflow Database](https://www.brentozar.com/archive/2015/10/how-to-download-the-stack-overflow-database-via-bittorrent/)
[Stack Exchange Data Dump Details and License](https://archive.org/details/stackexchange)