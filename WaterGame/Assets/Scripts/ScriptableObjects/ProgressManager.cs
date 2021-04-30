using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Progress Manager")]
public class ProgressManager : ScriptableObject
{
    public List<string> flavors;
    [SerializeField]
    private Dictionary<string, bool> collectedFlavors = new Dictionary<string, bool>();
    public int sugarCollected = 0;

    //Has the tutorial been completed?
    bool tutorialCompleted = false;


    private static ProgressManager instance;

    public static ProgressManager Instance { get => instance; }
    public Dictionary<string, bool> CollectedFlavors { get => collectedFlavors; }
    public bool TutorialCompleted { get => tutorialCompleted;}

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }

        ReInitializeDict();
    }

    public void Init()
    {

        if (instance == null)
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


    public void SetupLoadedLevel()
    {
        //TODO: Set up level (collectables) based on save file data
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


    /// <summary>
    /// Set data loaded from save file
    /// </summary>
    /// <param name="isTutorialCompleted">Has the tutorial been completed(load)</param>
    /// <param name="collectedFlavorsIN">Dictionary containing collected flavors(load)</param>
    /// <param name="sugarCollectedIN">How much sugar has been collected (load)</param>
    public void SetLoadedData(bool isTutorialCompleted, Dictionary<string, bool> collectedFlavorsIN, int sugarCollectedIN)
    {
        tutorialCompleted = isTutorialCompleted;
        collectedFlavors = collectedFlavorsIN;
        sugarCollected = sugarCollectedIN;
    }
}
