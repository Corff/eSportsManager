using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.UI;

public class addTeamsToDropdown : MonoBehaviour
{

    // public TMP_Dropdown dropdown;
    public string[][] teamList;
    List<string> actualTeamList;
    public int[] count;

    void Start()
    {
        string path = "Assets/Lists/playersTeamsAndStats.csv";
        teamList = File.ReadLines(path).Select(x => x.Split(',')).ToArray();
        actualTeamList = new List<string>();
        foreach (string[] line in teamList)
        {
            if (actualTeamList.Contains(line[4]) == false && line[4].Contains("(benched)") == false && line[4] != "No team")
            {
                actualTeamList.Add(line[4]);
            }
        }
        
        count = new int[actualTeamList.Count];
        //Remove teams if they dont have 5 players
        for (int i = 0; i < actualTeamList.Count; i++)
        {
            for (int j = 0; j < teamList.Length; j++)
            {
                if (actualTeamList[i] == teamList[j][4])
                {
                    count[i]++;
                }
            }
        }
        
        

        Debug.Log(actualTeamList.Count);

        for (int i = 0; i < actualTeamList.Count; i++)
        {
            if (count[i] == 5)
            {
                string pathImage = "Team Logos/" + actualTeamList[i];
                Debug.Log(pathImage);
                Sprite image = Resources.Load<Sprite>(pathImage);
                //this.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(actualTeamList[i], image));
            }
        }
        
        //foreach (string team in actualTeamList)
        //{
        //    this.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(team));
        //}

    }
}
