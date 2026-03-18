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
// default enableReflectionCache as true
builder.Services.UseDapper();
```

#### Release Notes
 - fix: Removed ExecuteAllAsync that returns response because it is unnecessary `18/03/2026`
 - feat: New method overrides to returns only Response in INormalProcedure `18/03/2026`
 - feat: New method overrides to returns only Response in IComplexProcedure `18/03/2026`
 - feat: Added cache as configurable property `18/03/2026`
 - fix: IsValueType was not considered string as true, so it returns 'Parameter count mismatch' `10/03/2026`
 - feat: Documentation for all public methods `10/03/2026`
 - fix: IEnumerable<T> and T are considered as a datatable parameter and static cache has been changed by extension method `05/03/2026`
 - refactor: apply SRP by splitting DapperExtension and ConnectionHelper into focused classes `05/03/2026`
 - refactor: eliminate duplicated connection pattern via DbExecutor and BaseProcedureHelper (DRY) `05/03/2026`
 - perf: add reflection cache, fix connection pooling, pure async reader, and ArrayPool for SqlParameter `05/03/2026`
 - refactor: introduce IDbConnectionFactory to invert connection dependency (DIP) `05/03/2026`
 - fix: Now are included primitives, objects and enumarables as a UDT parameters in Complex Procedures (tested) `04/03/2026`
 - refactor: Fixing method naming `04/03/2026`
 - fix: Now are included primitives, objects and enumarables as a UDT parameters in Complex Procedures `04/03/2026`
 - refactor: Implementation of own helper to decoupling the procedure helper `04/03/2026`
 - fix: Type specification on add column in the Datatable when use ComplexProcedure interface (Reported by user implementation on try to add Datetime field) `04/03/2026`
 - refactor: Implementation of own service to decoupling the procedure service `04/03/2026`
 - The IConnectionString service was removed to allowing the developer configure options, before to call UseDapper function `28/01/2026`
 - Segregation of dependecy IProcedureService in IComplexProcedure, INormalProcedure and IMultipleProcedure `28/01/2026`
 - Refactor Dapper helpers for DI and connection validation `19/01/2026`
 - Health check implementation and updating of dependencies to the last version `21/10/2025`
 - Added SetConnection to set the runtime value `24/01/2025`

Created by [Mario Soto Moreno](https://github.com/MatProgrammerSM)