# Phema.Routing

[![Build Status](https://cloud.drone.io/api/badges/phema-team/Phema.Routing/status.svg)](https://cloud.drone.io/phema-team/Phema.Routing) [![Nuget](https://img.shields.io/nuget/v/Phema.Routing.svg)](https://www.nuget.org/packages/Phema.Routing)

C# strongly typed routing library for `ASP.NET Core` built on top of `MVC Model conventions` with built-in `From.*`, `ApiExplorer`, `Authorization` and `Caching` support

## Features

- Flexible constraint and filter configuration
- Extension methods for authorization, caching, api documentation
- Selection BindingSources and named parameters (constant names only) e.g. `From.Body`, `From.Form`, `From.Route` etc.
- Parameter name override e.g. From.Query<int>("id_override") `/route?id_override=12345`
- Extensions
  - Http methods - `.HttpPost()`...
  - Caching - `.Cached(...)`
  - Authorization - `.Authorize(...)`, `.AllowAnonymous()`
  - AntiForgeryToken - `.ValidateAntiForgeryToken()`, `.IgnoreAntiforgeryToken()`
  - Https - `.RequireHttps()`
  - RequestSizeLimit - `.RequestSizeLimit(...)`, `.DisableRequestSizeLimit()`
  - Produce, Consume for ApiExplorer - `.Produces<TModel>(...)`, `.Consumes(...)`

## Usage

```csharp
// Startup
services.AddControllers() // `.AddMvc()`, `.AddMvcCore()`
  .AddRouting(routing =>
  {
    // Simple route
    routing.AddController<OrdersController>("orders", controller =>
      controller.AddRoute("dashboard", c => c.Dashboard())
        .HttpGet());

    // Authorize controller with "id" parameter from query
    routing.AddController<TaskController>("tasks", controller =>
      controller.AddRoute("edit/{id}", c => c.Edit(From.Route<int>("id"))) // `From.*` is matches `[From*]` attributes
        .HttpGet()
        .Authorize());

    // Multiple routes in one controller
    routing.AddController<WashingController>("washing", controller =>
    {
      // Override controller `.Authorize()`
      controller.AddRoute("wash", c => c.Wash(From.Query<int>())) // If name not specified - used method parameter name
        .HttpGet()
        .AllowAnonymous()

      // Inherit authorization requirement from controller
      controller.AddRoute("dry", c => c.Dry(From.Body<DryModel>()))
        .HttpPost()
        .ValidateAntiForgeryToken();
    }).Authorize();

    // Multiple binding sources
    routing.AddController<MoviesController>("movies", controller =>
      controller.AddRoute("{category}/upload", c =>
        c.Upload(From.Route<string>(), From.Body<UploadMovieModel>(), From.Query<bool>("compress")))
        .HttpPost());
  });
```

## Action url generation (IUrlHelper used)

```csharp
// From.Query with parameter override
services.AddRouting(routing =>
  routing.AddController<Controller>("controller", controller =>
    controller.AddRoute("action", c => c.Action(From.Query<int>("parameter")))));

// "controller/action?parameter=10"
var action = Url.Action<Controller>(c => c.Action(10));

// From.Route with route part override
services.AddRouting(routing =>
  routing.AddController<Controller>("controller", controller =>
    controller.AddRoute("action/{mode}", c => c.Route(From.Route<Mode>("mode")))));

// "controller/action/access"
var action = Url.Action<Controller>(c => c.Action("access"));

// Combine query and route parts
services.AddRouting(routing =>
  routing.AddController<Controller>("controller", controller =>
    controller.AddRoute("action/{mode}", c => c.Route(From.Query<int>(), From.Route<Mode>()))));

// "controller/action/access?parameter=10"
var action = Url.Action<Controller>(c => c.Action(10, "access"));

// Get values from current request using From.Query in IUrlHelper generation
services.AddRouting(routing =>
  routing.AddController<Controller>("controller", controller =>
    controller.AddRoute("action", c => c.Action(From.Query<int>()))));

// P.S. BindingSources not used in url will ignored (e.g. `From.Body<TModel>()` used only for compile reasons)
// "controller/action?parameter=VALUE_FROM_CURRENT_REQUEST"
var action = Url.Action<Controller>(c => c.Action(From.Query<int>()));
```
