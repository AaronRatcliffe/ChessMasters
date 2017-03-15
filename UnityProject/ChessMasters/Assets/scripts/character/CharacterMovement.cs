using UnityEngine;
using System.Collections;
using System;

public class CharacterMovement : MonoBehaviour {

	//Current location of character
	public int locationX;
	public int locationY;

	//Where the character wants to move
	int matrixLookX;
	int matrixLookY;

	GameObject target;

	//To be Moved
	public float moveTimer;
	public float moveTimerCap = 0.5f;

    //the delay betwean presing the movment key and aculy moving
    public float moveAnimationDelay = .27f;

    // Use this for initialization
    void Start () {

        //Sets character start location
        locationX = 0;
        locationY = 0;
		moveTimer = moveTimerCap;


	}
	
	// Update is called once per frame
	void Update () {

        //if movement cooldown is up and the character is alive, allow movement
		if (moveTimer <= 0 && GetComponent<Animator>().GetInteger("State") != 2) {
		
			//Checks if left arrow key is pressed
			if (Input.GetKey(KeyCode.LeftArrow)) {
                
                //Checks to see if at edge (cannot move further)
                if (matrixLookX > 0) {
				
					//looks at destination
					matrixLookX = locationX - 1;
					matrixLookY = locationY;

					//set destination as target
					target = GameObject.FindGameObjectWithTag("Tile_" + matrixLookY + "" + matrixLookX);
                    //checks to see if the destination is pathable
					if (target.GetComponent<TileInfo>().pathable && !target.GetComponent<TileInfo>().hostile)
                    {
                        //playing the rogueMove animation
                        GetComponent<Animator>().SetInteger("State", 1);
                        //wating for rougeMove nimaiton to finish
                        StartCoroutine(WhaitForMove());
                    }
				}
			}
			//Checks if right arrow key is pressed
			if (Input.GetKey(KeyCode.RightArrow)) {

                //Checks to see if at edge (cannot move further)
                if (matrixLookX < 7) {

					//looks at destination
					matrixLookX = locationX + 1;
					matrixLookY = locationY;

					//set destination as target
					target = GameObject.FindGameObjectWithTag("Tile_" + matrixLookY + "" + matrixLookX);

					if (target.GetComponent<TileInfo>().pathable && !target.GetComponent<TileInfo>().hostile)
					{
                        //playing the rogueMove animation
                        GetComponent<Animator>().SetInteger("State", 1);
                        //wating for rougeMove nimaiton to finish
                        StartCoroutine(WhaitForMove());
                    }
				}
			}
			//Checks if up arrow key is pressed
			if (Input.GetKey(KeyCode.UpArrow)) {

                //Checks to see if at edge (cannot move further)
                if (matrixLookY > 0) {
					//looks at destination
					matrixLookX = locationX;
					matrixLookY = locationY - 1;

					//set destination as target
					target = GameObject.FindGameObjectWithTag("Tile_" + matrixLookY + "" + matrixLookX);
					if (target.GetComponent<TileInfo>().pathable && !target.GetComponent<TileInfo>().hostile)
					{

                        //playing the rogueMove animation
                        GetComponent<Animator>().SetInteger("State", 1);
                        //wating for rougeMove nimaiton to finish
                        StartCoroutine(WhaitForMove());
                    }
				}
			}
		   //Checks if down arrow key is pressed
			if (Input.GetKey(KeyCode.DownArrow)) {

                //Checks to see if at edge (cannot move further)
                if (matrixLookY < 7) {
					//looks at destination
					matrixLookX = locationX;
					matrixLookY = locationY + 1;

					//set destination as target
					target = GameObject.FindGameObjectWithTag("Tile_" + matrixLookY + "" + matrixLookX);
					if (target.GetComponent<TileInfo>().pathable && !target.GetComponent<TileInfo>().hostile)
					{

                        //playing the rogueMove animation
                        GetComponent<Animator>().SetInteger("State", 1);
                        //wating for rougeMove nimaiton to finish
                        StartCoroutine(WhaitForMove());

                    }
					
                   
                }
			}

        }

		//Timer countdown
		if (moveTimer > 0) {
			moveTimer -= Time.deltaTime;
		}
	}

    private IEnumerator WhaitForMove()
    {
        yield return new WaitForSeconds(moveAnimationDelay);
        //Move to target
        transform.position = target.transform.position;

        //set current to target
        locationX = matrixLookX;
        locationY = matrixLookY;
        //Reset timer
        moveTimer = moveTimerCap;
        //play the rogueIdle animation
        GetComponent<Animator>().SetInteger("State", 0);
    }
}
