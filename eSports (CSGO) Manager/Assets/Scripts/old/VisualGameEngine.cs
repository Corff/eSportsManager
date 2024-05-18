using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class VisualGameEngine : MonoBehaviour
{
    
    public TMP_Dropdown team1;
    public TMP_Dropdown team2;

    public string[] teamList;
    private string[][] playerList;

    public TextMeshProUGUI[] team1DisplayVars = new TextMeshProUGUI[6];
    public TextMeshProUGUI[] team2DisplayVars = new TextMeshProUGUI[6];

    public TextMeshProUGUI roundText;
    
    List<string> t1List;
    List<string> t2List;

    public int currentRound = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Lists/playersAndStats.csv";
        playerList = File.ReadLines(path).Select(x => x.Split(',')).ToArray();
    }

    public void processMatch()
    {
        t1List = getTeamMembers(team1.captionText.text);
        t2List = getTeamMembers(team2.captionText.text);
            
        displayTeams();
    }

    public void processRound()
    {
        t1List = getTeamMembers(team1.captionText.text);
        t2List = getTeamMembers(team2.captionText.text);
            
        displayTeams();
        
        currentRound++;
        roundText.text = currentRound.ToString();
        
        List<int> t1Health = new List<int>();
        List<int> t2Health = new List<int>();
        
        for (int i = 0; i < 5; i++)
        {
            t1Health.Add(100);
            t2Health.Add(100);
        }

        bool running = true;
        while (running)
        {
            int player1 = Random.Range(0, 5);
            int player2 = Random.Range(0, 5);
            if (t1Health[player1] != 0 && t2Health[player2] != 0)
            {
                string[] p1Stats = getPlayerStats(t1List[player1]);
                string[] p2Stats = getPlayerStats(t2List[player2]);
                
                float p1RatingTickets = truncate(((float.Parse(p1Stats[2])-0.77f) / (1.32f-0.77f))*100,2)*1000;
                float p2RatingTickets = truncate(((float.Parse(p2Stats[2])-0.77f) / (1.32f-0.77f))*100,2)*1000;
                
                int totalTickets = (int)p1RatingTickets + (int)p2RatingTickets;

                int winner = Random.Range(0, totalTickets);
                
                if (winner<=p1RatingTickets)
                {
                    //P1 won
                    t2Health[player2] = 0;
                    Debug.Log(t1List[player1] + " killed " + t2List[player2]);
                    team2DisplayVars[player2 + 1].text = "";

                }
                else
                {
                    //P2 won
                    t1Health[player1] = 0;
                    Debug.Log(t2List[player2] + " killed " + t1List[player1]);
                    team1DisplayVars[player1 + 1].text = "";
                }
            }
            if (t1Health.Sum() == 0)
            {
                running = false;
                Debug.Log(team2.captionText.text + " wins the round!");
            }
            else if (t2Health.Sum() == 0)
            {
                running = false;
                Debug.Log(team1.captionText.text + " wins the round!");
            }
        }
    }

    float truncate(float value, int digits)
    {
        double mult = Math.Pow(10.0, digits);
        double result = Math.Truncate(mult * value) / mult;
        return (float)result;
    }
    
    string[] getPlayerStats(string playerName)
    {
        foreach (var player in playerList)
        {
            if (player[0] == playerName)
            {
                return player;
            }
        }

        return null;
    }
    
    List<string> getTeamMembers(string teamName)
    {
        List<string> temp = new List<string>();
        for (int i = 0; i < playerList.Length; i++)
        {
            if(playerList[i][1] == teamName)
                temp.Add(playerList[i][0]);
        }

        return temp;
    }
    
    void displayTeams()
    {
        team1DisplayVars[0].text = team1.captionText.text;
        team1DisplayVars[1].text = t1List[0];
        team1DisplayVars[2].text = t1List[1];
        team1DisplayVars[3].text = t1List[2];
        team1DisplayVars[4].text = t1List[3];
        team1DisplayVars[5].text = t1List[4];
        
        team2DisplayVars[0].text = team2.captionText.text;
        team2DisplayVars[1].text = t2List[0];
        team2DisplayVars[2].text = t2List[1];
        team2DisplayVars[3].text = t2List[2];
        team2DisplayVars[4].text = t2List[3];
        team2DisplayVars[5].text = t2List[4];
    }
    
}
