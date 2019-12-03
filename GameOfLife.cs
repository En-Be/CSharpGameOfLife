using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        RandomGrid grid = new RandomGrid(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
        GameOfLife game = new GameOfLife(grid.grid);
        while(true)
        {
            game.Play();
        }
    }
}

public class GameOfLife
{
    public List<List<char>> currentGrid;
    public List<List<char>> nextGrid;
    
    public GameOfLife(List<List<char>> grid)
    {    
        if(GridIsValid(grid))
        {
            currentGrid = new List<List<char>>(grid);
            nextGrid = DeepCopyList(currentGrid);
            printNextGrid();
        }
    }
    
    public bool GridIsValid(List<List<char>> grid)
    {
        int columnsWidth = grid[0].Count;
        
        foreach(List<char> column in grid)
        {
            if(column.Count != columnsWidth)
            {
                throw new InvalidOperationException("column heights must be consistent");
            }
            
            foreach(char cell in column)
            {
                if(cell != '.' && cell != '*')
                {
                    throw new InvalidOperationException("cells must be dead '.' or alive '*'");
                }
            }
        }
        
        return true;
    }
    
    public List<List<char>> Play()
    {
        for(int x = 0; x < currentGrid.Count; x++)
        {
            for(int y = 0; y < currentGrid[0].Count; y++)
            {
                nextGrid[x][y] = LivesOrDies(LiveSurroundingCells(x, y), (currentGrid[x][y]));
            }
        }
        
        printNextGrid();
        currentGrid = nextGrid;
        return currentGrid;
    }
    
    public int LiveSurroundingCells(int x, int y)
    {
        int liveSurroundingCells = 0;

        for(int column = -1; column < 2; column++)
        {
            if(x+column >= 0 && x+column < currentGrid.Count)
            {
                for(int row = -1; row < 2; row++)
                {
                    if(y+row >= 0 && y+row < currentGrid[x+column].Count)
                    {
                        if(currentGrid[x+column][y+row] == '*')
                        {
                            liveSurroundingCells++;
                        }
                    }

                }
            }
        }
        
        if(currentGrid[x][y] == '*')
        {
            liveSurroundingCells--;
        }
        
        return liveSurroundingCells;
    }
    
    public char LivesOrDies(int neighbours, char cell)
    {
        if(cell == '.')
        {
            if(neighbours == 3)
            {
                return '*';
            }
            else
            {
                return '.';
            }
        }
        else
        {
            if(neighbours == 2 || neighbours == 3)
            {
                return '*';
            }
            else
            {
                return '.';
            }
        }
    }
    
    private void printNextGrid()
    {
        Console.WriteLine();
        foreach(List<char> row in nextGrid)
        {
            foreach(char cell in row)
            {
                Console.Write(cell);
            }
            
            Console.WriteLine();
        }
    }
    
    private List<List<char>> DeepCopyList(List<List<char>> fromGrid)
    {
        List<List<char>> newGrid = new List<List<char>>();
        
        foreach(List<char> fromRow in fromGrid)
        {
            List<char> newRow = new List<char>();
            
            foreach(char fromCell in fromRow)
            {
                char newCell = fromCell;
                newRow.Add(newCell);
            }
            
            newGrid.Add(newRow);
        }
        
        return newGrid;
    }
}

public class RandomGrid
{
    public List<List<char>> grid = new List<List<char>>();

    public RandomGrid(int x, int y)
    {
        for(int columns = 0; columns < x; columns++)
        {
            List<char> column = new List<char>();
            for(int rows = 0; rows < y; rows++)
            {
                column.Add(ChooseRandomChar());
            }
            grid.Add(column);
        }
    }

    private char ChooseRandomChar()
    {
        Random random = new Random();
        int i = random.Next(2);
        if(i == 0)
        {
            return '.';
        }
        else
        {
            return '*';
        }
    }
}
