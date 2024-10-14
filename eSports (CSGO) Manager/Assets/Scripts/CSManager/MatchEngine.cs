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

        return startScore;
    }
}
