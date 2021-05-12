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
    public void PlayGame()
    {
        SceneSwitcher.Instance.LoadScene(2);
        Time.timeScale = 1.0f;
    }
    public void BackToTitle()
    {
        SceneSwitcher.Instance.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeColor()
    {
        
    }
}
