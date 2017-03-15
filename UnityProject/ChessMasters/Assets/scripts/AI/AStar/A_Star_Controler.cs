using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class A_Star_Controler
{
    //stack of next moves for white
	public Stack<GameObject> whiteMoves;
    //stack of next moves for black
    public Stack<GameObject> blackMoves;

    int[] curentTileL;
    Ground_Tile curentTileObj;

    int moveCost = 10;

    //must be resorted by Ground_Tile.F on all additions and deleations
	private MinHeap<Ground_Tile> oppenList;

    private Dictionary<int, Ground_Tile> closedList;

    //the board script in the Ground object
    public Tile_Board Board = new Tile_Board();


    public void runAStar()
    {
		Debug.Log ("A Star Running");
        //reseting the board with new start and end points
        Board.updateBoard();
		//empteing the black move list
		whiteMoves = new Stack<GameObject> ();

		while (whiteMoves.Count > 0) 
		{
			whiteMoves.Pop ();
		}

        AStarFromStart(Board.bishopWValues.curentX, Board.bishopWValues.curentY);
        createPath(curentTileObj, 'W');

		//empteing the white move list
		blackMoves = new Stack<GameObject> ();

		while (blackMoves.Count > 0) 
		{
			blackMoves.Pop ();
		}

        AStarFromStart(Board.bishopBValues.curentX, Board.bishopBValues.curentY);
        createPath(curentTileObj, 'B');
    }

    public void AStarFromStart(int startX, int startY)
    {
		//creating the oppen and closed list
		oppenList = new MinHeap<Ground_Tile>();
		closedList = new Dictionary<int, Ground_Tile> ();

        //adding the start tile to the oppen list and setting it as the curent Tile
		oppenList.Insert(Board.board[startY, startX]);
        //oppenList.Insert(curentTileObj);

		int numTilesLookedAt = 0;
        //looping thrue untill hitting the end point
        while (oppenList.Count > 0)
        {
            //setting the curent tile to the next one in the oppen list
            curentTileObj = oppenList.ExtractMin();
			numTilesLookedAt++;
			//Debug.Log ("Tile curently located = " + curentTileObj.tileY + "" + curentTileObj.tileX);
           // Debug.Log("Tile is pathable = "+ curentTileObj.pathable);
			//curentTileL[0] = curentTileObj.tileY;
			//curentTileL[1] = curentTileObj.tileX;
			//Debug.Log ("Number of tiles looked at = " + numTilesLookedAt);

            //cheking to see if you are at the end
            if (curentTileObj.end)
            {
                break;
            }

            //else creating the tiles adjacent to curent tile
            createNeighbors();
            //adding curent tile to closed list
            //Debug.Log("adding Ground_Tile at Board Position \n" + curentTileObj.tileY + " "+ curentTileObj.tileX+  " \n To closed list");
            closedList.Add(curentTileObj.tileY * Board.board.GetLength(1) + curentTileObj.tileX, curentTileObj);
        }
			
    }

    private void createNeighbors()
    {
		//Debug.Log ("creating neighbors");
        int[] lookLocation = { curentTileObj.tileY - 1, curentTileObj.tileX - 1 };
        //looping thru upper left diagonal neighbors
        while (lookLocation[0] >= 0 && lookLocation[1] >= 0)
        {
			if(Board.board[lookLocation[0],lookLocation[1]].pathable == false) 
			{
				break;
			}
			chekNaborsViablity (Board.board [lookLocation [0], lookLocation [1]], lookLocation);
            lookLocation[0]--;
            lookLocation[1]--;
        }

        lookLocation[0] = curentTileObj.tileY + 1;
        lookLocation[1] = curentTileObj.tileX - 1;
        //looping thru lower left diagonal neighbors
        while (lookLocation[0] < Board.board.GetLength(0) && lookLocation[1] >= 0)
        {
			if(Board.board[lookLocation[0],lookLocation[1]].pathable == false) 
			{
				break;
			}
			chekNaborsViablity (Board.board [lookLocation [0], lookLocation [1]], lookLocation);
            lookLocation[0]++;
            lookLocation[1]--;
        }

        lookLocation[0] = curentTileObj.tileY - 1;
        lookLocation[1] = curentTileObj.tileX + 1;
        //looping thru upper right diagonal neighbors
        while (lookLocation[0] >= 0 && lookLocation[1] < Board.board.GetLength(1))
        {
			if(Board.board[lookLocation[0],lookLocation[1]].pathable == false) 
			{
				break;
			}
            chekNaborsViablity(Board.board[lookLocation[0], lookLocation[1]], lookLocation);
            lookLocation[0]--;
            lookLocation[1]++;
        }

        lookLocation[0] = curentTileObj.tileY + 1;
        lookLocation[1] = curentTileObj.tileX + 1;
        //looping thru lower right diagonal neighbors
        while (lookLocation[0] < Board.board.GetLength(0) && lookLocation[1] < Board.board.GetLength(1))
        {
			if(Board.board[lookLocation[0],lookLocation[1]].pathable == false) 
			{
				break;
			}
            chekNaborsViablity(Board.board[lookLocation[0], lookLocation[1]], lookLocation);
            lookLocation[0]++;
            lookLocation[1]++;
        }
    }

    private void chekNaborsViablity(Ground_Tile lookTile, int[] lookLocation)
    {
        int newG = moveCost;
        //cheking to see if the tile is pathable or if it is in the closed list
        if (lookTile.pathable == true && !closedList.ContainsKey(lookLocation[0] * Board.board.GetLength(1) + lookLocation[1]))
        {
            //Debug.Log("neighbor "+ i +" "+j +" is pathable and not in closed list");
            //setting its G equal to the cost of its position from the curent point added to the curent points G
            newG += curentTileObj.G;
            //cheking if the new tile is in the oppen list
            if (oppenList.contains(lookTile))
            {
				//Debug.Log("" + lookTile.tileY + " " + lookTile.tileX + " is in open list");
                //check to see if its g is smaller or greater then the curent one
                if (lookTile.G < oppenList.Peek(lookTile).G)
                {
                    int heapIndexOfLook = oppenList.PeekL(lookTile);
                    //change the G value and Parent
                    oppenList.changeValuesAt(heapIndexOfLook, newG, curentTileObj);
                }
            }
            else
            {
                //Debug.Log(i + " " + j + " is new");
                //setting the tiles values and puting it in the oppen list
                lookTile.updateValues(newG);
                lookTile.parent = curentTileObj;
                oppenList.Insert(lookTile);
            }
        }
    }

    private void createPath(Ground_Tile end, char color)
    {
        //Debug.Log("path found");
        //Debug.Log("end tile location =" + end.tileY + "" + end.tileX);
        Ground_Tile curentTile = end;

		/*if (color == 'W')
		{
			//whiteMoves.Push(curentTile.Tile);
			whiteMoves.Push(GameObject.FindGameObjectWithTag("Tile_" + curentTile.tileY + "" + curentTile.tileX));
		}
		else
		{
			//blackMoves.Push(curentTile.Tile);
			blackMoves.Push(GameObject.FindGameObjectWithTag("Tile_" + curentTile.tileY + "" + curentTile.tileX));
		}*/


        while (curentTile.parent != null)
        {
           //Debug.Log("previose tile location in path =" + curentTile.tileY + "" + curentTile.tileX);
            if (color == 'W')
            {
                //whiteMoves.Push(curentTile.Tile);
				whiteMoves.Push(GameObject.FindGameObjectWithTag("Tile_" + curentTile.tileY + "" + curentTile.tileX));
            }
            else
            {
                //blackMoves.Push(curentTile.Tile);
				blackMoves.Push(GameObject.FindGameObjectWithTag("Tile_" + curentTile.tileY + "" + curentTile.tileX));
            }
            curentTile = curentTile.parent;
        }
    }
}