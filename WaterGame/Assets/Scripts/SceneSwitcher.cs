using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    private static SceneSwitcher instance;

    public static SceneSwitcher Instance { get => instance; }

    private int currentlyLoadedScene;

    public GameObject loadingScreenUI;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        //Load main menu on start
        LoadScene(1);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(SceneLoading(sceneIndex));
    }

    /// <summary>
    /// Loads the next scene
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator SceneLoading(int index)
    {
        loadingScreenUI.SetActive(true);
        //Unload any currently active scene
        if (currentlyLoadedScene != 0)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentlyLoadedScene);
            while (!asyncUnload.isDone)
            {
                yield return null;
            }

            currentlyLoadedScene = 0;
        }
        //Make loading scene the active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
       

        //Get scene to load and start loading asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        //Wait until its done
        while(!asyncLoad.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        loadingScreenUI.SetActive(false);   //Hide loading screen
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));   //Make this the active screen
        currentlyLoadedScene = index;   //Track the loaded scene
    }
}
