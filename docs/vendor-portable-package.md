# Portable vendor package

Extract the complete Windows x64 package. Edit only the root `robots.json`, then double-click
`OPC-UA-Robotics-Workbench.exe`. Do not move individual files out of the package.

```text
OPC-UA-Robotics-Workbench.exe  launcher to double-click
robots.json                    only normal configuration file to edit
VERSION.txt                    product version and source revision
app\                           internal gateway runtime; do not edit
logs\                          runtime logs
certificates\                  application, trusted, rejected, issuers
docs\                          detailed guidance
licenses\                      license notices
```

The launcher locates its own directory, starts `app\Robotics.ClientGateway.exe`, forwards
arguments, sets `OPCUA_ROBOTICS_WORKBENCH_ROOT=<package root>`, waits for the gateway, and
returns its exit code. The gateway resolves its package root in this order:

1. explicit `PackageRoot` command-line configuration;
2. `OPCUA_ROBOTICS_WORKBENCH_ROOT` from the launcher;
3. `AppContext.BaseDirectory` for development or direct execution.

Consequently, production paths are `<package root>\robots.json`, `<package root>\logs`, and
the four OPC UA SDK stores under `<package root>\certificates`. The application certificate
is under `certificates\application`; peer certificates go under `trusted`, rejected peers
under `rejected`, and issuers under `issuers`. These names are the actual directories passed
to the OPC UA SDK by this application.

## Certificate trust

Production keeps `AutoAcceptUntrustedCertificates=false`. On `BadCertificateUntrusted`,
the SDK writes the server's public certificate to `certificates\rejected`. The Workbench
diagnostic names the robot, display name, endpoint, rejected path, and trusted path. Review
the rejected certificate out of band, copy only that public certificate file into
`certificates\trusted`, and restart or retry. Do not copy `.pfx` private material and do not
enable automatic trust. C18B graphical certificate trust is not part of this package.

## HTTP and development

Packaged Workbench traffic is same-origin: `/`, `/api/...`, and `/ws/...` use the gateway
origin and do not apply CORS. In Development only, `UseCors` applies the configured
`Gateway:CorsAllowedOrigins` policy for Vite origins. The separate Vite workflow remains
available with `VITE_GATEWAY_URL`.

## Release identity and recovery

`/api/metadata` exposes separate `version`, `informationalVersion`, `sourceRevisionId`, and
`runtimeIdentifier` fields. Startup and `VERSION.txt` show `Version: 1.0.0-rc1` and
`Revision: c18a-uncommitted` independently. Robot WebSockets retry independently at roughly
1, 2, 4, 8 seconds, capped at 30 seconds; a successful connection resets the delay. Closing,
disabling, or unmounting cancels its pending retry, and technical details show the latest
error and next retry.

The package is created by `scripts\Publish-PortableWorkbench.ps1`, which builds the SPA,
self-contained-publishes the gateway into `app\`, publishes the single-file launcher at the
root, copies docs and licenses, validates allowed root entries, and creates the ZIP and SHA-256
checksum. GitHub Actions invokes the same script.
