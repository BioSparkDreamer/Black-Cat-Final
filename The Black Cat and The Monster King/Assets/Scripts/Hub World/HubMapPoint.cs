using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubMapPoint : MonoBehaviour
{
    public HubMapPoint up, right, down, left;
    public string levelToLoad, levelToCheck, levelName;
    public bool isLevel, isLocked;
    public SpriteRenderer theSR;
    public Sprite hubSprite, lockedSprite;

    void Awake()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //If the map point is a level and has a level to load
        if (isLevel && levelToLoad != null)
        {
            //Make all the maps locked by default
            isLocked = true;

            //If the level to check is not equal to null then do this code
            if (levelToCheck != null)
            {
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                }
            }
        }

        //If the level to load is the same as level to check then make the level unlockable
        if (levelToLoad == levelToCheck)
        {
            isLocked = false;
        }

        if (isLocked && isLevel)
        {
            theSR.sprite = lockedSprite;
        }
        else if (!isLocked && isLevel)
        {
            theSR.sprite = hubSprite;
        }
    }

    void Update()
    {

    }
}
