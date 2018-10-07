#Documentation

Services are injected by their interfaces. It allows to write unit tests witch is how this project can be improved.
Soap calls are done in ExchangeRateService. Caching is implemented on soap call level. It is the responses of soap that will be cached.
ExchangeRatesController returns data to client.
All calculations are done in API and Client only shows them to user.

##API

API uses framework .NET Core 2.1
It is written with Visual studio 2017
To launch API go to `\exchange-rates\Api` and open Api.sln
Build & Run the API project

##Client

Client is written with React

1. Build & Run the API project
2. From the NodeJS command line, navigate to `\exchange-rates\Client`
3. Run command `npm install`
4. Run command `npm start`
5. Client should open with url `localhost:3000`