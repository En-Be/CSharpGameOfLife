# C# Game Of Life

## Plays Conway's Game Of Life in the command line

You start with a two dimensional grid of cells, where
each cell is either alive or dead. The grid is finite,
and no life can exist off the edges. When calculating
the next generation of the grid, follow these four rules:

1. Any live cell with fewer than two live neighbours
   dies, as if caused by underpopulation.
2. Any live cell with more than three live neighbours
   dies, as if by overcrowding.
3. Any live cell with two or three live neighbours
   lives on to the next generation.
4. Any dead cell with exactly three live neighbours
   becomes a live cell.

Examples: * indicates live cell, . indicates dead cell

Example input: (4 x 8 grid)
```
........
....*...
...**...
........
```
Example output:
```
........
...**...
...**...
........
```
---
### How to run:

- Clone the repo
- Inside the root directory of the project, run the program with numbers for width and height:

    ```
    dotnet run 4 8
    ```

- The program will run in the console until you press:

    ```
    ctrl + c
    ```

- To see the test coverage, run:

    ```
    dotnet test
    ```
---

### Extension

If I were to spend more time on this, I would test drive the development of:

- Timed refreshing of the grid, maybe 24 frames per second.
- Stop the program if all the cells are dead.
- Run it in a window with coloured squares instead of the command line.