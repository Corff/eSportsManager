using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Enums;

public class MatchEngine
{
    private Team Team1;
    private Team Team2;

    public MatchEngine(Team team1, Team team2)
    {
        Team1 = team1;
        Team2 = team2;
    }

    public Vector2 SimulateMatch(int numberOfMaps)
    {
        Vector2 seriesScore = new(0, 0);
        int winningMargin = numberOfMaps == 1 ? 1 : numberOfMaps == 3 ? 2 : 3;

        for (int i = 0; i < numberOfMaps; i++)
        {
            seriesScore = SimulateMap(new(0, 0), 24);
            if (seriesScore.x == winningMargin || seriesScore.y == winningMargin) break;
        }

        return seriesScore;
    }

    private Vector2 SimulateMap(Vector2 startScore, int roundsToBePlayed)
    {
        Vector2 mapScore = new(0, 0);
        int winningMargin = roundsToBePlayed == 24 ? 23 : 4;
        for (int i = 0; i < roundsToBePlayed; i++)
        {
            mapScore = SimulateRound(mapScore);
            if (mapScore.x == winningMargin || mapScore.y == winningMargin) return startScore + mapScore;
        }
        return SimulateMap(startScore + mapScore, 6);
    }

    private Vector2 SimulateRound(Vector2 startScore)
    {
        Dictionary<Player, int> team1PlayersAndHealth = new();
        Dictionary<Player, int> team2PlayersAndHealth = new();
        for (int i = 0; i < Team1.Players.Count; i++)
        {
            team1PlayersAndHealth.Add(Team1.Players[i], 100);
            team2PlayersAndHealth.Add(Team2.Players[i], 100);
        }
        bool running = true;
        int fightCount = 0;
        while(running)
        {
            //Simulate Battle
            Player player1;
            Player player2;
            do
            { 
                int playerIndex = Random.Range(0,5);
                player1 = Team1.Players[playerIndex];
            }
            while(team1PlayersAndHealth[player1] != 0);
            do
            {
                int playerIndex = Random.Range(0,5);
                player2 = Team2.Players[playerIndex];
            }
            while(team2PlayersAndHealth[player2] != 0);
            
            if(fightCount == 0)
            {
                //Entry
                double player1Entrying = player1.Entrying;
                double player2Entrying = player2.Entrying;
                double
                if(player1Entrying > player2Entrying)
                {
                    //Player1 is better
                }
                else
                {
                    //Player2 is better
                }
            }

            fightCount += 1;
        }
        return startScore;
    }
}
