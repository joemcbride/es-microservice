# Microservice

This is a basic setup for a microservice secured with JWT.

## Building

This project is setup with a Cake build system.  It uses `yarn` + `switch.js` to determine which cake build script to use (`build.ps1` or `build.sh`).

* `yarn compile` install node modules + compile project
* `yarn start` runs the `ES.Api` project using `dotnet watch`
* `yarn test` runs the tests for the project
* `yarn build` development publish to the `.build` folder
* `yarn release` production publish to the `.build` folder
