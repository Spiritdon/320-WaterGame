using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollector : MonoBehaviour
{
    //Progress manager
    ProgressManager pm;
    public Text sugarCounter;
    // Start is called before the first frame update

    public Button cherry;
    public Button grape;
    public Button lemon;
    public Button orange;
    public Button waterMelon;
    public Button strawBerry;
    public Button Water;

    
    void Start()
    {
        pm = ProgressManager.Instance;
        cherry.gameObject.SetActive(false);
        lemon.gameObject.SetActive(false);
        orange.gameObject.SetActive(false);
        waterMelon.gameObject.SetActive(false);
        strawBerry.gameObject.SetActive(false);
        grape.gameObject.SetActive(false);
        Water.gameObject.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Flavor Collision
        if (other.CompareTag("FlavorCOL"))
        {
            FlavorPickup flav = other.gameObject.GetComponent<FlavorPickup>();

            if (flav != null)
            {
                if(pm ==null)
                {
                    pm = ProgressManager.Instance;
                }


                pm.CollectedFlavor(flav.flavor);
                if (flav.flavor == "Cherry")
                {
                    cherry.gameObject.SetActive(true);
                }
                else if (flav.flavor == "Grape")
                {
                    grape.gameObject.SetActive(true);
                }
                else if (flav.flavor == "Lemon")
                {
                    lemon.gameObject.SetActive(true);
                }
                else if (flav.flavor == "Strawberry")
                {
                    strawBerry.gameObject.SetActive(true);
                }
                else if (flav.flavor == "Watermelon")
                {
                    waterMelon.gameObject.SetActive(true);
                }
                else if (flav.flavor == "Orange")
                {
                    orange.gameObject.SetActive(true);
                }
                Destroy(other.gameObject);
            }
            
        }

        //Sugar collision
        else if (other.CompareTag("SugarCOL"))
        {
            pm.sugarCollected++;
            sugarCounter.text = pm.sugarCollected.ToString();
            Destroy(other.gameObject);
        }
    }
}
