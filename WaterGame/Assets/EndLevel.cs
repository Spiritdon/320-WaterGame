using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{

    public GameObject cutscene;
    public int playtime;
    public CameraController camCont;
    // Start is called before the first frame update
    void Start()
    {
        cutscene.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            camCont.enabled = false;
            player.gameObject.SetActive(false);

            cutscene.SetActive(true);
            Destroy(cutscene, playtime);
        }
    }
}
