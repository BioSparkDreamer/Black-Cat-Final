using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    [Header("Object Variables")]
    private PlayerController thePlayer;

    [Header("Clamping Variables")]
    public BoxCollider2D boundsBox;
    private float halfHeight, halfWidth;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    void Update()
    {
        if (thePlayer != null)
        {
            //Move the cameras position towards the player
            transform.position = new Vector3(
                Mathf.Clamp(thePlayer.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth),
                Mathf.Clamp(thePlayer.transform.position.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight),
                transform.position.z);
        }
    }
}
