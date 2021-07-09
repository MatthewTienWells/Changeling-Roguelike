using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        //run the random walk and store the floor positions in our hash set
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        //Clear all floor tiles
        tilemapVisualizer.Clear();
        //use the tilemap visualizer to paint the floor tiles at the generated positions
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        //paint walls by passing in our floor positions and tilemap visualizer reference
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        //set our current position equal to our start position, since we are just starting
        var currentPosition = position;
        //create hash set for our floor positions
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        //iterate over the number of iterations we have and run our random walk algorithm
        for (int i = 0; i < randomWalkParameters.iterations; i++)
        {
            //run the simple random walk algorithm, passing in our current position and walk length
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
            //Add this new path to our floor positions hash set without the duplicates
            floorPositions.UnionWith(path);
            //if start randomly each iteration is on select a random element
            //of our floor positions hash set to start our walk at again
            if (randomWalkParameters.startRandomlyEachIteration) 
            {
                //select a random element of our floor positions hash set
                //and set our current position equal to it for a "random" iteration start
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
