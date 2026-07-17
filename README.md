# OPC UA Robotics Reference Implementation

This repository contains a reference OPC UA Robotics server, the shared `Robotics.Client.Core` client layer, the ASP.NET Core `Robotics.ClientGateway`, and the React/TypeScript `Robotics.Workbench`.

## Current capabilities

- Multi-robot registry with robot-scoped status, discovery, and snapshots.
- Robot-scoped SystemOperation and TaskControlOperation commands.
- Isolated robot-scoped WebSocket live streams and DataChange notifications.
- Multi-robot Workbench fleet dashboard with responsive laptop, tablet, and phone layouts.
- Hierarchical state-machine visualization, component-aware axis and motor widgets where runtime data exists, technical provenance, and exact OPC UA StatusCode visibility.

## Architecture

```text
Vendor OPC UA Robotics Server
        | opc.tcp
Robotics.Client.Core inside Robotics.ClientGateway
        | HTTP/WebSocket
Hosted Robotics.Workbench in the browser
```

Bundling is a deployment simplification. Gateway and Workbench responsibilities remain separate: the Workbench never speaks `opc.tcp`; `Robotics.Client.Core` remains the OPC UA client logic layer.

## Portable vendor package

Download and extract the ZIP, start the vendor robot/simulator and OPC UA Robotics server, edit the root `robots.json`, and double-click `OPC-UA-Robotics-Workbench.exe`. The browser opens automatically. Restart after changing `robots.json`.

The root is intentionally calm: `OPC-UA-Robotics-Workbench.exe` is the launcher, `robots.json` is the only normal configuration file to edit, `app\` contains internal gateway runtime files, `logs\` contains logs, `certificates\` contains the OPC UA application/trusted/rejected/issuer stores, `docs\` contains detailed guidance, and `licenses\` contains notices. Do not move individual files out of the package.

Vendors do not need the reference server, Visual Studio, the .NET SDK, Node.js, npm, or source code. See [README-FIRST.txt](packaging/README-FIRST.txt) and [docs/vendor-portable-package.md](docs/vendor-portable-package.md).

For local self-testing, a vendor runs its robot or simulator, its OPC UA Robotics server, and the portable Workbench bundle. For the central tradeshow setup, each vendor runs only its robot/simulator and OPC UA Robotics server; the demonstration operator runs one central portable Gateway/Workbench bundle, one multi-robot `robots.json`, and one browser or iPad.

The package is not a certification tool. A successful test demonstrates compatibility with this reference-client implementation and its tested use cases; it does not constitute OPC Foundation certification, formal conformance, or proof of complete OPC UA Robotics implementation.

## Configuration

The packaged file is `<distribution root>\robots.json`:

```json
{
  "robots": [
    {
      "id": "my-robot",
      "displayName": "My Robot",
      "endpointUrl": "opc.tcp://192.168.50.21:4840/RoboticsServer",
      "enabled": true
    }
  ]
}
```

IDs are URL-safe and unique. Endpoints must be absolute `opc.tcp` URIs with a valid port. At least one robot must be enabled. Multiple robots use additional entries in the same array. Configuration precedence is: internal appsettings defaults, environment-specific appsettings, external `robots.json`, environment variables, then command-line arguments. `robots.json` is read once at startup; restart after edits. Development remains backward compatible with the `Robots` section and the legacy `OpcUa:EndpointUrl` fallback when no registry is configured.

## Robot-scoped API

```text
GET  /api/robots
GET  /api/robots/{robotId}/status
GET  /api/robots/{robotId}/discovery
GET  /api/robots/{robotId}/snapshot?selection=all
POST /api/robots/{robotId}/system/get-ready
POST /api/robots/{robotId}/system/start
POST /api/robots/{robotId}/system/stop
POST /api/robots/{robotId}/system/stand-down
POST /api/robots/{robotId}/task/load-by-name
POST /api/robots/{robotId}/task/start
POST /api/robots/{robotId}/task/stop
POST /api/robots/{robotId}/task/reset-to-program-start
POST /api/robots/{robotId}/task/unload-program
GET  /ws/robots/{robotId}/live
```

Legacy default endpoints remain available under `/api/robotics/...` and `/ws/robotics/live` for backward compatibility.

## Development

The gateway can run independently and Vite can run independently. Vite development uses `http://localhost:5173` and may override the gateway with `VITE_GATEWAY_URL`; development CORS remains configured in appsettings. A missing production `wwwroot` does not prevent API startup. Developers can continue to run the reference server and `VerifyRobotCommands.ps1`.

The hosted production flow is one origin: `/` serves the compiled Workbench, `/api/...` serves REST, and `/ws/...` serves WebSockets. Production REST uses `window.location.origin`; WebSockets select `ws` or `wss` from the page protocol.

## Certificates and logs

Portable packages use package-relative `logs\` and `certificates\application`, `trusted`, `rejected`, and `issuers` directories. Production rejects untrusted server certificates and writes the public certificate to `certificates\rejected`; review it and copy only that public certificate into `certificates\trusted`, then restart or retry. Never copy private certificate material or re-enable automatic trust. The launcher establishes `OPCUA_ROBOTICS_WORKBENCH_ROOT`; package-root precedence is explicit `PackageRoot` command-line configuration, that environment variable, then the application-base fallback. Development retains the configured Vite CORS policy; hosted production is same-origin and does not apply CORS.

## Verification

Do not change OPC UA information-model semantics, vendor NodeIds, namespace indexes, BrowseNames, method signatures, or exact StatusCode handling. Developers should run:

```powershell
.\VerifyRobotCommands.ps1
```

## Roadmap

Future work includes selected-robot orchestration, guided in-app preflight, certificate trust UI, support-bundle export, reconnect hardening, signed releases, an installer, and vendor test report generation.

Further reading: [architecture](docs/architecture.md), [Workbench data flow](docs/workbench-data-flow.md), [portable vendor package](docs/vendor-portable-package.md), and [visualization](docs/visualization.md).
