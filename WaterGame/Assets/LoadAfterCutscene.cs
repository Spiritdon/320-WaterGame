using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterCutscene : MonoBehaviour
{
    // string nextLevel;
    public int sceneIndex;


    // Update is called once per frame
    void OnDestroy()
    {
        if(SceneSwitcher.Instance != null)
        {
            SceneSwitcher.Instance.LoadScene(sceneIndex); //Load main level
        }
        //SceneManager.LoadScene(nextLevel);
    }
}
