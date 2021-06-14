# Problem1

Framework used: .net core 3.1

The solution has 4 projects:

BlurbTemplate.Api - Provides an endpoint to use the word replacing functionality via an http POST message
BlurbTemplate.Interfaces - Set of interfaces that a client would require for a word replacing service to implement
BlurbTemplate.Implementation - The word replacing implementation, implements the required interfaces
BlurbTemplate.Implementation.Tests - Tests for the word implementation

To quickstart:

Open a Windows Terminal or Console in the BlurbTemplate.Api folder
Execute the command dotnet run
Open Postman -> New Request -> POST https://localhost:5001/api/template/replace
Put Header Content-Type to application/json
Put Body type raw
Paste in the body the next json
{
    "template":"This <plc> works",
    "placeholder":"<plc>",
    "replacement":"word replacer"
}

Press Send



