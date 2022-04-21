using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCameraController : MonoBehaviour
{
    public Vector2 minPosition, maxPosition;
    public Transform theTarget;

    void LateUpdate()
    {
        //Create variables to clamp on the x and y axis using the Vector2 variables we created
        float xPos = Mathf.Clamp(theTarget.position.x, minPosition.x, maxPosition.x);
        float yPos = Mathf.Clamp(theTarget.position.y, minPosition.y, maxPosition.y);

        //Move the transform position clamped to the variables we created ignoring the z-axis
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
