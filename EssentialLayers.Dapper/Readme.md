# Essential Layers

### EssentialLayers.Dapper

Is a complement to the package `EssentialLayers` to provide an extra layer with the ORM dapper, where the main purpose will be, write the business logic in the "stored procedures" using templates that receiving input parameters and return a result set, Currently is just compatible with SQL Server.

### Configure

Add the dependencies in your **Program.cs** file

```
builder.Services.Configure<ConnectionOption>(
	options =>
	{
		options.ConnectionString = builder.Configuration.GetConnectionString("Local")!;
	}
);
builder.Services.UseDapper();
```

#### Release Notes
 - The IConnectionString service was removed to allowing the developer configure options, before to call UseDapper function `28/01/2026`
 - Segregation of dependecy IProcedureService in IComplexProcedure, INormalProcedure and IMultipleProcedure `28/01/2026`
 - Refactor Dapper helpers for DI and connection validation `19/01/2026`
 - Health check implementation and updating of dependencies to the last version `21/10/2025`
 - Added SetConnection to set the runtime value `24/01/2025`

Created by [Mario Soto Moreno](https://github.com/MatProgrammerSM)