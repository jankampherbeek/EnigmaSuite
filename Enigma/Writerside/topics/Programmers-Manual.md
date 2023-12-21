# Developers Documentation Enigma Research

## Version 0.2


## Enigma Research - introduction

Enigma Suite is a software suite, written in C#. The program is free and open source. 
This document provides some information for interested programmers. 
In future releases, I hope to augment this document.

Please read the User Manual for information about the functionality of Enigma Research.

I want to thank Gökhan Yu for convincing me to use C#. 
It was the right choice for building a Windows based astrology application. 
Gökhan also provided valuable insights into the technicalities of C# and .Net.


### License / open source

Enigma is Open Source. You can use it following the terms of the GNU General Public License (GPL). 
The GPL allows you to use, change and redistribute this software only if your own software is open source. 
It does not have to be free, but the full source code should be publicly available. 
For more information, see the file *gpl-3.0.txt* in the source's root.

Enigma uses libraries from the Swiss Ephemeris (SE). For the SE, additional conditions are in place. 
These conditions prohibit the use of the software unless it is open source and also free. 
If you want to charge money for a program using software from the SE, you need to buy a professional license from the SE. 
For more information, see the file *se_license.htm* in the source's root.

To use software from Enigma in your program, that program has to be open source. 
If you include the libraries from the SE, it also has to be free. 
Buying a license from the SE does not change the condition from the GPL that the software should remain open source. 
If you want to create software that is not open source you can use the libraries from the SE but you will need to buy 
a professional license, and you cannot use any code from Enigma.


## Technical basics

### Development environment

#### IDE
I develop Enigma with _JetBrains Rider_. This IDE is not free but you can try to apply for a free open source license. 
I am happy that Enigma is accepted by JetBrains for such a free license :-) 
The code also works on MicroSoft Visual Studio (Community Edition).

#### Dependency injection
For dependency injection, Enigma uses the NuGet package Micosoft.Extensions.DependencyInjection.

#### MVVM
The UI is based on the MVVM pattern. This is implemented with the NuGet package Community.Toolkit.Mvvm from Microsoft.

#### Material Design
Enigma uses some aspects from Material Design, using the NuGet package MaterialDesignThemes

#### Persistency
The database is created with LiteDB, also accessible via Nuget.

#### Logging
Serilog takes care of the logging, you can find it in NuGet Serilog.Sinks.File.

#### Testing
Unit testing is done with NUnit (NuGet NUnit). For mocking I use Moq (NuGet Mock).


### Coding conventions

I will try to abide to the standards. 
For a definition check: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions .


### Architectural decisions

#### Windows based
Enigma can be used on Windows platforms. I will not attempt to support other platforms, at least not for the foreseeable future. 
The main reason is that being multi-platform requires much effort, even with the availability of solutions like Avalonia.
These efforts would be in time and financial: supporting Apple hardware is not easy without buying it.

#### Using separated projects
The code of Enigma consists of 6 seperate projects:
* **Frontend.Ui** : everything for the user interface.
* **Api** : API's that are used to access the backend.
* **Core** : code for the backend.
* **Facades** : facades that provide access to external systems (for now only the Swiss Ephemeris).
* **Domain**: domain definitions, accessible by both the frontend and backend.
* **Test**: All unit tests.

#### WPF for the frontend
The frontend uses WPF and XAML. I also considered Avalonia, which supports multiple environments and improves the XAML syntax.
But Avalonia does not support as many NuGet packages as plain WPF does. 
The material design package is not supported which was a no-go for me.

#### Frontend specifics
The frontend uses the MVVM pattern. Navigation between views is realised by messaging.
The look-and-feel is loosely based on Material Design.

#### Separation of frontend and backend
The frontend and backend are clearly separated. All functionality from the backend is accessible via a set of API's. 

#### Based on dependency injection
The backend is fully based on dependency injection. The frontend uses some DI, but not consistently. 
The creation and termination of views is handled by separate classes that react on messages received.

#### Unit testing
I use NUnit for unit testing. I believe testing is very important though I am not religious about Test Driven Development.
Enigma does not yet support integration testing but I want to add that in a future release.



### Using the Swiss Ephemeris

For astronomical calculations, I use the Swiss Ephemeris (SE). 
The SE comprises a set of data and a 64-bits dll: *swedll64.dll*.
To access the dll, the attribute [DllImport] is used. 
All imports from the dll are defined in facades. 
As an example for the definitions I used the file swissdelphi.pas that Pierre Fontaine and others created to access 
the same dll from Delphi.

### Icons

All icons in Enigma, except the main icon that appears on the screen, are from the icon set by Google, used for 
Material Design.
You can download the originals at https://fonts.google.com/icons .

## Installing the code

Clone the repository from GitHub: https://github.com/jankampherbeek/EnigmaSuite .

Copy swedll64.dll from Enigmasuite/Enigma/Enigma.Frontend.res to
Enigmasuite/Enigma/Enigma.Frontend/bin/Debug/net6.0-windows and to
Enigmasuite/Enigma/Enigma.Frontend/bin/Release/net6.0-windows

## Projects

Enigma is build as a .NET solution that contains 6 projects. 
There is a separate project for unit testing, the other 5 projects contain the application. 
There is only limited communication between these 6 projects.

#### Project Frontend.Ui

*Frontend.Ui* is activated as the application starts. It takes care of some initializing. 
Its main task is showing information to the user and receiving input from the user.

#### Project API

The classes in this project receive requests from the Frontend, perform some basic validation and pass the request to 
a Handler in the *Core.Handlers* project. In most cases, the API returns a response to the Frontend. 
An API never contains any business logic.

#### Project Core.Handlers

A Handler orchestrates the fulfillment of a request. Possibly it uses some basic business logic but in many cases 
it will rely on helper classes. A handler may call other handlers. 
Sometimes it will simply pass through a request but it can also combine the results of several helper classes from 
the project *Work*.

#### Project Facades

The project *Facades* contains classes that can access the outside world. 
A range of classes is used to access the dll from the Swiss Ephemeris. Other classes take care of persistency.

#### Project Domain

*Domain* contains all domain objects, including enums, DTO's and records. 
*Domain* cannot access other projects and is itself accessible by all projects.

## Astronomical aspects

I follow the usual approach using the Swiss Ephemeris but I need to mention some specifics.

### School of Ram: hypothetical planets

Enigma supports the three hypothetical planets as proposed by the School of Ram: Persephone, Hermes and Demeter. 
The calculations are based on the orbital elements and calculated separately, without accessing the SE.

### School of Ram: oblique longitude

The School of Ram supports a solution for the projection of the solar system bodies to the ecliptic. 
This solution ensures a proper placing of bodies in a house. However, the projection to the ecliptic is skewed. 
The solution is called 'true place' and also 'astrological place'. I prefer the more correct term 'oblique longitude'.

Enigma implements a dedicated calculation of this oblique longitude. 

## Research

### Control groups and random numbers

To create a control group, you need to calculate random values. 
The standard *PRNG* (Pseudo Random Number Generator) does not supply true random numbers.

Enigma uses *System.Security.Cryptography* from Microsoft, a *CSPRNG* (Cryptographic Secure Pseudo Random Number 
Generator). 
More information: 
[https://download.microsoft.com/download/1/c/9/1c9813b8-089c-4fef-b2ad-ad80e79403ba/Whitepaper%20-%20The%20Windows%2010%20random%20number%20generation%20infrastructure.pdf](https://download.microsoft.com/download/1/c/9/1c9813b8-089c-4fef-b2ad-ad80e79403ba/Whitepaper - The Windows 10 random number generation infrastructure.pdf)

This solution is sufficiently random to support the creation of control groups.

I want to thank Cees Jansen for explaining the peculiarities of randomness to me. 



## Configuration
Enigma uses two configurations: a configuration for general use and an additional configuration for progressions.
A standard configuration is defined and the user can change the configurations by defining delta's.
To define the actual configuration, the standard configuration is corrected with these delta's.

### Peristency of the general configuration
The configuration is saved as a dictionary that is converted to Json. 
The key-value pairs in the dictionary use a predefined key and a value that can consist of multiple values, 
separated by two pipes (standing lines). It is not possible to use a single char as separator as all character are being used by the 
Enigma font, using one character would make interfere with the glyph for that character.

There are three groups of keys:
* _keys for chartpoints_, these are prefixed with <strong>cp_</strong>, followed by an index thar refers to the enum for chartpoints.
* _keys for aspecttypes_, the prefix is <strong>at_</strong>, followed by the index for the enum aspecttypes.
* _all other keys_, no specific prefix. The key cannot start with one of the prefixes mentioned above.


The values for both chartpoints and aspecttypes have the following structure:

u||g||o||s
* **u** means 'use', enter 'y' if the chartpoint or aspecttype should be used, otherwise 'n'.
* **g** means 'glyph', enter the character or unicode for the glyph.
* **o** means 'orb percentage', enter a value of 100 or lower.
* **s** means 'show', enter 'y' if the chartpoint or aspecttype should be shown in the graphic chart, otherwise 'n'. This will be used in future versions.

An example: **y||a||100||y**  means: use this point or aspect, the glyph is 'a', the orb percentage is 100% and the 
point/aspect should be shown in the chart drawing.
