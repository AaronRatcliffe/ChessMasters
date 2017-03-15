using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveList : MonoBehaviour {

    A_Star_Controler aStar;
	Transform bishopBT;
	Transform bishopWT;
    BishopMove bishopB;
    BishopMove bishopW;
    bool runAStar = true;
	bool setTarget = true;

    float moveTimer;
	bool setTimer;
    public float moveTimerCap = 2.0f;

    // Use this for initialization
    void Start () {
        bishopB = GameObject.Find("bishopB").GetComponent<BishopMove>();
        bishopW = GameObject.Find("bishopW").GetComponent<BishopMove>();
		bishopBT = GameObject.Find("bishopB").transform;
		bishopWT = GameObject.Find("bishopW").transform;
		//Instantiate (Resources.Load ("BishopCDBubble"), new Vector3 (bishopBT.position.x, bishopBT.position.y + 0.30f, bishopBT.position.z), new Quaternion (0, 0, 0, 1));
		//Instantiate (Resources.Load ("BishopCDBubble"), new Vector3 (bishopWT.position.x, bishopWT.position.y + 0.30f, bishopWT.position.z), new Quaternion (0, 0, 0, 1));
        aStar = new A_Star_Controler();
		moveTimer = moveTimerCap;
		setTimer = true;
		setTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
		//print ("Bishop Move Cooldown = " + moveTimer);
        //creating a list of randomly genorated moves to test movment
        //for (int i = 0; i < 3; i++)
        //{
          //  aStar.whiteMoves.Push(GameObject.FindGameObjectWithTag("Tile_" + (Random.Range(0, 7)) + "" + (Random.Range(0, 7))));
          //  aStar.blackMoves.Push(GameObject.FindGameObjectWithTag("Tile_" + (Random.Range(0, 7)) + "" + (Random.Range(0, 7))));
        //}

		if (moveTimer <= 2 && moveTimer > 1.98) {
			setTimer = false;
			//Instantiate (Resources.Load ("BishopCDBubble"), new Vector3 (bishopBT.position.x, bishopBT.position.y + 0.30f, bishopBT.position.z), new Quaternion (0, 0, 0, 1));
			//Instantiate (Resources.Load ("BishopCDBubble"), new Vector3 (bishopWT.position.x, bishopWT.position.y + 0.30f, bishopWT.position.z), new Quaternion (0, 0, 0, 1));
		}
        //calling A* to create a new path when the privios gole has been reached.
        if (moveTimer > 1.00 && moveTimer < 1.75 && runAStar) {
			Debug.Log ("Move timer runs A-star");
			aStar.runAStar ();
			runAStar = false;
		}
		if (moveTimer < 1.00 && moveTimer > 0.75 && setTarget) {
			//setting the target curser for each bishop
			Instantiate(Resources.Load("TargetWhite"), aStar.whiteMoves.Peek().transform.position, new Quaternion(0, 0, 0, 1));
			Instantiate(Resources.Load("TargetBlack"), aStar.blackMoves.Peek().transform.position, new Quaternion(0, 0, 0, 1));
			setTarget = false;
		}
        if (moveTimer <= 0 )
        {
            if (bishopB.targetLocationX == bishopB.curentX && bishopB.targetLocaitonY == bishopB.curentY) {
                //Debug.Log("move timer up get new target");

                //target = MoveList.getNextMove(bishopColor);
				if (aStar.blackMoves.Count > 0) {
					bishopB.target = aStar.blackMoves.Pop();
					bishopB.targetLocaitonY = bishopB.target.GetComponent<TileInfo>().boardRow;
					bishopB.targetLocationX = bishopB.target.GetComponent<TileInfo>().boardColumn;
					bishopB.spacesToMove = Mathf.Abs(bishopB.targetLocationX - bishopB.curentX);
				}
            }
			if (bishopW.targetLocationX == bishopW.curentX && bishopW.targetLocaitonY == bishopW.curentY) {
				//Debug.Log("move timer up get new target");

				//target = MoveList.getNextMove(bishopColor);
				if (aStar.whiteMoves.Count > 0) {
					bishopW.target = aStar.whiteMoves.Pop();
					bishopW.targetLocaitonY = bishopW.target.GetComponent<TileInfo>().boardRow;
					bishopW.targetLocationX = bishopW.target.GetComponent<TileInfo>().boardColumn;
					bishopW.spacesToMove = Mathf.Abs(bishopW.targetLocationX - bishopW.curentX);
				}
			}
			if (bishopW.canMoveTo == false && bishopB.canMoveTo == false)
			{
				moveTimer = moveTimerCap;
				setTimer = true;
				setTarget = true;
				runAStar = true;
			}
        }

        //cool downs for ecah bishop setting moves
        //Timer countdown
        if (moveTimer > 0)
		{
            moveTimer -= Time.deltaTime;
        }
    }

    //reset moves
    public void resetMoves(char bishopColor)
    {
        if(bishopColor == 'B')
        {
            while (aStar.blackMoves.Count > 0)
            {
                aStar.blackMoves.Pop();
            }
        }
        else
        {
            aStar.whiteMoves.Pop();
        }
    }

    //pops the next move off of the lincked list of future moves
    public GameObject getNextMove(char bishopColor) {
        if(bishopColor == 'B')
        {
            if (aStar.blackMoves.Count > 0)
            {
                return aStar.blackMoves.Pop();
            }
            else
            {
                //returning where the bishop curently is
                return GameObject.FindGameObjectWithTag("Tile_" + (GameObject.Find("bishopB").GetComponent<BishopMove>().curentY) + "" + (GameObject.Find("bishopB").GetComponent<BishopMove>().curentX));
            }
        }
        else
        {
            if (aStar.whiteMoves.Count > 0) {
                return aStar.whiteMoves.Pop();
            }
            else
            {
                //returning where the bishop curently is
                return GameObject.FindGameObjectWithTag("Tile_" + (GameObject.Find("bishopW").GetComponent<BishopMove>().curentY) + "" + (GameObject.Find("bishopW").GetComponent<BishopMove>().curentX));
            }
        }
    }
}
