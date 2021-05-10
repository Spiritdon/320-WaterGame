using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorUIController : MonoBehaviour
{

    public GameObject[] flavorButtons;
    public string[] flavors;

    public Dictionary<string, GameObject> buttonDict;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < flavorButtons.Length; i++)
        {
            buttonDict.Add(flavors[i], flavorButtons[i]);
            flavorButtons[i].SetActive(false);
        }

       
    }

    private void OnEnable()
    {
        ProgressManager.Instance.flavorPickup += UpdateFlavorUI;
    }

    private void OnDisable()
    {
        ProgressManager.Instance.flavorPickup -= UpdateFlavorUI;
    }


    void UpdateFlavorUI(string flavor)
    { 
        if(buttonDict.ContainsKey(flavor))
        {
            buttonDict[flavor].SetActive(true);
        }
    }
}
