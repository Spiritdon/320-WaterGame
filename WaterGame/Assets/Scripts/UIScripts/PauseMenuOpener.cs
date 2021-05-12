using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuOpener : MonoBehaviour
{

    //Pause Menu
    public GameObject panel;
    bool isPanelActive;


    [SerializeField] GameObject tutorialPanels;

    // Update is called once per frame
    void Update()
    {
        //Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }


    public void TogglePause()
    {
        isPanelActive = panel.activeSelf;
        isPanelActive = !isPanelActive;

        if (tutorialPanels != null)
        {
            tutorialPanels.SetActive(!isPanelActive);
        }

        panel.SetActive(isPanelActive);
        if (isPanelActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1.0f;
        }
    }
}
