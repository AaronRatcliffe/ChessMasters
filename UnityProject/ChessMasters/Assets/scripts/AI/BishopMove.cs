using UnityEngine;
using System.Collections;

public class BishopMove : MonoBehaviour
{

    //where you are
    public int curentX = 0;
    public int curentY = 0;

    public char bishopColor;
    public bool canMoveTo;

    //stors wether the target is in the posotive or negotive x and y orientation from the bishop
    int targetDirectionY;
    int targetDirectionX;

    public int targetLocationX;
    public int targetLocaitonY;


    public GameObject target;


    //Vector2 stearing;
    public float timeToTarget = 0.2f;
    public float maxSpeed = 4;
    public float spacesToMove;
    //Rigidbody2D rb2D;

    //public float satRadius = 1;

    //bool moving;


    // Use this for initialization
    void Start()
    {
        if (name == "bishopW")
        {
            bishopColor = 'W';
        }
        else
        {
            bishopColor = 'B';
        }
        //rb2D = GetComponent<Rigidbody2D>();
        //moving = false;
        canMoveTo = false;
        target = GameObject.FindGameObjectWithTag("Tile_" + (curentY) + "" + (curentX));
        targetLocaitonY = target.GetComponent<TileInfo>().boardRow;
		targetLocationX = target.GetComponent<TileInfo> ().boardColumn;
    }

    // Update is called once per frame
    void Update()
    {
        //geting the target When movment timer is off cool down and the bishop is not moving

        checkTargetDirection();

        faceTargetDirection();

        //cheking to see if the target is in a strate diagonal line from the bishop
        if (Mathf.Abs(targetLocationX - curentX) == Mathf.Abs(targetLocaitonY - curentY))
        {
            //itorating thru all tiles going from the bishop to the target to see if it has a clear path.
            for (int i = 0; i < spacesToMove; i++)
            {
                //cheaking to see if the curent tile you are looking at is blocked
                if (!GameObject.FindGameObjectWithTag("Tile_" + (curentY + (i * targetDirectionY)) + "" + (curentX + (i * targetDirectionX))).GetComponent<TileInfo>().pathable)
                {
                    Debug.Log("found an unpathable tile inbetween bishop and target");
                    //since you are looking at an unpathable tile. Set the new target to the tile you visited befor this
                    target = GameObject.FindGameObjectWithTag("Tile_" + (curentY + ((i - 1) * targetDirectionY)) + "" + (curentX + ((i - 1) * targetDirectionX)));
                    targetLocaitonY = target.GetComponent<TileInfo>().boardRow;
                    targetLocationX = target.GetComponent<TileInfo>().boardColumn;
                    spacesToMove = Mathf.Abs(targetLocationX - curentX);
                }
            }
            canMoveTo = true;
        }
        else
        {
            //Debug.Log("target is not on a diagonal line from the bishop");
            canMoveTo = false;
        }

        //cheking to see if the target is pathable to
        if (canMoveTo)
        {
            if (curentX != targetLocationX && curentY != targetLocaitonY && spacesToMove >= 0)
            {

                //moving twords the target
                float newX = transform.position.x + Time.deltaTime * 0.319f * maxSpeed * targetDirectionX;
                float newY = transform.position.y + Time.deltaTime * 0.319f * maxSpeed * targetDirectionY * -1;
                transform.position = new Vector2(newX, newY);
                spacesToMove -= Time.deltaTime * maxSpeed;
            }
            else
            {
				transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z - 0.5f);
                curentX = targetLocationX;
                curentY = targetLocaitonY;
                canMoveTo = false;
            }

        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Tile_" + (curentY) + "" + (curentX));
            targetLocaitonY = target.GetComponent<TileInfo>().boardRow;
            targetLocationX = target.GetComponent<TileInfo>().boardColumn;
            spacesToMove = Mathf.Abs(targetLocationX - curentX);
        }
    }

    //makes the bishop face the target direction
    void faceTargetDirection() {
        if (targetDirectionX == 1 && targetDirectionY == 1) {
            //playing the bishop look down right
            GetComponent<Animator>().SetInteger("Direction", 1);
        }
        else if (targetDirectionX == -1 && targetDirectionY == 1)
        {
            //playing the bishop look down left
            GetComponent<Animator>().SetInteger("Direction", 0);
        }
        else if(targetDirectionX == 1 && targetDirectionY == -1) {
            //playing the bishop look up right
            GetComponent<Animator>().SetInteger("Direction", 2);
        }
        else if (targetDirectionX == -1 && targetDirectionY == -1)
        {
            //playing the bishop look up lefft
            GetComponent<Animator>().SetInteger("Direction", 3);
        }
    }
    void checkTargetDirection()
    {
        //Debug.Log("Checking targets direction");
        //cheking to see where the target is in relation to the bishops curent location
        if (curentX - targetLocationX < 0)
        {
            targetDirectionX = 1;
        }
        else
        {
            targetDirectionX = -1;
        }

        if (curentY - targetLocaitonY < 0)
        {
            targetDirectionY = 1;
        }
        else
        {
            targetDirectionY = -1;
        }
    }
}
