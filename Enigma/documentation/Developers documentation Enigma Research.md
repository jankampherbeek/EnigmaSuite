# Developers documentation Enigma Research 0.1

*Jan Kampherbeek, September 14, 2022

[TOC]



## Enigma Research - introduction

Enigma Suite is a software suite,written in C#. The program is free and open source. This document provides information about the technical aspects for interested programmers.

Please rad the User Manual for information about the functionality of Enigma Research. 



## Choices made

### Language: C# 

The conditions that I deem most important for a programming language are:

- Object orientation.
- Good support for the UI.
- Easy implementable Unit Testing.
- Sufficient libraries.
- Support for dependency injection.

I experimented with Java, Kotlin, Free Pascal, Delphi and C# and I believe that C# is the best approach.

I will use C# in combination with .NET Core and use WPF for the UI.

For Dependency Injection I use the standard solution as offered by Microsoft.

The current versions are C# 10 and .NET Core 6. I will update to more recent versions shortly after they come available. 



### Testing

#### Unit tests: NUnit

I will use nUnit for unit tests.
In Visual Studio the following solutions for unit testing are available.

- xUnit

- nUnit

- MSTest

MSTest is the standard solution from Microsoft but nUnit has more functionality and is a long
time de facto standard. I did not investigate this thoroughly but after a short search on the
Internet it appears that nUnit and MSTest are comparable. xUnit has some specifics, like a less
understandable syntax. I will use nUnit, mainly because it is a long time proven solution.

#### Mocking: MOQ

For mocking MOQ is clearly the most used solution, so I will use this framework.

### Database: SQLite

The data to save is mostly about data for charts and in a much smaller amount for configurations. A RDBMS is well suited to handle this type of data. As Enigma is a single-user application, concurrency is not a requirement but an embedded, zero-configuration, database is. SQLite is a perfect match. A simple but proven database engine that can easily be matched with C#.



## Coding conventions

I will try to abide to the standards. For a definition check: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions



## Technical aspects

### Using the Swiss Ephemeris

For astronomical calculations i will use the Swiss Ephemeris (SE). The SE consists of a set of data and a 64-bits dll: *swedll64.dll*.

To access the dll, the attribute [DllImport] is used. All imports from the dll are defined in facades. As an example for the definitions I used the file swissdelphi.pas that Pierre Fontaine and others created to access the same dll from Delphi.



## Architecture

The GUI is based on the MVC model. The view consists of the XAML and accompanying C# part. An additional ViewModel is added to the view. 

### Core

The core of the application handles all calculations and analyses. All requests should be done via an API. An API always uses a Handler to take care of the request. The Handler will use one or more other classes to perform the required actions and uses the results of these actions to return a response.

If the Swiss Ephemeris needs to be accessed, a separate Facade is called that is aware of the specifics of the SE.
All classes that are used by an API, except the Facades, are part of a functional whole, typically within one folder and one namespace. I use the term *swimming lane* for this functional whole.

The outside world of the Core will be the UI and external resources like the SE, databases etc.

Domain information is available in a part called *Shared* which is accessible for the UI and part of
the core.

In the following diagram, all green boxes are part of the Core.

![](D:\dev\proj\EnigmaSuite\MyDocu\diagrams\Architecture core - general approach.drawio.png)



### API

The API is divided in several groups: *AstronApi* for astronomical calculations, *DateTimeApi* for calculations related to clock and calendar etc. This will result in a relatively small set of classes, each with several public methods. These public methods provide the real API.

Each API method accepts an incoming request and, if the request is valid, will ask a *Handle*r to take care of this request and return a response, which is returned to the caller of the API. To check the validity of a request, a set of guards is used in each API call.

### Handlers

A handler is the starting point of a specific functionality. In the following diagram, each handler is shown in a kind of swimming lane. The swimming lane is typically implemented as a folder and a namespace for the same folder. Sub-folders and sub-namespaces are allowed.

A handler or other objects can access either objects in the same swimming lane, or API's in other
swimming lanes. By accessing the API f another swimming lane, the validity of the request is ensured because of the guard statements in the API.

### Facades

To access external functionality, a facade or comparable object is used. For the Swiss Ephemeris, this will be a facade (as shown in the following diagram). For a database, this will typically be a DAO.

The facades for the SE are outside of the swimming lanes as some facades (e.g. CalcUtFacade) will be accessed from several swimming lanes.

![](D:\dev\proj\EnigmaSuite\MyDocu\diagrams\Architecture core - details.drawio.png)

In the diagram classes are indicated by a green box. The green regions *AstronApi* and *DateTimeApi*
also indicate classes. The blue boxes indicate a method.
The overview is schematic, eventually several classes will be added to some of the swimming
lanes



## Astronomical aspects

The usual approach using the Swiss Ephemeris is followed but some specifics need to be mentioned.

### School of Ram: hypothetical planets

The three hypothetical planets as proposed by the School of Ram, Persephone, Hermes and Demeter, are supported.

The calculations are based on the orbitual elements and calculated separately, without accessing
the SE.

### School of Ram: oblique longitude

The School of Ram supports a solution for the projection of the solar system bodies to the ecliptic. This solution ensures a proper placing of bodies in a house. However, the projection to the ecliptic is skewed. The solution is called 'true place' and also 'astrological place'. I prefer the more correct term 'oblique longitude'.

A dedicated calculation of this oblique longitude is implemented in Enigma. Some background information will become available. [TODO].

