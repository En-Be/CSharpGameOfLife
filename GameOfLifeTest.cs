using NUnit.Framework;
using System;
using System.Collections.Generic;

public class GameOfLifeTest
{
    [Test]
    public void outputs_and_input_have_equal_dimensions()
    {
        List<List<char>> twoGrid = new List<List<char>>();
        twoGrid.Add(new List<char>{'.', '.'});
        twoGrid.Add(new List<char>{'.', '.'});
        
        GameOfLife gameTwo = new GameOfLife(twoGrid);
        Assert.That(gameTwo.Play(), Has.Count.EqualTo(twoGrid.Count));
        
        List<List<char>> fourGrid = new List<List<char>>();
        fourGrid.Add(new List<char>{'.', '.', '.', '.'});
        fourGrid.Add(new List<char>{'.', '.', '.', '.'});
        fourGrid.Add(new List<char>{'.', '.', '.', '.'});
        fourGrid.Add(new List<char>{'.', '.', '.', '.'});
        
        GameOfLife gameFour = new GameOfLife(fourGrid);
        Assert.That(gameFour.Play(), Has.Count.EqualTo(fourGrid.Count));
    }
    
    [Test]
    public void doesnt_accept_jagged_lists()
    {
        List<List<char>> jaggedList = new List<List<char>>();
        jaggedList.Add(new List<char>{'.'});
        jaggedList.Add(new List<char>{'.', '.'});
        
        Assert.Throws<InvalidOperationException>(() => new GameOfLife(jaggedList));
    }
    
    [Test]
    public void doesnt_accept_invalid_characters()
    {
        List<List<char>> invalidChars = new List<List<char>>();
        invalidChars.Add(new List<char>{'a', ','});
        invalidChars.Add(new List<char>{'/', ' '});
        
        Assert.Throws<InvalidOperationException>(() => new GameOfLife(invalidChars));
    }
    
    [Test]
    public void counts_the_live_cells_surrounding_a_cell()
    {
        List<List<char>> deadThreeGrid = new List<List<char>>();
        deadThreeGrid.Add(new List<char>{'.', '.', '.'});
        deadThreeGrid.Add(new List<char>{'.', '.', '.'});
        deadThreeGrid.Add(new List<char>{'.', '.', '.'});
        
        GameOfLife deadGame = new GameOfLife(deadThreeGrid);
        Assert.AreEqual(0, deadGame.LiveSurroundingCells(1, 1));
        
        List<List<char>> liveThreeGrid = new List<List<char>>();
        liveThreeGrid.Add(new List<char>{'*', '*', '*'});
        liveThreeGrid.Add(new List<char>{'*', '*', '*'});
        liveThreeGrid.Add(new List<char>{'*', '*', '*'});
        
        GameOfLife liveGame = new GameOfLife(liveThreeGrid);
        Assert.AreEqual(8, liveGame.LiveSurroundingCells(1, 1));
    }
    
    [Test]
    public void only_counts_cells_within_bounds()
    {   
        List<List<char>> liveThreeGrid = new List<List<char>>();
        liveThreeGrid.Add(new List<char>{'*', '*', '*'});
        liveThreeGrid.Add(new List<char>{'*', '*', '*'});
        liveThreeGrid.Add(new List<char>{'*', '*', '*'});
        
        GameOfLife liveGame = new GameOfLife(liveThreeGrid);
        Assert.AreEqual(3, liveGame.LiveSurroundingCells(0, 0));
        Assert.AreEqual(5, liveGame.LiveSurroundingCells(2, 1));
    }
    
    [Test]
    public void cell_dies_with_too_few_neighbours()
    {
        List<List<char>> dyingGrid = new List<List<char>>();
        dyingGrid.Add(new List<char>{'*', '.'});
        dyingGrid.Add(new List<char>{'*', '.'});
        
        GameOfLife dyingGame = new GameOfLife(dyingGrid);
        dyingGame.Play();
        Assert.AreEqual('.', dyingGame.nextGrid[1][0]);
    }
    
    [Test]
    public void cell_dies_with_too_many_neighbours()
    {
        List<List<char>> dyingGrid = new List<List<char>>();
        dyingGrid.Add(new List<char>{'*', '*', '*'});
        dyingGrid.Add(new List<char>{'*', '*', '*'});
        dyingGrid.Add(new List<char>{'*', '*', '*'});
        
        GameOfLife dyingGame = new GameOfLife(dyingGrid);
        dyingGame.Play();
        Assert.AreEqual('.', dyingGame.nextGrid[1][0]);
    }
    
    [Test]
    public void cell_continues_living_with_two_or_three_neighbours()
    {
        List<List<char>> livingGrid = new List<List<char>>();
        livingGrid.Add(new List<char>{'*', '*', '*'});
        livingGrid.Add(new List<char>{'*', '*', '*'});
        livingGrid.Add(new List<char>{'.', '.', '*'});
        
        GameOfLife livingGame = new GameOfLife(livingGrid);
        livingGame.Play();
        Assert.AreEqual('*', livingGame.nextGrid[1][0]);
        Assert.AreEqual('*', livingGame.nextGrid[2][2]);
    }
    
    [Test]
    public void dead_cell_lives_with_three_neighbours_only()
    {
        List<List<char>> reviveGrid = new List<List<char>>();
        reviveGrid.Add(new List<char>{'*', '.', '*'});
        reviveGrid.Add(new List<char>{'.', '*', '*'});
        reviveGrid.Add(new List<char>{'.', '.', '*'});
        reviveGrid.Add(new List<char>{'.', '.', '.'});

        
        GameOfLife reviveGame = new GameOfLife(reviveGrid);
        
        //Console.WriteLine(reviveGame.currentGrid[2][1]);
        //Console.WriteLine(reviveGame.LiveSurroundingCells(2,1));
        
        reviveGame.Play();
        Assert.AreEqual('.', reviveGame.nextGrid[2][0]);
        Assert.AreEqual('*', reviveGame.nextGrid[2][1]);
        Assert.AreEqual('.', reviveGame.nextGrid[1][0]);
    }

    [Test]
    public void GeneratesRandomGrid()
    {
        RandomGrid randomGrid = new RandomGrid(2,2);
        Assert.That(randomGrid.grid, Has.Count.EqualTo(2));
    }
}
