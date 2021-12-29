<h1 align="center"> Argos.Framework.ServiceInjector</h1>
<h6 align="center">© Visual Studio EX3, José Miguel Sánchez Fernández - 2021</h6>

### A simple and lightweight service injector library for .NET.

[![Build](https://github.com/VisualStudioEX3/Argos.Framework.ServiceInjector/actions/workflows/main.yml/badge.svg)](https://github.com/VisualStudioEX3/Argos.Framework.ServiceInjector/actions/workflows/main.yml)
[![GitHub](https://img.shields.io/github/license/VisualStudioEX3/Argos.Framework.ServiceInjector?color=yellow)](https://opensource.org/licenses/MIT)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/VisualStudioEX3/Argos.Framework.ServiceInjector?color=green)](https://github.com/VisualStudioEX3/Argos.Framework.ServiceInjector/releases/)

A simple to use and setup dependency injection system to use in .NET projects:
- Supports normal and singleton instance services.
- Supports for generic services.
- Service initialization on request.
- Checks and validates service contracts when register them instead when request them. This allow to catch invalid contracts before request the services.
- Supports services with nested services, declaring them in the service constructor as parameters.
- Supports resolve dependencies from other service providers. You can setup the service providers that a service container use to resolve dependencies.
- Ease the implementation and initialization of service containers as service providers using our abstract implementation.
- Supports to implements custom service containers. This allow modifications on the inner behaviour of the service injector (implements your own service container implementation or mapping other service injector systems) and allow to keep your code without change it, using the same functions like the default ones.

The initial goal to develop this project was the challenge to understand the inner complexity of a dependency injection system (simple curiosity), and to improve my knowledge of reflexion and generic types in .NET (and, also, I developed it just for fun).

Was developed in **.NET 5** and, a previous developed version was tested in [Artemis](https://github.com/VisualStudioEX3/Artemis) project, a little **Unity 2021** game for a job interview.

## TODO
- Setup github actions.
- Autopublish on NuGet on sucessfull commits to master branch.
- Write a simple documentation (maybe using [Doc FX](https://github.com/dotnet/docfx) or simply write a simple wiki).
- Improve this README.md content.
