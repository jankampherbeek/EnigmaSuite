# Release 2022.0.2 - detailed planning

Finishing the general approach and adding a calculator for obliquity.

- Preparation
  - Study DI (van Deursen/Seeman).
  - Study MVVM
  - Investigate help-screens
  - Investigate i18N.
- Screens
  - Add validation to screens.
  - Add possibility to start Calculation of obliquity to Dashboard for Calculations.
  - Create screen for Calculation of obliquity. Entry of date, time and calendar. Assumes astronomical year-count. Show results after calling service. Show both mean and true obliquity.
  - Move calculation for JDnr to separate screen.
- Add backend support for validations
- Add i18N, including language definitions for Dutch and English. 
  - Check for an existing solution.
  - Implement solution or create a C# version of Rosetta.
- Define architecture: check MVVM
- Add help screens, using the look-and-feel as used in RadixPro (2008 version), help screens should also be bi-lingual.
- Add hints (bi-lingual)
- Add proper DI: either Pure DI or a framework. 
- Service to access calculation of obliquity.
- Backend for calculation of obliquity.
- Unit tests for all functionality.
- Start with user manual
  - Dutch
  - English