using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    //Progress manager
    ProgressManager pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = ProgressManager.Instance;
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
                print(pm);
                pm.CollectedFlavor(flav.flavor);
                Destroy(other.gameObject);
            }
        }

        //Sugar collision
        else if (other.CompareTag("SugarCOL"))
        {
            pm.sugarCollected++;
            Destroy(other.gameObject);
        }
    }
}
