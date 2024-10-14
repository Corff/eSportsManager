using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;


public class MainMenuUIHandler : MonoBehaviour
{
    private int[] dayAmounts = new int[] {31,28,31,30,31,30,31,31,30,31,30,31 };
    private string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    private string year;
    
    private int dayNum;
    private string saveName;
    private string[][] playerList;    
    private string currentTeam;

    public TMP_Text dateText;
    public TMP_Text teamName;
    public GameObject teamValues;
    
    // Start is called before the first frame update
    void Start()
    {
        saveName = File.ReadAllText("Assets/Saves/currentSave.txt");
        playerList = File.ReadLines("Assets/Saves/"+saveName+"/playersTeamsAndStats.csv").Select(x => x.Split(',')).ToArray();
        loadDate();
        loadTeamName();
        loadPlayers();
    }

    private void loadDate()
    {
        dayNum = int.Parse(File.ReadAllText("Assets/Saves/" + saveName + "/date.txt"));
        dateText.text = "Date: " + getDateString();
    }

    string getDateString()
    {
        int amountOfDaysGone = 0;
        int finalDay = 0;
        string month = "";
        int currentDay = 0;
        string returnString = "";
        
        if (dayNum % 365 == 0)
            currentDay = 365;
        else
        {
            currentDay = dayNum % 365;
        }
        
        for (int i = 0; i < dayAmounts.Count(); i++)
        {
            amountOfDaysGone += dayAmounts[i];
            if (currentDay <= amountOfDaysGone)
            {
                month = months[i];
                for (int j = 0; j < i; j++)
                {
                    currentDay -= dayAmounts[j];
                }

                finalDay = currentDay;
                
                int output = finalDay % 10;
                string suffix = output == 1 ? "st" : output == 2 ? "nd" : output == 3 ? "rd" : "th";
                if(finalDay == 11 || finalDay == 12 || finalDay == 13)
                    suffix = "th";

                year = calculateYear();
                
                returnString =  currentDay + suffix + " of " + month + " " + year;
                break;
            }
        }
        
        return returnString;
    }

    private string calculateYear()
    {
        float floatDay = (float)dayNum;
        double yearAdd = Math.Floor(floatDay / 365);
        if (dayNum % 365 == 0)
            yearAdd -= 1;
        
        return (2023 + yearAdd).ToString();
    }

    private void loadTeamName()
    {
        currentTeam = File.ReadAllText("Assets/Saves/" + saveName + "/teamSelected.txt");
        teamName.text = "Team: " + currentTeam;
    }

    private void loadPlayers()
    {
        List<TMP_Text> playerText = new();
        Transform parent = teamValues.GetComponent<Transform>();
        foreach (Transform child in parent)
        {
            playerText.Add(child.GetComponent<TMP_Text>());
        }
        int i = 0;
        foreach (string[] player in playerList)
        {
            if (player[4] == currentTeam)
            {
                playerText[i + 1].text = player[0];
                i++;
            }
        }

        playerText[0].text = "Team: " + File.ReadAllText("Assets/Saves/" + saveName + "/teamSelected.txt");
    }
    
    public void onAdvance()
    {
        dayNum++;
        dateText.text = "Date: " + getDateString();
    }

    public void Save()
    {
        File.WriteAllText("Assets/Saves/" + saveName + "/date.txt", dayNum.ToString());
        File.WriteAllText("Assets/Saves/"+saveName+"/playersTeamsAndStats.csv", File.ReadAllText("Assets/Saves/"+saveName+"/playersTeamsAndStats.csv"));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
