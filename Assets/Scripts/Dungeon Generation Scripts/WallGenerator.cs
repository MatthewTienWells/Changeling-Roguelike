using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        //find all the wall positions in our dungeon
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        //create walls that face the cardinal directions
        CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorPositions);
        //create the corner walls
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
    }

    /// <summary>
    /// Creates walls facing in the cardinal directions
    /// </summary>
    /// <param name="tilemapVisualizer"></param>
    /// <param name="basicWallPositions"></param>
    /// <param name="floorPositions"></param>
    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        //for each position in the basic wall positions has set
        foreach (var position in basicWallPositions)
        {
            //variable to build our binary number, it will be used to generate corners and diagonals
            string neighborsBinaryType = "";
            //for each cardinal direction in the cardinal direction list
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                //get the position of our neighbor
                var neighborPosition = position + direction;
                //if the neighbor is one of our floors
                if (floorPositions.Contains(neighborPosition))
                {
                    //add 1 to its binary type
                    neighborsBinaryType += "1";
                }
                //otherwise
                else 
                {
                    //add 0 to its binary type
                    neighborsBinaryType += "0";
                }
            }
            //paint wall
            tilemapVisualizer.PaintSingleBasicWall(position, neighborsBinaryType);
        }
    }

    /// <summary>
    /// Creates corner walls using the diagonal directions list in Directions2D
    /// </summary>
    /// <param name="tilemapVisualizer"></param>
    /// <param name="basicWallPositions"></param>
    /// <param name="floorPositions"></param>
    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        //for each position in our corner wall positions set
        foreach (var position in cornerWallPositions)
        {
            //variable to build our binary number, it will be used to generate corners and diagonals
            string neighborsBinaryType = "";
            //for each direction in our eight directional list
            foreach (var direction in Direction2D.eightDirectionalList)
            {
                //get the position of our neighbor
                var neighborPosition = position + direction;
                //if the neighbor is one of our floors
                if (floorPositions.Contains(neighborPosition))
                {
                    //add 1 to its binary type
                    neighborsBinaryType += "1";
                }
                //otherwise
                else
                {
                    //add 0 to its binary type
                    neighborsBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighborsBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        //create a new hash set to deal with duplicates
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        //for each position in the floor positions hash set
        foreach (var position in floorPositions)
        {
            //and for each direction in the direction list
            foreach (var direction in directionList)
            {
                //compute neighbor positions
                var neighbourPosition = position + direction;
                //if the floor position does NOT contain the neighbor position
                if (floorPositions.Contains(neighbourPosition) == false) 
                {
                    //add the neighbor position to our wall positions hash set
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
}
