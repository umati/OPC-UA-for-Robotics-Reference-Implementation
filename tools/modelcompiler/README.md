# OPC UA Robotics Model Compiler

This folder contains tooling for generating OPC UA Robotics model classes with the OPC Foundation UA-ModelCompiler.

UA-ModelCompiler is maintained separately from the OPC UA .NET Standard runtime SDK. The runtime SDK hosts and serves OPC UA applications; UA-ModelCompiler consumes OPC UA model definitions and can generate .NET classes and constants from NodeSet files.

We will use UA-ModelCompiler to generate .NET model code for OPC UA DI and OPC UA Robotics v1.02 from the official NodeSet XML files.

Generated files should not be manually edited. Any required changes should be made by updating the source model inputs or the generation process, then regenerating the output.

Generated files should live in a separate generated project so hand-written reference server code stays isolated from generated model code.

## Install or Verify UA-ModelCompiler

Install or build the OPC Foundation UA-ModelCompiler separately from the OPC UA .NET Standard runtime SDK.

Verify the installed command is available:

```powershell
Opc.Ua.ModelCompiler --help
```

The generation script uses this command shape:

```powershell
Opc.Ua.ModelCompiler compile -d2 <DiNodeSet>,Opc.Ua.DI,DI -o2 <DiGeneratedOutputFolder>
Opc.Ua.ModelCompiler compile -d2 <RoboticsNodeSet>,Opc.Ua.Robotics,Robotics -d2 <DiNodeSet>,Opc.Ua.DI,DI -o2 <RoboticsGeneratedOutputFolder>
```

## Inputs

Before generation, the official NodeSet XML files must be present in:

```text
src/Robotics.ReferenceServer/NodeSets/
```

Expected input files:

- `Opc.Ua.Di.NodeSet2.xml`
- `Opc.Ua.Robotics.NodeSet2.xml`

These files should come from official OPC Foundation sources or from the official `UA-Nodeset` repository.

The main NodeSets folder may also contain optional or future integration models, including:

- `Opc.Ua.IA.NodeSet2.xml`
- `Opc.Ua.Machinery.NodeSet2.xml`

The current milestone generates OPC UA DI first, then OPC UA Robotics with OPC UA DI as its dependency. Robotics generated code references DI generated classes, including types such as `ComponentTypeState`, so the DI generated output must be available to the generated model project.

The script creates clean temporary compiler input folders at:

```text
generated/modelcompiler-input/di-core/
generated/modelcompiler-input/robotics-core/
```

Before each run, those folders are deleted and recreated. The DI input folder receives only `Opc.Ua.Di.NodeSet2.xml`. The Robotics input folder receives only `Opc.Ua.Di.NodeSet2.xml` and `Opc.Ua.Robotics.NodeSet2.xml`. This prevents UA-ModelCompiler from scanning optional IA, Machinery, combined, or other NodeSet XML files that may be present in the source NodeSets folder.

IA and Machinery are intentionally not included yet because they are future optional integration layers for this reference implementation, not part of the current Robotics-only generated model target.

## Output

The script writes generated files to:

```text
generated/opcua-di/
generated/opcua-robotics/
```

The exact generated file names may depend on the installed UA-ModelCompiler version.

Generated files should not be manually edited. Treat `generated/opcua-di/` and `generated/opcua-robotics/` as disposable output and make changes through the source NodeSets or generation script instead.

The generated files are linked into a separate class library project:

```text
src/Robotics.OpcUa.RoboticsModel.Generated/
```

The generated project intentionally includes both DI and Robotics generated code for now because Robotics generated classes depend on DI generated classes.

Do not copy generated files into hand-written server projects. Keep generated model code isolated so it can be regenerated cleanly.

## Run

From the repository root:

```powershell
.\tools\modelcompiler\generate-robotics-model.ps1
```

The script validates the required NodeSet XML files, stages isolated DI and Robotics compiler input folders, creates both output folders, and runs UA-ModelCompiler. It only generates model code; it does not load NodeSets into the running server.
