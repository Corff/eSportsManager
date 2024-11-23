using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Enums;
using Random = UnityEngine.Random;

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
        
        float halfwayPoint = ((float)numberOfMaps) / 2f;
        int winningMargin = (int)Math.Ceiling(halfwayPoint);
        if (winningMargin == halfwayPoint)
        {
            winningMargin += 1;}
        
        for (int i = 0; i < numberOfMaps; i++)
        {
            //seriesScore = SimulateMap(new(0,0), 24);
            Vector2 mapScore = SimulateMap(new(0, 0), 24);
            if (mapScore.x > mapScore.y) seriesScore.x += 1;
            else seriesScore.y += 1;
            if (Mathf.Approximately(seriesScore.x, winningMargin) || Mathf.Approximately(seriesScore.y, winningMargin))
            {
                break;
            }
        }

        Debug.Log($"SeriesScore: {seriesScore.x}:{seriesScore.y}");
        return seriesScore;
    }

    private Vector2 SimulateMap(Vector2 startScore, int roundsToBePlayed)
    {
        Vector2 mapScore = new(0, 0);
        int winningMargin = roundsToBePlayed == 24 ? 13 : 4;
        for (int i = 0; i < roundsToBePlayed; i++)
        {
            mapScore = SimulateRound(mapScore);
            if (mapScore.x == winningMargin || mapScore.y == winningMargin)
            {
                Vector2 finalScore = startScore + mapScore;
                Debug.Log($"{finalScore.x}:{finalScore.y}");
                return finalScore;
            }
        }

        return SimulateMap(startScore + mapScore, 6);
    }

    private Vector2 SimulateRound(Vector2 startScore)
    {
        ResetPlayersHealth(Team1, Team2);
        bool running = true;
        int fightCount = 0;
        while (running)
        {
            (Player p1, Player p2) = PickPlayers(Team1, Team2);
            //Debug.Log($"{p1.InGameName} vs {p2.InGameName}");
            SimulateFight(ref p1, ref p2);
            fightCount += 1;

            if (!CheckTeamIsAlive(Team1))
            {
                startScore.x += 1;
                running = false;
            }

            if (!CheckTeamIsAlive(Team2))
            {
                startScore.y += 1;
                running = false;
            }
        }
        return startScore;
    }

    private (Player, Player) PickPlayers(Team t1, Team t2)
    {
        Player t1Player;
        Player t2Player;
        do
        {
            int playerIndex = Random.Range(0, Team1.Players.Count);
            t1Player = Team1.Players[playerIndex];
        } while (t1Player.Health == 0);

        do
        {
            int playerIndex = Random.Range(0, Team2.Players.Count);
            t2Player = Team2.Players[playerIndex];
        } while (t2Player.Health == 0);
        return (t1Player, t2Player);
    }

    private void SimulateFight(ref Player p1, ref Player p2)
    {
        //Determine p1 damage dealt
        double p1DamageDealt = Random.Range(0f, 100f);
        //Determine p2 damage dealt
        double p2DamageDealt = Random.Range(0f, 100f);
        //Determine Damage Randomly
        if (Random.Range(0, 2) == 1)
        {
            p2.Health -= p1DamageDealt;
            if (p2.Health > 0)
            {
                p1.Health -= p2DamageDealt;
            }
        }
        else
        {
            p1.Health -= p2DamageDealt;
            if (p1.Health > 0)
            {
                p2.Health -= p1DamageDealt;
            }
        }
    }

    private bool CheckTeamIsAlive(Team team)
    {
        bool retVal = true;
        var teamHealths = team.Players.Select(i => i.Health);
        if (teamHealths.All(i => i <= 0)) retVal = false; 
        return retVal;
    }

    private void ResetPlayersHealth(Team team1, Team team2)
    {
        for (int i = 0; i < team1.Players.Count; i++)
        {
            team1.Players[i].Health = 100;
            team2.Players[i].Health = 100;
        }
    }
}
