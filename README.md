# Phema.Routing

C# typed routing library for asp.net core

- [x] Core library
- [x] Extensions
- [x] Tests

# Simple usage
```csharp
// Startup
services.AddMvcCore()
  .AddRouting(routing => {});
```
- You have two ways to add routing: using `IMvcBuilder` or `IMvcCoreBuilder` extensions - `AddRouting`
- In `AddRouting` pass callback with `IRoutingConfiguration` to register route parts

# Adding controller
```csharp
// Startup
services.AddMvcCore()
  .AddRouting(routing =>
    // Add controller
    routing.AddController<TestController>("controller-route-part", controller => {});
```
- To add controller you need to specify `TController` and route part
- You can remove route part: `AddController<TestController>(controller => {})`. 
- It will be an empty string (so just `/` route)

# Adding routes
```csharp
// Startup
services.AddMvcCore()
  .AddRouting(routing => 
    routing.AddController<TestController>("controller-route-part", 
      // Add route
      controller => controller.AddRoute("action-route-part", c => c.TestMethod()));
```
- To add action route you need to specify an action route (goes after controller route `controller/action`) and expression for method
- By default route adds without any http method constraint (so you can call it with get, post, etc.)

# Adding constraints
```csharp
// Startup
services.AddMvcCore()
  .AddRouting(routing => 
    routing.AddController<TestController>("controller-route-part", 
      controller => 
        controller.AddRoute("action-route-part", c => c.TestMethod())
          // Add constraint
          .HttpGet());
```
- You can add constraints to both: `controller` and `route` using `IRouteBuilder` returned by `AddController` or `AddRoute`
- You can add them by using `AddFilter`/`AddConstraint` method with `IServiceProvider` parameter or extension without it
- You can add named routes by using `WithNamed` method
- To use predefined constraints and filters you need a `Phema.Routing.Extensions` package
- For caching you can use `Phema.Routing.Extensions.Caching` package
- To create custom filters and constraints you need `IFilterMetadata` or `IActionConstraintMetadata` in `Microsoft.AspNetCore.Mvc.Abstractions` package or inherit from derivatives
- You can add `HttpGet`, `Cached`, etc. to controller's `IRouteBuilder` it means that any action inside will be `HttpGet`, `Cached`, etc.

# Adding parameters
```csharp
services.AddMvcCore()
  .AddRouting(routing => 
    routing.AddController<TestController>("controller-route-part", 
      controller => 
        controller.AddRoute("action-route-part", 
            // Add parameter source
            c => c.TestMethodWithStringParameter(From.Query<string>()))
          .HttpGet());
```
- You can specify from wich source mvc will bind your model
- To specify parameters you have to use `From` static class
- To inject arguments from DI use `From.Services` method
- `From.*` is matches `[From*]` attributes, so no more attributed parameters polluting your controller actions
- To use `From.Body` don't forget to add mvc formatters using `AddMvcCore`



