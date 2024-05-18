using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateHandler : MonoBehaviour
{

    public int dayNum;
    private string saveName;
    public TMP_Text dateText;
    
    // Start is called before the first frame update
    void Start()
    {
        saveName = File.ReadAllText("Assets/Saves/currentSave.txt");
        dayNum = int.Parse(File.ReadAllText("Assets/Saves/" + saveName + "/date.txt"));
        dateText.text = "Date: " + dayNum.ToString();
    }

    public void onAdvance()
    {
        dayNum++;
        dateText.text = "Date: " + dayNum.ToString();
    }
}
