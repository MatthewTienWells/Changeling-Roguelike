using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength) 
    {
        //path to be returned for our random walk
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        //add start position to path
        path.Add(startPosition);
        //set our previous position to our start position before moving on
        var previousPosition = startPosition;

        //move one step in a random direction from our previous position
        for (int i = 0; i < walkLength; i++)
        {
            //move in a random cardinal direction
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            //add this new position to our path
            path.Add(newPosition);
            //set our previous position equal to our new position since it is now our previous
            previousPosition = newPosition;
        }
        return path;
    }

    /// <summary>
    /// Creates a corridor at a specified length and then returns the path created.
    /// It is a list so that we can return the last position in the newly created corridor
    /// to ensure all parts of the corridor connect.
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="corridorLength"></param>
    /// <returns></returns>
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength) 
    {
        //make a new list of vectors to represent the corridor
        List<Vector2Int> corridor = new List<Vector2Int>();
        //select a direction to generate in
        var direction = Direction2D.GetRandomCardinalDirection();
        //save our position, which is the start position
        var currentPosition = startPosition;
        //add that position to the corridor
        corridor.Add(currentPosition);
        for (int i = 0; i < corridorLength; i++)
        {
            //move in a direction
            currentPosition += direction;
            //add this position to the corridor
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight) 
    {
        //create a new queue to hold our room we will split, a queue always us a "first-in first-out" method of access
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        //list to hold the split rooms
        List<BoundsInt> roomsList = new List<BoundsInt>();
        //add our spaceToSplit to the Queue
        roomsQueue.Enqueue(spaceToSplit);
        //while we have rooms to split
        while (roomsQueue.Count > 0) 
        {
            //take the room out of the queue
            var room = roomsQueue.Dequeue();
            //if the rooms size is greater or equal to our min height AND
            //its width is greater or equal to our min Width
            if (room.size.y >= minHeight && room.size.x >= minWidth) 
            {
                //split the room randomly horizontally or vertically
                if (UnityEngine.Random.value < 0.5f)
                {
                    //if the room is bigger than two times our min height
                    if (room.size.y >= minHeight * 2)
                    {
                        //check if we can split it horizontally
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    //else if the room is bigger than two times our min width
                    else if (room.size.x >= minWidth * 2)
                    {
                        //check if we can split it vertically
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    //else if the room is bigger than both minimums but not to a degree of 2
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        //add it to our list of rooms
                        roomsList.Add(room);
                    }
                }
                else 
                {
                    //else if the room is bigger than two times our min width
                    if (room.size.x >= minWidth * 2)
                    {
                        //check if we can split it vertically
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    //if the room is bigger than two times our min height
                    else if (room.size.y >= minHeight * 2)
                    {
                        //check if we can split it horizontally
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    //else if the room is bigger than both minimums but not to a degree of 2
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        //add it to our list of rooms
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    /// <summary>
    /// used in the Binary Space Partitioning method of generation to split rooms vertically
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="roomsQueue"></param>
    /// <param name="room"></param>
    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        //get a random value for the split point, 
        //Random Range already takes off the end of our room size 
        //so we dont need -1
        var xSplit = Random.Range(1, room.size.x);
        //create our room one by passing the correct parameters (room.min is the bottom left corner of our rooms presplit)
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        //create room 2 with consideration for its new room.min.x (room.min.x + xSplit) and size
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z)); //this wrapped line is the new size of room 2
        //add both newly created rooms to the Queue
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    /// <summary>
    /// used in the Binary Space Partitioning method of generation to split rooms horizontally
    /// </summary>
    /// <param name="minHeight"></param>
    /// <param name="roomsQueue"></param>
    /// <param name="room"></param>
    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        //get a random value for the split point, 
        //Random Range already takes off the end of our room size 
        //so we dont need -1
        var ySplit = Random.Range(1, room.size.y);
        //create our room one by passing the correct parameters
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        //create room 2 with consideration for its new room.min.y
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z)); //this wrapped line is the new size of room 2
        //add both newly created rooms to the Queue
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}
/// <summary>
/// A helper class for easy access to direction data
/// </summary>
public static class Direction2D 
{
    //List of cardinal directions
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,0) //LEFT
    };

    //List of diagonal directions
    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1,-1), //DOWN-LEFT
        new Vector2Int(-1,1) //LEFT-UP
    };

    //both of the above lists combined in a clockwise fashion
    public static List<Vector2Int> eightDirectionalList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,-1), //DOWN-LEFT
        new Vector2Int(-1,0), //LEFT
        new Vector2Int(-1,1) //LEFT-UP
    };

    /// <summary>
    /// returns a random cardinal direction from the list of cardinal directions
    /// </summary>
    /// <returns>random Vector2Int of cardinal direction (up, right, down, left)</returns>
    public static Vector2Int GetRandomCardinalDirection() 
    {
        return cardinalDirectionsList[UnityEngine.Random.Range(0, cardinalDirectionsList.Count)];
    }
}