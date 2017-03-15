using UnityEngine;
using System.Collections;

public class TileInfo : MonoBehaviour {
    public bool pathable;
    //white tiles are 0 black tiles are 1
    public char color;
    public int boardRow;
    public int boardColumn;

	//variable for whether an enemy is on the tile
	public bool hostile;

	GameObject currentBishop;
	int currentBishopX;
	int currentBishopY;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(color == 'W'){
			currentBishop = GameObject.Find("bishopW");
			currentBishopX = currentBishop.GetComponent<BishopMove>().curentX;
			currentBishopY = currentBishop.GetComponent<BishopMove>().curentY;
			if (currentBishopX == boardColumn && currentBishopY == boardRow) {
				hostile = true;
			} else {
				hostile = false;
			}
		}
		if (color == 'B') {
			currentBishop = GameObject.Find ("bishopB");
			currentBishopX = currentBishop.GetComponent<BishopMove>().curentX;
			currentBishopY = currentBishop.GetComponent<BishopMove>().curentY;
			if (currentBishopX == boardColumn && currentBishopY == boardRow) {
				hostile = true;
			} else {
				hostile = false;
			}
		}
	}
}
