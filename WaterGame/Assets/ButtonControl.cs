using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public GameObject panel;

    public void OpenPanel()
    {
        panel.SetActive(true);
    }
    public void ClosePanel()
    {
        panel.SetActive(false);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
