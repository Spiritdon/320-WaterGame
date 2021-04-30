using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOInitializer : MonoBehaviour
{

    public ProgressManager pm;
    public SaveLoadManager slm;
    // Start is called before the first frame update
    void Awake()
    {
        pm.Init();
        slm.Init();
    }
}
