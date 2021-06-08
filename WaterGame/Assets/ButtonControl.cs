using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public GameObject panel;
    public GameObject optionPanel;
    public GameObject pausePanel;

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
    public void OptionsPanelToggle()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
        if (optionPanel.activeSelf == true)
        {
            pausePanel.SetActive(false);
            pausePanel.GetComponent<Image>().raycastTarget = false;
        }
        else
        {
            pausePanel.SetActive(true);
            pausePanel.GetComponent<Image>().raycastTarget = true;
        }
    }
    public void CloseFromOptions()
    {
        optionPanel.SetActive(false);
    }
    public void ChangeColor()
    {
        
    }
}
