using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarMap
{
    public AStarSpot[,] Spots;

    public AStarMap(Vector3Int[,] grid, int columns, int rows)
    {
        Spots = new AStarSpot[columns, rows];
        
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Spots[i, j] = new AStarSpot(grid[i, j].x, grid[i, j].y, grid[i, j].z);
            }
        }
    }
    
    private bool IsValidPath(Vector3Int[,] grid, AStarSpot start, AStarSpot end)
    {
        if (end == null)
            return false;
        if (start == null)
            return false;
        if (end.Height >= 1)
            return false;
        return true;
    }
    
    public List<AStarSpot> CreatePath(Vector3Int[,] grid, Vector2Int start, Vector2Int end, int length)
    {
        //if (!IsValidPath(grid, start, end))
        //     return null;

        AStarSpot End = null;
        AStarSpot Start = null;
        var columns = Spots.GetUpperBound(0) + 1;
        var rows = Spots.GetUpperBound(1) + 1;
        
        Spots = new AStarSpot[columns, rows];
        
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Spots[i, j] = new AStarSpot(grid[i, j].x, grid[i, j].y, grid[i, j].z);
            }
        }

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Spots[i, j].AddNeighbors(Spots, i, j);
                if (Spots[i, j].X == start.x && Spots[i, j].Y == start.y)
                    Start = Spots[i, j];
                else if (Spots[i, j].X == end.x && Spots[i, j].Y == end.y)
                    End = Spots[i, j];
            }
        }
        if (!IsValidPath(grid, Start, End))
            return null;
        List<AStarSpot> OpenSet = new List<AStarSpot>();
        List<AStarSpot> ClosedSet = new List<AStarSpot>();

        OpenSet.Add(Start);

        while (OpenSet.Count > 0)
        {
            //Find shortest step distance in the direction of your goal within the open set
            int winner = 0;
            for (int i = 0; i < OpenSet.Count; i++)
                if (OpenSet[i].F < OpenSet[winner].F)
                    winner = i;
                else if (OpenSet[i].F == OpenSet[winner].F)//tie breaking for faster routing
                    if (OpenSet[i].H < OpenSet[winner].H)
                        winner = i;

            var current = OpenSet[winner];

            //Found the path, creates and returns the path
            if (End != null && OpenSet[winner] == End)
            {
                List<AStarSpot> Path = new List<AStarSpot>();
                var temp = current;
                Path.Add(temp);
                while (temp.Prev != null)
                {
                    Path.Add(temp.Prev);
                    temp = temp.Prev;
                }
                if (length - (Path.Count - 1) < 0)
                {
                    Path.RemoveRange(0, (Path.Count - 1) - length);
                }
                return Path;
            }

            OpenSet.Remove(current);
            ClosedSet.Add(current);


            //Finds the next closest step on the grid
            var neighboors = current.Neighbors;
            for (int i = 0; i < neighboors.Count; i++)//look threw our current spots neighboors (current spot is the shortest F distance in openSet
            {
                var n = neighboors[i];
                if (!ClosedSet.Contains(n) && n.Height < 1)//Checks to make sure the neighboor of our current tile is not within closed set, and has a height of less than 1
                {
                    var tempG = current.G + 1;//gets a temp comparison integer for seeing if a route is shorter than our current path

                    bool newPath = false;
                    if (OpenSet.Contains(n)) //Checks if the neighboor we are checking is within the openset
                    {
                        if (tempG < n.G)//The distance to the end goal from this neighboor is shorter so we need a new path
                        {
                            n.G = tempG;
                            newPath = true;
                        }
                    }
                    else//if its not in openSet or closed set, then it IS a new path and we should add it too openset
                    {
                        n.G = tempG;
                        newPath = true;
                        OpenSet.Add(n);
                    }
                    if (newPath)//if it is a newPath caclulate the H and F and set current to the neighboors previous
                    {
                        n.H = Heuristic(n, End);
                        n.F = n.G + n.H;
                        n.Prev = current;
                    }
                }
            }

        }
        return null;
    }

    private int Heuristic(AStarSpot a, AStarSpot b)
    {
        //manhattan
        var dx = Math.Abs(a.X - b.X);
        var dy = Math.Abs(a.Y - b.Y);
        return 1 * (dx + dy);
    }
}

public class AStarSpot
{
    public int X;
    public int Y;
    public int F;
    public int G;
    public int H;
    public int Height = 0;
    public List<AStarSpot> Neighbors;
    public AStarSpot Prev = null;

    public AStarSpot(int x, int y, int height)
    {
        X = x;
        Y = y;
        F = 0;
        G = 0;
        H = 0;
        Neighbors = new List<AStarSpot>();
        Height = height;
    }

    public void AddNeighbors(AStarSpot[,] grid, int x, int y)
    {
        if (x < grid.GetUpperBound(0))
            Neighbors.Add(grid[x + 1, y]);
        if (x > 0)
            Neighbors.Add(grid[x - 1, y]);
        if (y < grid.GetUpperBound(1))
            Neighbors.Add(grid[x, y + 1]);
        if (y > 0)
            Neighbors.Add(grid[x, y - 1]);
    }
}


