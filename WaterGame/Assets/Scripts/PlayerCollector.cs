using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollector : MonoBehaviour
{
    
    public Text sugarCounter;
    // Start is called before the first frame update

    public Button cherry;
    public Button grape;
    public Button lemon;
    public Button orange;
    public Button waterMelon;
    public Button strawBerry;
    public Button Water;

    public Image cherryLock;
    public Image grapeLock;
    public Image lemonLock;
    public Image orangeLock;
    public Image melonLock;
    public Image strawLock;


    public AudioSource pickupAud;
    void Start()
    {
        cherryLock.gameObject.SetActive(true);
        grapeLock.gameObject.SetActive(true);
        lemonLock.gameObject.SetActive(true);
        orangeLock.gameObject.SetActive(true);
        melonLock.gameObject.SetActive(true);
        strawLock.gameObject.SetActive(true);

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


                ProgressManager.Instance.CollectedFlavor(flav.flavor);
                if (flav.flavor == "Cherry")
                {
                    cherry.gameObject.SetActive(true);
                    cherryLock.gameObject.SetActive(false);
                    
                }
                else if (flav.flavor == "Grape")
                {
                    grape.gameObject.SetActive(true);
                    grapeLock.gameObject.SetActive(false);
                    
                }
                else if (flav.flavor == "Lemon")
                {
                    lemon.gameObject.SetActive(true);
                    lemonLock.gameObject.SetActive(false);
                    
                }
                else if (flav.flavor == "Strawberry")
                {
                    strawBerry.gameObject.SetActive(true);
                    strawLock.gameObject.SetActive(false);
                }
                else if (flav.flavor == "Watermelon")
                {
                    waterMelon.gameObject.SetActive(true);
                    melonLock.gameObject.SetActive(false);
                }
                else if (flav.flavor == "Orange")
                {
                    orange.gameObject.SetActive(true);
                    orangeLock.gameObject.SetActive(false);
                    
                }
                Destroy(other.gameObject);

                pickupAud.pitch = Random.Range(0.8f, 1.2f);
                pickupAud.Play();
            }
            
        }

        //Sugar collision
        else if (other.CompareTag("SugarCOL"))
        {
            pickupAud.pitch = Random.Range(0.8f, 1.2f);
            pickupAud.Play();
            ProgressManager.Instance.sugarCollected++;
            sugarCounter.text = ProgressManager.Instance.sugarCollected.ToString();
            Destroy(other.gameObject);
        }
    }
}
