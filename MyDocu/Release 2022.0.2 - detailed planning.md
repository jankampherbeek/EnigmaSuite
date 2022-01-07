# Release 2022.0.2 - detailed planning

Finishing the general approach and adding a calculator for obliquity.

- ~~Preparation~~
  - ~~Study DI (van Deursen/Seeman).~~
  - ~~Study MVVM~~
  - ~~Investigate help-screens.~~  ==> move to Backlog
  - ~~Investigate i18N. Decide if the application will be bi-lingual. --> Only English.~~
- Screens
  - Add validation to screens.
  - Add possibility to start Calculation of obliquity to Dashboard for Calculations.
  - Create screen for Calculation of obliquity. Entry of date, time and calendar. Assumes astronomical year-count. Show results after calling service. Show both mean and true obliquity.
  - Move calculation for JDnr to separate screen.
  - Improve look-and-feel of screens.
- Add backend support for validations
- ~~Add i18N, including language definitions for Dutch and English if deciding to make the application bilingual.~~~~--> only English~~ 
  - ~~Check for an existing solution. Many available.~~ 
  - ~~Implement solution or create a C# version of Rosetta.~~
- Define architecture ( MVVM, DI, layers).
- ~~Add help screens.~~  ==> move to Backlog
- ~~Add hints.~~  ==> move to Backlog
- Add proper DI: either Pure DI or a framework. --> Simple Injector
- Service to access calculation of obliquity.
- ~~Backend for calculation of obliquity.~~
- Unit tests for all functionality.
- ~~Start with user manual~~ ==> move to Backlog