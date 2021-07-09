using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap; //tilemap for our floor
    [SerializeField]
    private TileBase floorTile, wallTop, wallRight, wallLeft, wallBottom, wallFull, 
        wallInnerCornerDownLeft, wallInnerCornerDownRight, 
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft; //tiles for all of tilemaps

    /// <summary>
    /// Paints all the floor tiles in a list
    /// </summary>
    /// <param name="floorPositions"></param>
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) 
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    /// <summary>
    /// loops through all the tiles in the passed in list and paints them
    /// </summary>
    /// <param name="positions"></param>
    /// <param name="tilemap"></param>
    /// <param name="tile"></param>
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    /// <summary>
    /// paints wall tiles with the same method as floor tiles,
    /// using the wall tilemap and wall tiles instead
    /// </summary>
    /// <param name="position"></param>
    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        //convert our binary type into a 32-bit integer
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        //create an empty tile to assign our correct tile to
        TileBase tile = null;
        //if our type value is contained in our set of top wall values
        if (WallTypesHelper.wallTop.Contains(typeAsInt)) 
        {
            //assign the wall top tile to our to-be-pained tile
            tile = wallTop;
        }
        //otherwise if our type value is contained in our set of right side wall values
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            //assign the wall right tile to our to-be-pained tile
            tile = wallRight;
        }
        //otherwise if our type value is contained in our set of left side wall values
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            //assign the wall left tile to our to-be-pained tile
            tile = wallLeft;
        }
        //otherwise if our type value is contained in our set of bottom wall values
        else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
        {
            //assign the wall bottom tile to our to-be-pained tile
            tile = wallBottom;
        }
        //if our tile is NOT null
        if (tile != null) 
        {
            //paint the tile
            PaintSingleTile(wallTilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        //set tile position equal to its cell position on the tilemap
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        //paint the specified tile in its specified position
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear() 
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        //convert our binary type into a 32-bit integer
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        //create an empty tile to assign our correct tile to
        TileBase tile = null;
        //if our type value is contained in our set of inner corner bottom left wall values
        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)) 
        {
            //assign that tile to our tile-to-be-painted
            tile = wallInnerCornerDownLeft;
        }
        //Otherwise if our type value is contained in our set of inner corner bottom right wall values
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            //assign that tile to our tile-to-be-painted
            tile = wallInnerCornerDownRight;
        }
        //Otherwise if our type value is contained in our set of diagonal corner bottom left wall values
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            //assign that tile to our tile-to-be-painted
            tile = wallDiagonalCornerDownLeft;
        }
        //Otherwise if our type value is contained in our set of diagonal corner bottom right wall values
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            //assign that tile to our tile-to-be-painted
            tile = wallDiagonalCornerDownRight;
        }
        //Otherwise if our type value is contained in our set of diagonal corner top right wall values
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            //assign that tile to our tile-to-be-painted
            tile = wallDiagonalCornerUpRight;
        }
        //Otherwise if our type value is contained in our set of diagonal corner top left wall values
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            //assign that tile to our tile-to-be-painted
            tile = wallDiagonalCornerUpLeft;
        }
        //Otherwise if our type value is contained in our set of full wall values
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            //assign that tile to our tile-to-be-painted
            tile = wallFull;
        }
        //Otherwise if our type value is contained in our set of bottom wall values
        else if (WallTypesHelper.wallBottomEightDirections.Contains(typeAsInt))
        {
            //assign that tile to our tile-to-be-painted
            tile = wallBottom;
        }
        //if our tile is NOT null
        if (tile != null)
        {
            //paint the tile
            PaintSingleTile(wallTilemap, tile, position);
        }
    }
}
