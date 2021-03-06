# Enigma suite - Release notes

[TOC]



## Release 2022.0.3

2022- 02-24 Code-only release,

- Calculation of planets and houses. 
- Input form for data for chart calculation.
- Calculator for Julian Day number



## Release 2022.0.2

2022-01-12 - Code-only release

Mainly technical improvements

- Implementing Dependency Injection, using Simple Injector.
- Implementing MVVM.
- Functional additions
  - Calculation of Obliquity.
  - Basic error handling.


## Release 2022.0.1

2022-01-02 - Code-only release

Checks the feasibility of choices made. A minimal implementation with the following functionality:

- Frontend that shows an image and presents the choice for performing calculations. Using WPF.
- Simple screen for calculations. No menu yet, gives input for calculation of JD number.
- Construction of the required classes in the cs part of the calculations screen (temporary solution for DI).
- Service for handling the calculation of the JD number.
- A Facade that makes it possible to access the 64-bits dll from the Swiss Ephemeris.
- Backend handling that intermediates between the Service and the Facade.
- Simple unit test for the backend handling, including a mocked object.

