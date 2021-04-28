using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOInitializer : MonoBehaviour
{

    public ProgressManager pm;
    // Start is called before the first frame update
    void Awake()
    {
        pm.Init();
    }
}
