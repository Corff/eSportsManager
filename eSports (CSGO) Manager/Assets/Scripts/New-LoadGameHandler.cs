using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;
using System.IO;
using Directory = System.IO.Directory;
using File = System.IO.File;


public class saveGameHandler : MonoBehaviour
{

    public TMP_Dropdown newTeamName;
    public TMP_InputField newSaveName;
    
    public TMP_Dropdown loadSaveName;
    
    public void createNewSave()
    {
        string dir = "Assets/Saves/"+newSaveName.text;
        if(!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        File.WriteAllText(dir+"/teamSelected.txt", newTeamName.captionText.text);
        File.WriteAllText(dir+"/playersTeamsAndStats.csv", File.ReadAllText("Assets/Lists/playersTeamsAndStats.csv"));
        File.WriteAllText(dir+"/date.txt", "1");
        File.WriteAllText("Assets/Saves/currentSave.txt", newSaveName.text);
    }

    public void loadSave()
    {
        File.WriteAllText("Assets/Saves/currentSave.txt", loadSaveName.captionText.text);
    }
}
