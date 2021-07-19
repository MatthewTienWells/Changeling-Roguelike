using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [Header("Generation Data"), SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField, Range(0,10)]
    private int offset = 1;
    [SerializeField]
    private bool usingRandomWalkRooms = false;
    [Header("Game Data"), SerializeField]
    public static bool isGameStart = true;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject playerController;
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private List<GameObject> currentEnemies;
    [SerializeField]
    private int maxEnemies = 10;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        foreach (GameObject enemy in currentEnemies)
        {
            Destroy(enemy);
        }
        currentEnemies.Clear();
        //create our dungeon to begin BSP generation
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, 
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        //create a new hash set to store our floor positions
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        //if we are using random walk rooms
        if (usingRandomWalkRooms) 
        {
            //create random walk rooms and add them to our floor set
            floor = CreateRandomWalkRooms(roomsList);
        }
        else
        {
            //create simple square rooms and add them to our floor set
            floor = CreateSimpleRooms(roomsList); 
        }
        //create a list to store room centers for corridor connection later
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        //for each room in the list of rooms
        foreach (var room in roomsList)
        {
            //add their centers to the room centers list and convert them to Vector2Int
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        //create hash set for corridors
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        //add the corridors to the floor set since they are now floors
        floor.UnionWith(corridors);
        //have the tilemapvisualizer paint it for view in editor
        tilemapVisualizer.PaintFloorTiles(floor);
        //pass our wall generator our floors and tilemap visualizer
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
        //spawn the player
        SpawnPlayer(floor);
        //spawn the enemies
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemies(floor); 
        }
        //tell the program it is not the start of the game anymore
        isGameStart = false;
    }

    private HashSet<Vector2Int> CreateRandomWalkRooms(List<BoundsInt> roomsList)
    {
        //hash set for room floor return
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        //iterate through every room in room list
        for (int i = 0; i < roomsList.Count; i++)
        {
            //get the bounds for each room in the list
            var roomBounds = roomsList[i];
            //find its center
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            //create a room at the center according to the random walk scriptable object selected
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            //for each floor position in our room floor set
            foreach (var position in roomFloor)
            {
                //if it is within bounds and offset
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset)
                    && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset)) 
                {
                    //add it to our floor set
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        //create a hash set to return for later floor union
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        //select a random room center from our list
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        //then remove it from the list
        roomCenters.Remove(currentRoomCenter);
        //while there are still centers in our room center list
        while (roomCenters.Count > 0) 
        {
            //find the closest other room center 
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            //remove the closest from the list so we dont find it again on accident
            roomCenters.Remove(closest);
            //create a new set of corridors
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            //set our current room center to the one thats currently stored in closest
            currentRoomCenter = closest;
            //union our new corridors set with our corridors set to remove duplicates before return
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        //create a hash set to define corridors
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        //set the start position for our corridor
        var position = currentRoomCenter;
        //add our current position to the corridor
        corridor.Add(position);
        //while our current position's y value IS NOT equal to our destination y
        while (position.y != destination.y)
        {
            //and if our destination's y is greater than our current position's y
            if (destination.y > position.y)
            {
                //our destination is above us, move up
                position += Vector2Int.up;
            }
            //otherwise if our destination's y is less than our current position's y
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            //add the new position to the corridor
            corridor.Add(position);
        }
        //while our current position's x value IS NOT equal to our destination x
        while (position.x != destination.x)
        {
            //and if our destination's x is greater than our current position's x
            if (destination.x > position.x)
            {
                //it is to our right so move to the right
                position += Vector2Int.right;
            }
            //otherwise if our destination's x is less than our current position's x
            else if (destination.x < position.x) 
            {
                //it is to our left so move to the left
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        //create a new vector for our closest center
        Vector2Int closest = Vector2Int.zero;
        //float for our current center's distance to the closest one
        float distance = float.MaxValue;
        //for each center in our room centers list
        foreach (var position in roomCenters)
        {
            //get the distance between our current center and all other centers
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            //if the current distance is less the our max distance
            if (currentDistance < distance) 
            {
                //set max distance to current distance
                distance = currentDistance;
                //set our closest to the position that we just found
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        //create new hash set for our floors inside the rooms
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        //for each room in roomslist
        foreach (var room in roomsList)
        {
            //iterate through each column minus the offset
            for (int col = offset; col < room.size.x - offset; col++)
            {
                //iterate through each row minus the offset
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    //create a new position at each point in the room
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    //add it to our floor set
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private void SpawnPlayer(HashSet<Vector2Int> floors)
    {
        //conver the hash set to a list and sort it
        List<Vector2Int> floorsList = floors.ToList<Vector2Int>();
        //check if it is the first level or not
        if (isGameStart)
        {
            //if it is
            //get the first room in the list and set it as our spawn point
            Vector2Int spawnPoint = floors.ElementAt(Random.Range(0, floors.Count));
            //spawn the player's controller
            GameObject _pController = Instantiate<GameObject>(playerController, new Vector3(spawnPoint.x, spawnPoint.y, 0), Quaternion.identity);
        }
        //otherwise
        else
        {
            //get the player's sprite renderer
            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
            //get the player's collider
            Collider2D col = player.GetComponent<Collider2D>();
            //disable them
            sr.enabled = false;
            col.enabled = false;
            //get the first room in the list and set it as our spawn point
            var spawnPoint = floors.ElementAt(Random.Range(0, floors.Count));
            //move the player there
            player.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, 0);
            //enable previously disabled components
            sr.enabled = true;
            col.enabled = true;
        }
    }

    private void SpawnEnemies(HashSet<Vector2Int> floors) 
    {
        //convert the hash set to a list and sort it
        List<Vector2Int> floorsList = floors.ToList<Vector2Int>();
        Vector2Int spawnPoint = floors.ElementAt(Random.Range(0, floors.Count));
        GameObject _enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(spawnPoint.x, spawnPoint.y, 0), Quaternion.identity);
        currentEnemies.Add(_enemy);
    }

    private void CreateLevelEnd(List<BoundsInt> roomsList) 
    {
        //TODO: Create a method for spawning the "victory stairs"
    }
}
