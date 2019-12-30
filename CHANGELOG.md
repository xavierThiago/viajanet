# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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
- Some enhancements on analytics script.

## [0.0.6] - 2019-12-28
### Added
- JSHint directives on vn.analytics.js.

### Changed
- Analytics script now has Apache (version 2.0) licensing.

### Fixed
- Unused wrapper variable on vn-analytics.js.

## [0.0.5] - 2019-12-28
### Added
- Remaning HTML structure [(backbone), but still have work to do].

### Changed
- Analytics script now hits on page load.

## [0.0.4] - 2019-12-28
### Added
- Remaining HTML pages (backbone);
- Unit tests for AnalyticsPayload class;
- CORS configuration on Startup;
- RESTful documentation scratches;
- API call on main front-end script.

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
- Project name on SonarQube.

## [0.0.1] - 2019-12-25
### Added
- Initial project structure.
