# Release 2022.0.4 -  detailed planning

- Data input for the calculation of charts.
  - Domain object to hold inputted data.
  - Error handling.

- Calculation of planetary positions and houses (Sun .. Pluto, Chiron, Nodes).
  - View and viewform for results.
    - Layout.
    - Menu.
    - Implementation UI.

- Using one standard modus: Western Astrology.
  - Define.
  - Temporarily solution: hardcoded.

- Calculated results are shown in a textual table.
  - Tabbed pages for ecliptical, equatorial and horizontal positions.
  - Where applicable, positions are given with glyphs.

- Add help screens, using the look-and-feel as used in RadixPro (2008 version).

- Use mutex to force single instance being run. See Chowdhury, WPF dev. p. 46.

- Unit testing:
  - Create tests for viewmodels.

- Drawing of chart, with equal signs

  