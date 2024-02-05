using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataSystem : MonoBehaviour
{
    public string savePath = "DataUser.dat";
    public static DataUser DataUser;

    public static SaveDataSystem instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //  https://stackoverflow.com/questions/2507808/how-to-check-whether-a-file-is-empty-or-not
        //   To fix if the file is blank

        DataUser = IOData.Load(savePath) as DataUser;
        if (DataUser == null)
        {
            InitializeData();
        }
    }

    [ContextMenu("Reset Data")]
    public void ResetData()
    {
        InitializeData();
    }

    public void Save()
    {
        IOData.Save(savePath, DataUser);
    }

    /// <summary>
    /// Maybe in the future we need to separate them
    /// </summary>
    public void InitializeData()
    {
        DataUser = new()
        {
            test = 0,
        };

        IOData.Save(savePath, DataUser);
    }

}

[System.Serializable]
public class DataUser
{
    public int test;
}
