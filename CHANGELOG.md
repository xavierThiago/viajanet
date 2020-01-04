# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.14.0] - 2020-01-04
### Added
- Docker instructions on README.md;
- Implementation of application service in queue consumption job.

### Fixed
- IQueueProvider generics restriction.

## [0.13.2] - 2020-01-04
### Fixed
- Typo on README.md.

## [0.13.1] - 2020-01-04
### Fixed
- Typo on README.md.

## [0.13.0] - 2020-01-04
### Added
- Project and repository information to the community.

## [0.12.0] - 2020-01-04
### Changed
- API resource documentation.

## [0.11.2] - 2020-01-04
### Fixed
- Again id type on several layers.

## [0.11.1] - 2020-01-04
### Fixed
- This changelog.

## [0.11.0] - 2020-01-04
### Added
- SqlServer service layer;
- CouchDb implementation.

### Fixed
- Repository handler contracts.

## [0.10.0] - 2020-01-03
### Added
- Added an response interface to the API (forces developer to ensure consistence);

### Changed
- Analytics payload separated into two categories: request and response.

### Fixed
- Id type throughout the application layers.

## [0.9.0] - 2020-01-03
### Added
- Entity framework (SqlServer) simple structure;
- Analytics entity that will be persisted;
- Several convenience methods to convert payload, DTO and entity back and forth;
- SqlServer DI on container;
- .nuspec in SqlServer and CouchDb projects;
- SqlServer connection string.

### Fixed
- Lack of id property in AnalyticsDto;

## [0.8.0] - 2020-01-03
### Added
- Repository handler that supports commands and queries;
- Added configuration data in appSettings.json.

### Fixed
- CouchDbConfiguration error in connection string property;

## [0.7.0] - 2020-01-03
### Added
- CouchDb factory and service implementation;
- Added configuration data in appSettings.json.

### Changed
- Refactored providers and services to supress garbage collector on disposal.

## [0.6.0] - 2020-01-03
### Added
- Controller's application service dependency injection;
- Queue configuration in API appSettings.js.

### Changed
- Simplified queue provider factory dispose method.

## [0.5.0] - 2020-01-03
### Added
- .nuspec for extensions project.

### Changed
- Application layer. Now, it has two projects: core and service;

### Fixed
- Missing extensions project language version specification.

## [0.4.0] - 2020-01-03
### Added
- Application service layer contracts;
- Transfer objects that will be used by the API.

### Changed
- Dependency injection through extensions pattern.

### Removed
- Unecessary environment appSettings.json files;
- Namespace clean up.

## [0.3.0] - 2020-01-02
### Added
- Get by ID operation on analytics controller.

### Fixed
- Parameters collection data type (AnalyticsPayload.cs);
- AnalyticsPayload constructor and unit tests;
- Typo on vn-analytics.js making API unreachable.

## [0.2.0] - 2020-01-02
### Added
- Swagger generation tool;
- Added ViajaNet's logo;
- Added some Swagger metadada in analytics controller.

### Fixed
- .nuspec icon not being generated correctly;
- Typo in vn-analytics.js API address.

## [0.1.0] - 2020-01-02
### Added
- Debug profile to console project (Worker);
- License.txt to be compatible with NuGet packaging;
- .nuspec of queue infrastructure (it can be packable and referenced through NuGet);
- Some configuration in appSettings.json;
- Queue consumption job implementation;
- Primitive DI container implementation (will be transported in a separated file later);
- Created an interface to the queue factory (dependency injection);

### Changed
- Queue service class no longer receives a channel from DI container. Now, it will be constructed with each new instance;
- Better format (markdown) in API resources descriptions (request/response).

## [0.0.18] - 2020-01-01
### Added
- Queue service implementation;
- Proper disposing of queue provider factory.

### Changed
- Queue provider factory to lazy load a singleton connection.

### Removed
- Async methods of queue provider (for now, it's not necessary as things are simple and fast).

## [0.0.17] - 2020-01-01
### Added
- Queue provider factory;

### Changed
- Renamed some files.

### Fixed
- Package reference typo.

## [0.0.16] - 2020-01-01
### Added
- Initial queue provider project;
- Initial application service project.

## [0.0.15] - 2019-12-30
### Changed
- Also deleting dist folder in Makefile's "dist" and "purge" rules.

### Fixed
- Main .css file path;
- Updated Newtonsoft.Json version to solve MSBuild conflict;
- Grunt tasks were wrongly outputing compiled files.

### Removed
- Unused Grunt tasks;
- Unecessary package references.

## [0.0.14] - 2019-12-29
### Added
- Application layer (backbone);
- Infrastructure layer (backbone);
- Common API responses [success and error (backbone)];

### Fixed
- Namespace typo.

## [0.0.13] - 2019-12-29
### Added
- New makefile rule to initiate and distribute front-end assets.

## [0.0.12] - 2019-12-29
### Added
- NPM configuration;
- Grunt tasks to cover .js and .css minification and bundling;
- Dockerignore file.

## [0.0.11] - 2019-12-29
### Added
- Dockerignore file;
- Some documentation.

## [0.0.10] - 2019-12-29
### Added
- Worker project to solution file and test project;
- Unit test of job execution.

### Changed
- Job execution trigger now fires every ten seconds;
- Refactored scheduler creation to a factory.

## [0.0.9] - 2019-12-29
### Added
- Logging initial configuration (later, the interface will be injected in application services);
- Explicit URL binding on initialization (Docker port binding);
- Worker project backbone, responsible for queue consumption at a later development stage;
- Some documentation.

### Fixed
- Docker file runtime image.

## [0.0.8] - 2019-12-29
### Added
- API's Dockerfile.

## [0.0.7] - 2019-12-28
### Changed
- Some enhancements in analytics script.

## [0.0.6] - 2019-12-28
### Added
- JSHint directives in vn.analytics.js.

### Changed
- Analytics script now has Apache (version 2.0) licensing.

### Fixed
- Unused wrapper variable in vn-analytics.js.

## [0.0.5] - 2019-12-28
### Added
- Remaning HTML structure [(backbone), but still have work to do].

### Changed
- Analytics script now hits on page load.

## [0.0.4] - 2019-12-28
### Added
- Remaining HTML pages (backbone);
- Unit tests for AnalyticsPayload class;
- CORS configuration at Startup;
- RESTful documentation scratches;
- API call in main front-end script.

### Changed
- Decreased code coverage threshold to 50% and changed analysis method to "method";
- Assets naming;

## [0.0.3] - 2019-12-27
### Added
- Analytics script, responsible for server hits with client information;
- Initial HTML template.

## [0.0.2] - 2019-12-27
### Added
- RESTful API initial structure;
- Website initial structure;
- Makefile's purging rule now deletes all "bin" and "obj" folders, in any project;
- Git ignore to SonarQube and coverage files.

### Changed
- Makefile

### Fixed
- Makefile's rule commenting typo.

### Fixed
- Project name in SonarQube's makefile rule.

## [0.0.1] - 2019-12-25
### Added
- Initial project structure.
