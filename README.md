# Pokemon API
Weird Pokemon Rest API using .Net 6.

## Before you start
Be sure to have .NET 6 installed:
- https://dotnet.microsoft.com/download/dotnet/6.0

## Nuget packages required
- Newtonsoft.Json 13.0.1
- Swashbuckle(Swagger) 6.2.3
- Prometheus-net 6.0.0
- XUnit 2.4.1
- Moq 4.18.0

## Things outside of my control

We depend upon these public APIs - no responsiblity is taken for their reliability or availability. Additionally, both have very small limits on usage. Please be thoughtful in how you use the API.
- https://funtranslations.com/api/shakespeare
- https://pokeapi.co/api/v2


## Running the API
### Via source
Clone the project locally. In the command prompt, run "dotnet run" in this folder: {path}/PokemonApi/PokemonApi. This will start the api running, and will tell you which ports the API is running on.

Use the https url appended with swagger (eg. https://localhost:24248/swagger) to view generated swagger docs for the endpoints

### Via Docker
TODO

## API Doc
There are two GET endpoints to this API. Both take the same parameter and return a similar object, with fields as follows:
  - Name  (string)
  - Description  (string)
  - Habitat  (string)
  - IsLegendary  (boolean)

### Endpoint 1 - normal details
  pokemon/<name>/ returns the details for the named Pokemon

### Endpoint 2 - translated details
   pokemon/translated/<name> returns identically to endpoint 1, however will translate the description into either Yoda speak, or Bard speak. Yoda if the Pokemon lives in a cave or is legendary, otherwise it's a one-way ticket to Stratford-upon-Avon.
  
### Bonus endpoint - Prometheus Metrics
  Head to url/metrics to see some fun metrics around the machine the service is running on, and the details and counts of calls made to the API endpoints.
   
## Running Unit Tests
  Run via the command prompt. Run "dotnet test" in this path: \PokemonApi\PokemonApi.Test. Alternaively, run all tests via Visual Studio.
  Be aware, there is a .runsettings file in the root directory to advise the test runner to run on multiple threads. This is required for a test against the threadsafety of the cache, but also speeds up the run time.
  
  Coverage (at time of writing) sits at 83% across the API code.
  ![image](https://user-images.githubusercontent.com/46675810/168822262-2ec3e7dd-e810-4ac6-a580-b2ac93a321fc.png)

## The future... how to productionise?
  - Version the API to support major changes
  - Improve logging (actually add some) for debugging and support
  - Host via the cloud, allow for greater scale and uptime
  - Monitor heathchecks to ensure low downtime
  - Hook up to static code analyser and third party library checker. Avoids bugs, code smells & vulnerabilities
   
