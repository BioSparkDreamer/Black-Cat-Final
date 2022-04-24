using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPlayerController : MonoBehaviour
{
    [Header("Map Point Variables")]
    public HubMapPoint currentPoint;
    public float moveSpeed = 10f;
    private bool canMove = true;
    public HubWorldManager theManager;

    void Start()
    {

    }

    void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            //Move the transform position of the player towards the next currentPoint
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

            //If the player has reaches the current point then allow him to move again
            if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.05f && canMove)
            {
                //Input for moving the player up
                if (Input.GetAxisRaw("Vertical") > .5f)
                {
                    if (currentPoint.up != null)
                    {
                        SetNextPoint(currentPoint.up);
                    }
                }

                //Input for moving the player down
                if (Input.GetAxisRaw("Vertical") < -.5f)
                {
                    if (currentPoint.down != null)
                    {
                        SetNextPoint(currentPoint.down);
                    }
                }

                //Input for moving the player right
                if (Input.GetAxisRaw("Horizontal") > .5f)
                {
                    if (currentPoint.right != null)
                    {
                        SetNextPoint(currentPoint.right);
                    }
                }

                //Input for moving the player left
                if (Input.GetAxisRaw("Horizontal") < -.5f)
                {
                    if (currentPoint.left != null)
                    {
                        SetNextPoint(currentPoint.left);
                    }
                }

                //If the players current point is a level and the jump button is pressed then do this code
                if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
                {
                    //Call the hub world ui controller to show information panel using the current point as a parameter
                    if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.05f)
                    {
                        HubWorldUIController.instance.ShowInformation(currentPoint);

                        if (Input.GetButtonDown("Jump") && !PauseMenu.instance.isPaused)
                        {
                            canMove = false;
                            theManager.LoadLevel();
                        }
                    }
                }
            }
            else
            {
                HubWorldUIController.instance.HideInformation();
            }
        }
    }

    public void SetNextPoint(HubMapPoint nextPoint)
    {
        //Set the current point equal to the next point
        currentPoint = nextPoint;
    }
}
