using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubWorldManager : MonoBehaviour
{
    public HubPlayerController thePlayer;
    private HubMapPoint[] allPoints;

    void Start()
    {
        allPoints = FindObjectsOfType<HubMapPoint>();

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach (HubMapPoint point in allPoints)
            {
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint = point;
                }
            }
        }
    }

    void Update()
    {

    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    public IEnumerator LoadLevelCo()
    {
        HubWorldUIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / HubWorldUIController.instance.fadeSpeed) + .25f);

        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
