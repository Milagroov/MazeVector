using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool canMove = true;
    public Rigidbody rb;



    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;

    private Vector2 playerDirection = Vector2.zero;

    [SerializeField] LayerMask layerWall;




    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        rb = gameObject.GetComponent<Rigidbody>();

        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(canMove);

        if (canMove == true)
        {

            if (Input.touchCount == 1) // user is touching the screen with a single touch
            {
                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe
                                rb.velocity = new Vector2(1 * speed , 0);
                                playerDirection = new Vector2(1, 0);
                                canMove = false;
                                Debug.Log("right");
                            }
                            else
                            {   //Left swipe
                                rb.velocity = new Vector2(-1 * speed, 0);
                                playerDirection = new Vector2(-1, 0);
                                canMove = false;
                                Debug.Log("left");
                            }
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement
                            if (lp.y > fp.y)  //If the movement was up
                            {   //Up swipe
                                rb.velocity = new Vector2(0, 1 * speed);
                                playerDirection = new Vector2(0, 1);
                                canMove = false;
                                Debug.Log("up");
                            }
                            else
                            {   //Down swipe
                                rb.velocity = new Vector2(0, -1 * speed);
                                playerDirection = new Vector2(0, -1);
                                canMove = false;
                                Debug.Log("down");
                            }
                        }
                    }
                }
            }
        }



                /*if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(0, 1 * speed * Time.deltaTime);
                canMove = false;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(0, -1 * speed * Time.deltaTime);
                canMove = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-1 * speed * Time.deltaTime, 0);
                canMove = false;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(1 * speed * Time.deltaTime, 0);
                canMove = false;
            }

        }

        Debug.Log(canMove);*/


    }

    private void OnCollisionEnter(Collision collision)
    {
        DetectCollisionWithWalls(collision);

    }

    private void DetectCollisionWithWalls(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.DrawRay(transform.position, playerDirection, Color.red, 3);

            Debug.Log("playerDirection: " + playerDirection);

            if (Physics.Raycast(transform.position, playerDirection, 1, layerWall))
            {
                Debug.Log("Hit wall");
                canMove = true;
            }

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        DetectCollisionWithWalls(collision);
    }

    /*private void OnCollisionExit(Collision collision)
    {
        canMove = false;
    }*/


}
