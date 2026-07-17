OPC UA ROBOTICS WORKBENCH - FIRST STEPS

1. Start the robot or simulator and its OPC UA Robotics server.
2. Edit the root robots.json: id, displayName, endpointUrl, and enabled.
3. Save robots.json.
4. Double-click OPC-UA-Robotics-Workbench.exe.
5. The browser opens at http://localhost:5080 (or the configured Workbench URL).

OPC-UA-Robotics-Workbench.exe is the launcher. It starts app\Robotics.ClientGateway.exe
and sets OPCUA_ROBOTICS_WORKBENCH_ROOT to the package root. The gateway therefore reads
robots.json and writes logs and certificates beside the launcher, not inside app\.
Precedence is PackageRoot command-line configuration, then OPCUA_ROBOTICS_WORKBENCH_ROOT,
then the gateway application directory (development fallback). Do not move individual files.

app\ contains internal gateway runtime files and wwwroot; do not edit it.
logs\ contains runtime logs.
certificates\application, trusted, rejected, and issuers are the OPC UA SDK directory stores.
docs\ contains detailed vendor guidance. licenses\ contains license notices.

Production rejects untrusted server certificates but writes the public certificate to
certificates\rejected. Review that certificate and copy only the public certificate file
to certificates\trusted, then restart the launcher or retry the connection. Never copy a
private key and never enable automatic trust in the package.

This is a reference-client test tool, not an OPC Foundation certification tool. Include
VERSION.txt, the endpoint, and relevant logs when reporting an issue. Do not send private keys.
