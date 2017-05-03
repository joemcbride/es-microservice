# Microservice

This is a basic setup for a microservice secured with JSON Web Tokens (JWT) RS256.

## Building

This project is setup with a Cake build system.  It uses `yarn` + `switch.js` to determine which cake build script to use (`build.ps1` or `build.sh`) depending on your OS.

* `yarn compile` install node modules + compile project
* `yarn start` runs the `ES.Api` project using `dotnet watch`
* `yarn test` runs the tests for the project
* `yarn build` development publish to the `.build` folder
* `yarn release` production publish to the `.build` folder

## Certificate

The certificate used to verify the JWT is loaded via `ICertificateLoader` interface.  The `LocalCertificateLoader` will load a certificate via the file system and the `StoreCertificateLoader` will load a certificate from the local machine X509 certificate store.  macOS will need to use the `LocalCertificateLoader` as [you cannot yet load certificates from the local Keychain](https://github.com/dotnet/corefx/issues/11182#issuecomment-242763503).  You can use the `TokenSettings` to configure information related to the certificate.
