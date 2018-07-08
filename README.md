# Palletizing-DES
A descrete event system (DES) of a palletizing process created in Universal Windows Platform for Control Theory study course. 

## Structure
- The projects follows the MVVM pattern separating the bussiness logic and the view. 
- Dependency injection was applied for decoupling classes.
- For either purpouses MVVM Light Toolkit was used.

## Functionalities
- Displaying the current state of DES
- Possibility to nagivagte between states by chosing an event available from current state
- Displaying previous state and event
- Check DES for blocks occurrences (states that cannot be left once inside)
- Finding possibly shortest path between two chosen states
- Loading DES from config file in JSON format

## Visual overview

- ### Overral ![Overview](https://drive.google.com/uc?id=1SN3tdj4wh_BSndLuiwPJdGQsUHLs4qsX)


- ### Navigation between states ![Overview](https://drive.google.com/uc?id=1SGR3UBErOoHi-oD0nnp5bjkHxsNXrnFg)


- ### Blocks check ![Overview](https://drive.google.com/uc?id=1zC4U5YQc5YfELMR-uUXZi0JQUGWBbwVp)


- ### Loading DES from chosen file ![Overview](https://drive.google.com/uc?id=13w7tBn6zsp7rqAB-gsgNPCwiuiyhTKNZ)


- ### Shortest path finding displayed on scrollable view ![Overview](https://drive.google.com/uc?id=1d1vzwkJmcDXTqLK6KRnIO4GP65NxsiO4)
  ![Overview](https://drive.google.com/uc?id=1s02YtdXN7ThGM3alUkQtMaZzHoU3Hibp)

## Raspberry PI
Universal Windows Platform app is capable of running on any platform that is able to run Windows 10 system. On a Raspberry PI Windows 10 IoT can be installed, so the app was also deployd on the ARM platform and as a result it was working just fine.

![Overview](https://drive.google.com/uc?id=1paM52shH3fe2kXW73chBb-kumFMFaAQO)


License
----

MIT
