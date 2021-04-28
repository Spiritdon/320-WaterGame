using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Progress Manager")]
public class ProgressManager : ScriptableObject
{
    public List<string> flavors;
    private Dictionary<string, bool> collectedFlavors = new Dictionary<string, bool>();
    public int sugarCollected = 0;

    private static ProgressManager instance;

    public static ProgressManager Instance { get => instance; }

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }

        ReInitializeDict();
    }

    public void CollectedFlavor(string flavor)
    {
        if(collectedFlavors.ContainsKey(flavor))
        {
            collectedFlavors[flavor] = true;
            Debug.Log("Collected " + flavor);
        }
    }


    /// <summary>
    /// Has the player collected this flavor yet?
    /// </summary>
    /// <param name="flavor">String name of the flavor</param>
    /// <returns>TRUE if player has collected the flavor</returns>
    public bool HasFlavor(string flavor)
    {
        if (collectedFlavors.ContainsKey(flavor))
        {
            if(collectedFlavors[flavor])
            {
                return true;
            }
        }

        return false;
    }


    private void ReInitializeDict()
    {
        collectedFlavors.Clear();
        for (int i = 0; i < flavors.Count; i++)
        {
            collectedFlavors.Add(flavors[i], false);
        }
    }
}
