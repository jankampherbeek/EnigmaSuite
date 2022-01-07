# Developers documentation Enigma Suite 2022.0.1

*Jan Kampherbeek, January 2, 2022*

[TOC]



## Enigma Suite - introduction

Enigma Suite is a software suite,written in C#.

The suite has 4 components:

- **Charts**: calculation and analysis of horoscopes.
- **Cycles**: calculation and presentation of astronomical cycles.
- **Counting**: support for astrological statistics.
- **Calculations**: astronomical calculations.

The program is free and open source. 

This document provides information about the technical aspects for interested programmers.



## Choices made

### Language

I will use C# 10 and .NET Core 6.0 from Microsoft. 

After experimenting with Java, Kotlin, Free Pascal and Delphi I found that C# has the following advantages:

- Excellent support for the GUI, even better than Delphi and Free Pascal. And much better than JavaFX. I did not compare with Jetpack Compose for Desktop (as used by Kotlin) as this is still in its infancy.
- A very good language, in my opinion on the same level as Kotlin and better than Java. Even if I prefer the Pascal syntax of FP and Delphi, I believe that C# does a better job.
- Available functionality like Dependency Injection and Mocking. This is an advantage compared to FP (no DI and mocking)  and Delphi (one good library Spring4D, but only one maintainer).
- Documentation. Extensive and very good. 
- Integration: a lot of functionality is directly available in the Community Edition of Visual Studio. For Java and Kotlin your need to select many external libraries (sometimes frameworks). You can do the same in Visual Studio but it will often not be necessary.
- Long term availability. VS has been available in a free edition for a very long time, the community edition for Delphi at this moment for about three years. Free versions of Delphi have been discontinued in the past. This is only an advantage compared to Delphi as FP, Java and Kotlin are freely available.

Of course, there are also some drawbacks:

- The use of .NET is required. This is not a big problem anymore for installations, as .NET Core can be included in the installer.  But it seriously enlarges the footprint. This disadvantage is only compared to FP and Delphi, Java and Kotlin have the same problem.
- Performance could be less than the performance of FP or Delphi because of the use of the .NET environment. I intend to do some benchmarks.

The selection for C# is mainly based on the support for the GUI, the quality of the language and the documentation, and the expected long-term availability.

### Dependency Injection

All volatile objects will use DI. The approach is mostly based on *Dependency Injection: Principles, Practices, and Patterns* by Steven van Deursen and Mark Seeman. I will use a DI Container: Simple Injector, developed by Steven van Deursen. This DI Container performs well and has all required functionality. For a comparison with other frameworks, see: https://www.palmmedia.de/blog/2011/8/30/ioc-container-benchmark-performance-comparison



### Testing

#### MS Test for unit tests

I will use MS Test for unit tests.

In Visual Studio the following solutions for unit testing are available.

- xUnit
- nUnit
- MSTest

MSTest is the standard solution from Microsoft. I did not investigate this thoroughly but after a short search on the Internet it appears that nUnit and MSTest are comparable. xUnit has some specifics, like a less understandable syntax. I will use MS Test , mainly because the integration with VS is pretty much guaranteed.

#### MOQ for mocking

For mocking MOQ is clearly the most used solution, so I will use this framework.



## Coding conventions

I will try to abide to the standards. For a definition check: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions



## Technical aspects

### Using the Swiss Ephemeris

For astronomical calculations, the Swiss Ephemeris (SE) is used. The Se consists of a set of data and a 64-bits dll: *swedll64.dll*.

To access the dll, the attribute [DllImport] is used. All imports from the dll are defined in the file *SeFacade.cs* and the namespace *E4C.be.sefacade*. As an example for the definitions I used the file swissdelphi.pas that Pierre Fontaine and others created to access the same dll from Delphi.



