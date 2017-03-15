using UnityEngine;
using System.Collections;

public class Tile_Board
{
    //number of tiles the board will be divided into
    public static int numTileRow = 8;
    public static int numbTileColumn = 8;

    //array holding all tiles
    public Ground_Tile[,] board = new Ground_Tile[numbTileColumn, numTileRow];

    int[] endTileLocationW = new int[2];

    int[] endTileLocationB = new int[2];


    GameObject bishopB = GameObject.Find("bishopB");
    GameObject bishopW = GameObject.Find("bishopW");
    GameObject player = GameObject.Find("Character");

    CharacterMovement playerValues = GameObject.Find("Character").GetComponent<CharacterMovement>();
    public BishopMove bishopBValues = GameObject.Find("bishopB").GetComponent<BishopMove>();
    public BishopMove bishopWValues = GameObject.Find("bishopW").GetComponent<BishopMove>();

    // Use this for initialization
    public Tile_Board()
    {
        //create the tiles in there correct locations
        for (int Y = 0; Y < numTileRow; Y++)
        {
            for (int X = 0; X < numbTileColumn; X++)
            {
                //creating a Gound_Tile out of the tile at the correct matrix locaiton
				board[X, Y] = new Ground_Tile(GameObject.FindGameObjectWithTag("Tile_" + X + "" + Y));
            }
        }

    }

    //resets the board with new targets and updates the huristics of the tiles
    public void updateBoard()
    {
        //resetting the board to all null
        resetBoard();
        //setting the start end points
        setEndPoints();
    }

    public void setHeuristic(int[] tileAddress)
    {
        if (board[tileAddress[0], tileAddress[1]].color == 'W')
        {
            //calculating how far away the end pont is from the created tile and sets ing it
            board[tileAddress[0], tileAddress[1]].updateValues((Mathf.Abs(tileAddress[0] - endTileLocationW[0])) + (Mathf.Abs(tileAddress[1] - endTileLocationW[1])));
        }
        else
        {
            //calculating how far away the end pont is from the created tile and sets ing it
            board[tileAddress[0], tileAddress[1]].updateValues((Mathf.Abs(tileAddress[0] - endTileLocationB[0])) + (Mathf.Abs(tileAddress[1] - endTileLocationB[1])));
        }
    }

    public void setG(int[] tileAddress, int newG)
    {
        board[tileAddress[0], tileAddress[1]].updateValues(newG);
    }

    public void setEndPoints()
    {
        Vector2 playerBishop;
        //cheking to see what color the player is standing on
        if (board[playerValues.locationX, playerValues.locationY].color == 'W')
        {
            //finding the angle between the player and the black bishop
            playerBishop = bishopB.transform.position - player.transform.position;
        }
        else
        {
            //finding the angel between the player and the white bishop
            playerBishop = bishopW.transform.position - player.transform.position;
        }
        float angle = AngleBetweenVector2(playerBishop, (Vector2)player.transform.right);
        int xAjust = 0;
        int yAjust = 0;
        //determoning which side to place the target depending on the size of the angle
        if (angle >= 0 && angle < 45 || angle >= -360 && angle < -315)
        {
            xAjust = -1;
            yAjust = 0;
            if (playerValues.locationX == 0)
            {
                xAjust = 0;
                yAjust = -1;
                if (playerValues.locationY == 7)
                {
					yAjust = 1;
					xAjust = 0;
                }
            }
        }
        else if (angle >= 45 && angle < 90 || angle >= -315 && angle < -270)
        {
            xAjust = 0;
            yAjust = -1;
            if (playerValues.locationY == 7)
            {
                yAjust = 0;
                xAjust = -1;
                if (playerValues.locationX == 0)
                {
                    yAjust = 1;
                    xAjust = 0;
                }
            }

        }
        else if (angle >= 90 && angle < 135 || angle >= -270 && angle < -225)
        {
            xAjust = 0;
            yAjust = -1;
            if (playerValues.locationY == 7)
            {
                yAjust = 0;
                xAjust = 1;
                if (playerValues.locationX == 7)
                {
                    yAjust = 1;
                    xAjust = 0;
                }

            }
        }
        else if (angle >= 135 && angle < 180 || angle >= -225 && angle < -180)
        {
            xAjust = 1;
            yAjust = 0;
            if (playerValues.locationX == 7)
            {
                yAjust = -1;
                xAjust = 0;
                if (playerValues.locationY == 7)
                {
                    yAjust = 1;
                    xAjust = 0;
                }
            }
        }
        else if (angle >= 180 && angle < 225 || angle >= -180 && angle < -135)
        {
            xAjust = 1;
            yAjust = 0;
            if (playerValues.locationX == 7)
            {
                yAjust = 1;
                xAjust = 0;
                if (playerValues.locationY == 0)
                {
                    yAjust = -1;
                    xAjust = 0;
                }
            }
        }
        else if (angle >= 225 && angle < 270 || angle >= -135 && angle < -90)
        {
            xAjust = 0;
            yAjust = 1;
            if (playerValues.locationY == 0)
            {
                yAjust = 0;
                xAjust = 1;
                if (playerValues.locationX == 7)
                {
                    yAjust = -1;
                    xAjust = 0;
                }
            }
        }
        else if (angle >= 270 && angle < 315 || angle >= -90 && angle < -45)
        {
            xAjust = 0;
            yAjust = 1;
            if (playerValues.locationY == 0)
            {
                yAjust = 0;
                xAjust = -1;
                if (playerValues.locationX == 0)
                {
                    yAjust = -1;
                    xAjust = 0;
                }
            }
        }
        else if (angle >= 315 && angle < 360 || angle >= -45 && angle < 0)
        {
            xAjust = -1;
            yAjust = 0;
            if (playerValues.locationX == 0)
            {
                yAjust = 1;
                xAjust = 0;
                if (playerValues.locationY == 0)
                {
                    yAjust = -1;
                    xAjust = 0;
                }
            }
        }

        board[playerValues.locationX + xAjust, playerValues.locationY - yAjust].end = true;
        //setting one of the end points to players curint location
        board[playerValues.locationX, playerValues.locationY].end = true;
        if (board[playerValues.locationX, playerValues.locationY].color == 'W')
        {
            endTileLocationW[0] = playerValues.locationX;
            endTileLocationW[1] = playerValues.locationY;
            endTileLocationB[0] = playerValues.locationX + xAjust;
            endTileLocationB[1] = playerValues.locationY - yAjust;
        }
        else
        {
            endTileLocationB[0] = playerValues.locationX;
            endTileLocationB[1] = playerValues.locationY;
            endTileLocationW[0] = playerValues.locationX + xAjust;
            endTileLocationW[1] = playerValues.locationY - yAjust;
        }
		Debug.Log (" " + endTileLocationW [0] + " " +  endTileLocationW [1] + "/n" +  endTileLocationB [0] + " " + endTileLocationB [1]);
    }

    public void resetBoard()
    {
        for (int Y = 0; Y < numTileRow; Y++)
        {
            for (int X = 0; X < numbTileColumn; X++)
            {
                //removing old end points
                board[X, Y].end = false;
                //removing old G values
                board[X, Y].updateValues(0);
                board[X, Y].parent = null;
            }
        }

    }
    //by Answer by DarkMatter
    //http://answers.unity3d.com/questions/317648/angle-between-two-vectors.html
    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

}
