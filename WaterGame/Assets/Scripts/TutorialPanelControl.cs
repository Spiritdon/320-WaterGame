using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialPanelControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public TriggerEvent triggerEvent;
    public SphereCollider player;
    bool panelActive;
    public BoxCollider trigger;

    // Start is called before the first frame update
    void Start()
    {
        panelActive = false;
        triggerEvent.triggerEvent += TriggerPanel;
    }
    void OnDestroy()
    {
        triggerEvent.triggerEvent -= TriggerPanel;
    }

    void TriggerPanel(TriggerState triggerState, Collider other)
    {
        if (triggerState != TriggerState.Exit)
        {
            if (playerCollision())
            {
                panel.SetActive(true);
            }
        }
        else
        {
            panel.SetActive(false);
        }
    }
    bool playerCollision()
    {
        return player.bounds.Intersects(trigger.bounds);
    }

    void activatePanel()
    {
        panel.SetActive(true);
    }
}
