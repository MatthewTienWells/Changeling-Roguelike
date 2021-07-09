using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    //abstract method for generating dungeons
    public void GenerateDungeon() 
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    //this method will allow us to choose which method of generation is being used
    protected abstract void RunProceduralGeneration();
}
