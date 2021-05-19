# TwitterCodingChallenge

Live Demo: [Here](https://twitter-elonmusk.azurewebsites.net/)

## Up and Running guidelines
1. Install [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0), please select the appropriate SDK based on your OS
2. Open Command Line or Terminal on the codebase folder
3. Run `dotnet restore`
4. Run `dotnet run`

## Technical Architecture
For the purpose of this challenge, I developed it using Blazor Server. Which means UI get rendered in the server; but if user interactions make any changes to the application state, then it will get updated via a realtime pipeline called SignalR. This technology usually gets high latency because every user interaction involves a roundtrip to the server. Hence, it is not suitable for a large scale system.

It is better to adopt Client-Server model or even better Microservice Architecture.

To get top 10 records on a table of millions of records might be tricky if the system does not store any thing like a incremental count that got increased everytime an user click on reweet or got decreased everytime they undo it.

But there are also problems with updating the count keys due to concurrency problems. It can be solved in two solutions.
1. Using Message Queue or Service Bus to maintain the count keys, periodically update the keys in Cache Server
2. Using background jobs or worker services to reconcile the count keys in case of downtime, incidents and disaster.

