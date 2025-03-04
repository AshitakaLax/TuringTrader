I have made many updates and improvements to this. I am still in the works of dev and it is very unstable.
It is very very loosely built off the TuringTrader, Basically none of the algorithms written for TuringTrader would work for this updated version.


# TuringTrader

![GNU Affero General Public License](https://www.gnu.org/graphics/agplv3-155x51.png)

An open-source backtesting engine/ market simulator, licensed under [AGPL 3.0](https://www.gnu.org/licenses/agpl-3.0).

For news, updates, and  more information about the project, please visit our website at https://www.turingtrader.org/.

## Installing

To install TuringTrader, download the setup file from https://www.turingtrader.org/download/.

## Documentation

Find the TuringTrader documentation at https://www.turingtrader.org/documentation/.

## Building from Source

### Prerequisites

see https://dotnet.microsoft.com/download/dotnet-core/3.1

- Microsoft Visual Studio Community 2019
    - due to using .NET Core 3, we need at least version 16.2.0 Preview 3
    - installation must include Workloads for .NET desktop development and Universal Windows Platform development
- .NET Core 3.1 SDK
- WiX Toolset version 3.11
- WiX Toolset Visual Studio 2019 Extension
- DocFX toolset version 2.43.2.0

### Build Steps

Here are the individual steps to build TuringTrader from source:

- Install TuringTrader from binary distribution
- Open TuringTrader solution in Visual Studio
- Build release version of TuringTrader project
- Publish TuringTrader application
- Build BooksAndPubs project (will link against the binary version installed)
- Build setup project
- Build documentation

The project also contains a script for a single-click build. See SINGLE_CLICK_BUILD.bat at the repository root!





Happy coding!

Felix Bertram
info@TuringTrader.org
