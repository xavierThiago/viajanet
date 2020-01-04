# ViajaNet's Fullstack Developer Job Application Project

A simple analytics API, that sends hits to the server, composed of a RabbitMQ pub/sub mechanism consumed by two databases, SqlServer and CouchDb, at regular intervals.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

To build and run the project, you'll need to have installed theses tools:
1. .NET 2.2 SDK and Runtime;
2. NPM 2.0+;
3. NodeJS 8+;
4. Docker.

### Installing

To prepare your local machine there are just **four** steps:

1. Create your local NPM assets:

```
npm install
```

2. Run GruntJs tasks to perform HTML, CSS and JS bundling to your local **/dist** folder:

```
grunt dist
```

3. And, finnally, you can build your project with .NET Core SDK:

```
dotnet build
```

4. Start containers of RabbitMQ, Microsoft SqlServer and CouchDb:

```
docker run -d -p 5672:5672 --name awesome-queue --hostname localhost rabbitmq

docker run -d -p 8091:8091 --name couchdb couchdb

docker run -d -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=just-testing' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

## Running the tests

There are simple back-end tests made with XUnit. You can simply run a command line:

```
dotnet test
```

Collecting code coverage information (at least 50%) is just as easy, running this command, instead:

```
dotnet test /p:CollectCoverage=true /p:Threshold=50 /p:ThresholdType=method /p:CoverletOutputFormat=opencover
```

Also, you can use makefile rules of this project. Be aware that these rules only work on Linux and macOS systems:

```
make cbt
```

## Deployment

For now, deployment is not ready. In the future, a GitHub Action will be createad and mantained.

## Built With

* [.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) - Cross-platform .NET implementation for websites, servers, and console apps on Windows, Linux, and macOS.
* [NPM](https://www.npmjs.com/) - Dependency Management
* [GruntJs](https://gruntjs.com/) - Task management
* [Docker](https://www.docker.com/) - Build containerized apps

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/xavier/viajanet/tags).

## Authors

* **Thiago J. Xavier** - *Initial work* - [xavierThiago](https://github.com/xavierThiago)

## License

This project is licensed under the Apache License, Version 2.0 - see the [LICENSE](LICENSE) file for details

## Acknowledgments

* Althought not much used on this project, I'd like to thanks [Rafael Cruzeiro](https://github.com/rcruzeiro) for inspiring me on DDD development.
