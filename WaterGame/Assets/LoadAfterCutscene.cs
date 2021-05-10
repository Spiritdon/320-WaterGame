using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterCutscene : MonoBehaviour
{
    public string nextLevel;


    // Update is called once per frame
    void OnDestroy()
    {
        SceneSwitcher.Instance.LoadScene(3); //Load main level
        //SceneManager.LoadScene(nextLevel);
    }
}
