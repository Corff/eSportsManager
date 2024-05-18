using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class matchEngine : MonoBehaviour
{

    public TMP_Dropdown team1;
    public TMP_Dropdown team2;

    public string[] teamList;
    private string[][] playerList;
    
    public TextMeshProUGUI team1Name;
    public TextMeshProUGUI T1P1;
    public TextMeshProUGUI T1P2;
    public TextMeshProUGUI T1P3;
    public TextMeshProUGUI T1P4;
    public TextMeshProUGUI T1P5;
    
    public TextMeshProUGUI team2Name;
    public TextMeshProUGUI T2P1;
    public TextMeshProUGUI T2P2;
    public TextMeshProUGUI T2P3;
    public TextMeshProUGUI T2P4;
    public TextMeshProUGUI T2P5;
    
    List<string> t1List;
    List<string> t2List;

    void Start()
    {
        string path = "Assets/Lists/playersTeamsAndStats.csv";
        playerList = File.ReadLines(path).Select(x => x.Split(',')).ToArray();
    }
    
    public void processMatch(int numOfMaps)
        {
            int winningMargin = numOfMaps == 1 ? 1 : numOfMaps == 3 ? 2 : 3;
            Vector2 seriesScore = new Vector2(0, 0);
            Vector2[] scores = new Vector2[numOfMaps];  
            t1List = getTeamMembers(team1.captionText.text);
            t2List = getTeamMembers(team2.captionText.text);
            
            displayTeams();

            for (int i = 0; i < numOfMaps; i++)
            {
                Vector2 mapScore = processMap(new Vector2(0,0),30);
                if (mapScore.x > mapScore.y)
                    seriesScore.x++;
                else
                    seriesScore.y++;
    
                scores[i] = mapScore;
                
                if (seriesScore.x == winningMargin || seriesScore.y == winningMargin)
                    break;
            }
            
            displayScores(seriesScore, scores);
        }

    Vector2 processMap(Vector2 startScore, int roundsPlayed)
    {
        int winningMargin = roundsPlayed == 30 ? 16 : 4;
        Vector2 currentScore = new Vector2(0, 0);
        for (int i = 0; i < roundsPlayed; i++)
        {
            currentScore = processRound(currentScore);

            if (currentScore.x == winningMargin || currentScore.y == winningMargin)
                return startScore + currentScore;
        }
        //Recursion to deal with overtime. Basically handles every map as an overtime period, but the first period starts at 0:0 and plays 30 rounds, each next one is MR6 and uses the score after previous phase of play.
        return processMap(startScore + currentScore,6);
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

    float truncate(float value, int digits)
    {
        double mult = Math.Pow(10.0, digits);
        double result = Math.Truncate(mult * value) / mult;
        return (float)result;
    }
    
    Vector2 processRound(Vector2 score)
    {

        //Initialize 2 lists of both teams with their health.
        List<int> t1Health = new List<int>();
        List<int> t2Health = new List<int>();
        for (int i = 0; i < t1List.Count; i++)
        {
            t1Health.Add(100);
            t2Health.Add(100);
        }

        bool running = true;
        while (running)
        {
            
            //choose 2 random players
            int player1 = Random.Range(0, 5);
            int player2 = Random.Range(0, 5);
            

            //Make sure none of the players are dead
            if (t1Health[player1] != 0 && t2Health[player2] != 0)
            {
                //Calculate winner of the fight
                string[] p1Stats = getPlayerStats(t1List[player1]);
                string[] p2Stats = getPlayerStats(t2List[player2]);


                string winnerText = "";

                //Normalize the rating 2.0 (min .77, max 1.32)
                float p1RatingTickets = truncate(((float.Parse(p1Stats[3])-0.77f) / (1.32f-0.77f))*100,2)*1000;
                float p2RatingTickets = truncate(((float.Parse(p2Stats[3])-0.77f) / (1.32f-0.77f))*100,2)*1000;

                int totalTickets = (int)p1RatingTickets + (int)p2RatingTickets;

                int winner = Random.Range(0, totalTickets);
                
                if (winner<=p1RatingTickets)
                {
                    t2Health[player2] = 0;
                    winnerText = "1";
                }
                else
                {
                    t1Health[player1] = 0;
                    winnerText = "2";
                }
                
                string player1message = t1List[player1] + " " + p1RatingTickets;
                string player2message = t2List[player2] + " " + p2RatingTickets;
                Debug.Log(player1message + "\n" + player2message + "\n" + winnerText + "\n" + totalTickets);
            }
            
            if (t1Health.Sum() == 0 || t2Health.Sum() == 0)
            {
                running = false;
            }
        }

        if (t1Health.Sum() == 0)
            score.y++;
        else if (t2Health.Sum() == 0)
            score.x++;
        


        return score;

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
        team1Name.text = team1.captionText.text;
        T1P1.text = t1List[0];
        T1P2.text = t1List[1];
        T1P3.text = t1List[2];
        T1P4.text = t1List[3];
        T1P5.text = t1List[4];
        
        team2Name.text = team2.captionText.text;
        T2P1.text = t2List[0];
        T2P2.text = t2List[1];
        T2P3.text = t2List[2];
        T2P4.text = t2List[3];
        T2P5.text = t2List[4];
    }
    
    void displayScores(Vector2 seriesScore, Vector2[] mapScores)
    {
        TMP_Text seriesScoreText = GameObject.FindGameObjectWithTag("SeriesScore").GetComponent<TMP_Text>();
        TMP_Text winnerText = GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TMP_Text>();
        
        string winnerT = "Winner: " + (seriesScore.x > seriesScore.y ? team1.captionText.text : team2.captionText.text);
        for (int i = 0; i < mapScores.Length; i++)
        {
            Debug.Log(mapScores[i][0]);
            Debug.Log(mapScores[i][1]);
            winnerT = winnerT + " " + mapScores[i];
        }
        
        seriesScoreText.text = "Series Score: " + seriesScore.x + ":" + seriesScore.y;
        winnerText.text = winnerT;
    }
}
