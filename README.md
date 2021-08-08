# Pokedex API

## Prerequisite

Install .NET 5 SDK: https://dotnet.microsoft.com/download

## Running locally via dotnet cli - Development

From `Pokedex/Pokedex.API/` directory:

Run `dotnet run` from terminal

Naviagate to `https://localhost:5001/swagger` to explore and use API.

## Running with docker - Production

From `Pokedex/` directory:

`docker build --no-cache -f Pokedex.API/Dockerfile -t pokedex .`

`docker run -d -p 8080:80 --name pokemonapi pokedex`

API is now running at `http://localhost:8080`

Can be seen at: `http://localhost:8080/pokemon/mewtwo`

## TODO

- Give option for app to throw exception if translation service fails
- More unit tests
- More logging
- Rate limit API
- Version API
- Add postman file for testing
- Add certs for running in docker over HTTPS
- Add config to Azure App Config
- Format description to remove line breaks etc.
