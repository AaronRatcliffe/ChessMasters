using UnityEngine;
using System.Collections;

public class Ground_Tile {
    //A* values
    public int G;
    public int H;
    public int F;
    //special tile cases
    public bool pathable { get; set; }
    public bool end { get; set; }
    public char color { get; set; }
    //parent of this tile
    public Ground_Tile parent { get; set; }
    //the Tile object in the game space
    public GameObject Tile { get; set; }

    //tiles location in the board
    public int tileX { get; set; }
    public int tileY { get; set; }

    public Ground_Tile(GameObject tile) {
        tileX = tile.GetComponent<TileInfo>().boardColumn;
		tileY = tile.GetComponent<TileInfo>().boardRow;
        H = 0;
        G = 0;
        parent = null;
        pathable = tile.GetComponent<TileInfo>().pathable;
        color = tile.GetComponent<TileInfo>().color;
        end = false;
        Tile = tile;
        F = G + H;
    }
    public void updateValues(int h, int g)
    {
        H = h;
        G = g;
        F = G + H;
    }
    public void updateValues(int g)
    {
        G = g;
        F = G + H;
    }
    public bool equals(Ground_Tile other)
    {
        return this.Tile.tag.Equals(other.Tile.tag);
    }

    public int CompareTo(Ground_Tile other)
    {
        int result = 1;
        if (this.F == other.F)
        {
            result = 0;
        }
        else if(this.F < other.F)
        {
            result = -1;
        }
        return result;
    }
}
