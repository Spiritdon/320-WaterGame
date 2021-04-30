using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(menuName = "Scriptable Objects/Save-Load Manager")]
public class SaveLoadManager : ScriptableObject
{
    public ProgressManager progressManager;
    private static SaveLoadManager instance;

    public static SaveLoadManager Instance { get => instance;}

    public void Init()
    {
        if(instance == null)
        {
            instance = this;
        }
        progressManager = ProgressManager.Instance;
    }

    public void SaveGame()
    {
        //Null check
        if(progressManager == null)
        {
            progressManager = ProgressManager.Instance;
        }
       
        //Open file
        if (!Directory.Exists(Application.persistentDataPath + "/save"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/save");
        }

        BinaryFormatter bFormatter = new BinaryFormatter();

        //Create file to save to
        FileStream saveFile = new FileStream(Application.persistentDataPath + "/save/gameSave.dat", FileMode.OpenOrCreate);

        //bFormatter.Serialize(saveFile, <itemToSave>); - How to save serialized fields
        //Save Data
        bFormatter.Serialize(saveFile, progressManager.sugarCollected); //Collected Sugar
        bFormatter.Serialize(saveFile, progressManager.CollectedFlavors); //Flavors Dictionary
        bFormatter.Serialize(saveFile, progressManager.TutorialCompleted);

        /*TODO: To add to save:
          - player position
          - sugar ids collected
          - player's current chosen flavor (material/enum?)
        */

        //Close file
        saveFile.Close();
    }

    public void LoadGame()
    {
        progressManager = ProgressManager.Instance;

        //Open File if it exists
        if (File.Exists(Application.persistentDataPath + "/save/gameSave.dat"))
        {
            //Binary formatter -- allows us to write and read data to/from a file
            BinaryFormatter bFormatter = new BinaryFormatter();

            //Open file to save to
            FileStream saveFile = File.OpenRead(Application.persistentDataPath + "/save/gameSave.dat");


            //Load data and set manager

            //Load Data
            int sugarCollected = (int)bFormatter.Deserialize(saveFile); //Collected Sugar
            Dictionary<string,bool> loadedFlavors = (Dictionary<string,bool>)bFormatter.Deserialize(saveFile); //Flavors Dictionary
            bool tutorialCompleteLoad = (bool)bFormatter.Deserialize(saveFile);


            progressManager.SetLoadedData(tutorialCompleteLoad, loadedFlavors, sugarCollected);

            //Close file
            saveFile.Close();
        }

    }

    public void ClearSave()
    {
        if (File.Exists(Application.persistentDataPath + "/save/gameSave.dat"))
        {
            //Remove existing file if needed
            File.Delete(Application.persistentDataPath + "/save/gameSave.dat");
        }
    }
}
